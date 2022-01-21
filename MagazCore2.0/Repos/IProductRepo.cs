using CodeFirst.Model;
using MagazCore2._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagazCore2._0.Repos
{
    public interface IProductRepo
    {
        Task AddProdAsync(ProductViewModel myproduct);
        Task AddReviewAsync(string text, int user_id, int prod_id, short rate);
        Task CreatePropsAsync(int prod_id);
        Task DeleteProdAsync(Product product);
        Task DeleteProdAsync(int prod_id);
        SelectList Droplist();
        Task EditAsync(Product product);
        Task EditProps(List<PropProd> props, int ProdId);
        List<Product> Filter(Dictionary<string, List<string>> filters, int cat_id);
        Task<byte[]> GetImage(int id);
        Task<List<ProductViewModel>> GetListProdAsync();
        List<Product> GetProdByCat(int id);
        Task<ProductViewModel> GetProduct(int id);
        List<PropProd> GetProps(int id);
        List<string> GetPropsNames(int cat_id);
        Dictionary<string, List<string>> GetPropsValues(int cat_id);
        List<Shop_Prod> GetShops(int prod_id);
        List<Shop> GetShopsNames(int prod_id);
        List<Product> Search(string name);
        List<Product> Search(string name, int cat_id);
    }
}