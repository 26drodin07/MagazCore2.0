using AutoMapper;
using CodeFirst;
using CodeFirst.Model;
using MagazCore2._0.Models;
using MagazCore2._0.MongoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MagazCore2._0.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly DataContext db;
        private readonly ImageService imgsrv;

        public ProductRepo(DataContext db, ImageService imgsrv)
        {
            this.imgsrv = imgsrv;
            this.db = db;
        }
        public async Task<List<ProductViewModel>> GetListProdAsync()
        {
            var config = new MapperConfiguration(cfg =>
             cfg.CreateMap<Product, ProductViewModel>()
            );

            var mapper = new Mapper(config);
            var result = await mapper.ProjectTo<ProductViewModel>(db.Products.Include(x => x.Category), config).ToListAsync();
            foreach (var item in result)
            {
                var image = await GetImage(item.ID);
                var stream = new MemoryStream(image);               
                item.Picture = new FormFile(stream, 0, image.Length, "name", "fileName");
            }
            return result;
        }

        public async Task AddProdAsync(ProductViewModel myproduct)
        {

            Product product = new Product
            {
                CategoryId = myproduct.CategoryId,
                Name = myproduct.Name,
                Price = myproduct.Price,
                PictureId = ObjectId.GenerateNewId().ToString()

            };
            await imgsrv.StoreImage(product.PictureId, myproduct.Picture.OpenReadStream(), product.Name);

            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            db.Products.Update(await db.Products.FindAsync(product.ID));
            await db.SaveChangesAsync();
        }
        public async Task CreatePropsAsync(int prod_id)
        {
            Product product = await db.Products.FindAsync(prod_id);
            List<PropCat> a = db.Props_Cat.Where(b => b.CategoryId == product.CategoryId).ToList();
            foreach (var item in a)
            {
                await db.Props_Prod.AddAsync(new PropProd { Name = item.Name, Product = product });
            }
            await db.SaveChangesAsync();
        }

        public async Task<ProductViewModel> GetProduct(int id)
        {
            Product tmp = await db.Products.FindAsync(id);
            var image = await imgsrv.GetImage(tmp.PictureId);
            ProductViewModel result = new ProductViewModel()
            {
                Category = tmp.Category,
                Name = tmp.Name,
                Price = tmp.Price,
                Picture = new FormFile(new MemoryStream(image), 0, image.Length, "name", tmp.Name)
            };

            return result;

        }
        public async Task<byte[]> GetImage(int id)
        {
            return await imgsrv.GetImage((await db.Products.FindAsync(id)).PictureId);
        }


        public async Task EditProps(List<PropProd> props, int ProdId)
        {
            foreach (var item in props)
                item.ProductId = ProdId;
            db.UpdateRange(props);
            await db.SaveChangesAsync();
        }

        public List<PropProd> GetProps(int id)
        {
            return db.Props_Prod.Where(a => a.ProductId == id).ToList();
        }

        public List<Product> GetProdByCat(int id)
        {
            return db.Products.Where(a => a.CategoryId == id).ToList();
        }

        public SelectList Droplist()
        {
            return (new SelectList(db.Categories, "ID", "Name"));
        }
        //Имена свойтсв
        public List<string> GetPropsNames(int cat_id)
        {
            return db.Props_Prod.Where(i => i.Product.CategoryId == cat_id).Select(j => j.Name).Distinct().ToList();
        }
        //Вывод всех доступных значений свойств 
        public Dictionary<string, List<String>> GetPropsValues(int cat_id)
        {
            var propnames = GetPropsNames(cat_id);
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach (var propname in propnames)
            {
                result.Add(propname, db.Props_Prod
                    .Where(pp => pp.Name == propname && pp.Product.Category.ID == cat_id)
                    .Select(f => f.Value).Distinct().ToList());
            }
            return result;
        }
        public List<Shop_Prod> GetShops(int prod_id)//Наличие в магазинах и количество
        {
            return db.Shop_Prods.Where(i => i.ProductId == prod_id).ToList();
        }
        public List<Shop> GetShopsNames(int prod_id)//Магазины с товаром
        {
            return db.Shop_Prods.Include(s => s.Shop).Where(i => i.ProductId == prod_id).Select(j => j.Shop).ToList();
        }
        public List<Product> Search(string name, int cat_id)
        {
            return db.Products.Include(x => x.Category).Where(i => i.Name.Contains(name) && i.CategoryId == cat_id).ToList();
        }
        public List<Product> Search(string name)
        {
            return db.Products.Where(i => i.Name.Contains(name)).ToList();
        }
        public async Task DeleteProdAsync(Product product)
        {
            db.Products.Remove(product);
            await db.SaveChangesAsync();
        }

        public async Task DeleteProdAsync(int prod_id)
        {
           var prod = await db.Products.FindAsync(prod_id);
           await imgsrv.DeleteImage(prod.PictureId);
            db.Products.Remove(prod);
            await db.SaveChangesAsync();
        }

        public async Task AddReviewAsync(string text, int user_id, int prod_id, short rate)
        {
            User account = await db.Users.FindAsync(user_id);
            Product product = await db.Products.FindAsync(prod_id);
            await db.Reviews.AddAsync(new Review()
            {
                Date = DateTime.Now,
                Text = text,
                Account = account,
                Product = product,
                Rate = rate
            });
            await db.SaveChangesAsync();
        }
        public List<Product> Filter(Dictionary<string, List<string>> filters, int cat_id)
        {
            List<Product> list = db.Products.Where(i => i.Category.ID == cat_id).ToList();
            List<Product> tmp = new List<Product>();
            foreach (string keys in filters.Keys)
            {
                foreach (string value in filters[keys])
                {
                    tmp.AddRange(list
                    .Where(i => i.Props
                    .FirstOrDefault(j => (j.Name == keys) && (j.Value == value)) != null)
                    .ToList());
                }
                list.Clear();
                list.AddRange(tmp);
                tmp.Clear();
            }
            return list.OrderBy(i => i.Price).ToList();
        }



    }
}
