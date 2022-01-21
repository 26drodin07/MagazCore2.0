using CodeFirst;
using CodeFirst.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazCore2._0.Repos
{
    public class CategoryRepo
    {
        private readonly DataContext dataContext;

        public CategoryRepo(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task AddCategoryAsync(Category category)
        {
            await dataContext.Categories.AddAsync(category);
            await dataContext.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            dataContext.Categories.Remove(category);
            await dataContext.SaveChangesAsync();
        }
        public async Task AddPropAsync(int cat_id, string name)
        {
            var Parent = new PropCat { Category = await GetCat(cat_id), Name = name };
            await dataContext.Props_Cat.AddAsync(Parent);
            List<Product> updated = dataContext.Products.Where(i => i.CategoryId == cat_id).ToList();
            foreach (var item in updated)
            {
                await dataContext.Props_Prod.AddAsync(new PropProd { Name = name, Product = item,Parent= Parent });
            }
            await dataContext.SaveChangesAsync();
        }

        public async Task DeletePropAsync(int prop_id)
        {
            PropCat prop = dataContext.Props_Cat.Find(prop_id);
            List<PropProd> updated = dataContext.Props_Prod.Where(i =>  i.ParentId == prop_id).ToList();
            dataContext.Props_Prod.RemoveRange(updated);
            dataContext.Props_Cat.Remove(prop);
            await dataContext.SaveChangesAsync();
        }
        public async Task<List<Category>> GetListCatAsync()
        {
            return await dataContext.Categories.ToListAsync();
        }
        public async Task<Category> GetCat(int id)
        {
            return await dataContext.Categories.FirstOrDefaultAsync(i => i.ID == id);
        }
        public async Task EditAsync(Category category)
        {
            dataContext.Update(category);
            await dataContext.SaveChangesAsync();
        }

        public List<PropCat> GetProps(int id)
        {
            return dataContext.Props_Cat.Where(a => a.CategoryId == id).ToList();
        }


    }
}
