using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ProductsController(IProductsService productsService,ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;

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
        public IActionResult Create()
        {
            var catList = _categoriesService.GetCategories();

            ViewBag.Categories = catList;
            return View();
        }

        [HttpPost]

        public IActionResult Create(ProductViewModel data)
        {
            try
            {
                _productsService.AddProduct(data);

                ViewData["feedback"] = "Product was added successfully";
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
