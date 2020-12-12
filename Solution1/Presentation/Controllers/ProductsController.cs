using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Application.ViewModels;

namespace Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private ICategoriesService _categoriesService;
        private IWebHostEnvironment _env;

        public ProductsController(IProductsService productsService,ICategoriesService categoriesService,IWebHostEnvironment env)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _env = env;

        }

        // Products Catalogue
        public IActionResult Index()
        {

            //IQueryable -- Most Efficient
            //IEnumerable
            //List >>> IEnumerable >>> IQueryable

            var list = _productsService.GetProducts();
            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var myProduct = _productsService.GetProduct(id);
            return View(myProduct);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var catList = _categoriesService.GetCategories();

            ViewBag.Categories = catList;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data,IFormFile file)
        {
            try
            {

                if(file != null)
                {
                    if(file.Length > 0)
                    {
                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);

                        string absolutePath = _env.WebRootPath + @"\Images\";

                        using (var stream = System.IO.File.Create(absolutePath + newFilename))
                        {
                            file.CopyTo(stream);
                        }

                        data.ImageUrl = @"\Images\" + newFilename;
                    }
                }

                _productsService.AddProduct(data);

                ViewData["feedback"] = "Product was added successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewData["warning"] = "Product was not added. Check your details";

            }
            var catList = _categoriesService.GetCategories();

            ViewBag.Categories = catList;
            return View();
        }

        public IActionResult Delete(Guid id)
        {

            _productsService.DeleteProduct(id);
            TempData["warning"] = "Product was Deleted";
            return RedirectToAction("Index");
        }
    }
}
