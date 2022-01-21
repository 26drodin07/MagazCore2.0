using CodeFirst.Model;
using MagazCore2._0.Models;
using MagazCore2._0.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazCore2._0.Controllers
{
    public class ProductController : Controller
    {
        private readonly CategoryRepo _catRepo;
        private readonly IProductRepo _prodRepo;

        public ProductController(CategoryRepo catRepo, IProductRepo prodRepo)
        {
            _catRepo = catRepo;
            _prodRepo = prodRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ProductList()
        {
            return View(await _prodRepo.GetListProdAsync()) ;
        }
        public async Task<ActionResult> CategoryList()
        {
            return View(await _catRepo.GetListCatAsync());
        }

        public async Task<ActionResult> CreateCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category) 
        {
            if (ModelState.IsValid)
            await _catRepo.AddCategoryAsync(category);
            return RedirectToAction("CategoryList");
        }

        public async Task<ActionResult> EditCategory(int id)
        {
            return View(await _catRepo.GetCat(id));
        }

        [HttpPost]
        public async Task<ActionResult> EditCategory(Category category)
        {
            if (category != null)
                await _catRepo.EditAsync(category);
            return RedirectToAction("ProductList");
        }


        public async Task<ActionResult> CreateProduct()
        {

            
            ViewBag.Categories = (await _catRepo.GetListCatAsync())
                .Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
               ;
            return View(new ProductViewModel());
        }


        [HttpGet]
        public async Task<ActionResult> GetImage(int id)
        {
            

            return File(await _prodRepo.GetImage(id), "image/gif");
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductViewModel product)
        {
            
            if (ModelState.IsValid)
                await _prodRepo.AddProdAsync(product);
            return RedirectToAction("ProductList");
        }
        
        public async Task<ActionResult> DeleteProduct(int id)
        {          
            await _prodRepo.DeleteProdAsync(id);
            return RedirectToAction("ProductList");
        }



    }
}
