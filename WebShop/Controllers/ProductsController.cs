using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebShop.Data;
using WebShop.Data.Entities;
using WebShop.Models;
using WebShop.Models.Helpers;

namespace WebShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyAppContext _appContext;
        private readonly IMapper _mapper;

        public ProductsController(MyAppContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = _appContext.Products
                .Include(x=>x.Category)
                .Select(x => _mapper.Map<ProductItemViewModel>(x))
                .ToList();
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.Categories = _appContext.Categories.Select(x =>
            _mapper.Map<SelectItemViewModel>(x)).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product =_mapper.Map<ProductEntity>(model);
                _appContext.Products.Add(product);
                _appContext.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            model.Categories = _appContext.Categories.Select(x =>
            _mapper.Map<SelectItemViewModel>(x)).ToList();
            return View(model);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductEditViewModel model = _appContext.Products
                //.Include(x=>x.Category)
                .Where(x=>x.Id==id)
                .Select(x=>_mapper.Map<ProductEditViewModel>(x)).SingleOrDefault();
            model.Categories = _appContext.Categories.Select(x =>
            _mapper.Map<SelectItemViewModel>(x)).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<ProductEntity>(model);
                _appContext.Products.Update(product);
                _appContext.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            model.Categories = _appContext.Categories.Select(x =>
            _mapper.Map<SelectItemViewModel>(x)).ToList();
            return View(model);
        }

        [HttpPost]
        public string Delete(ProductEditViewModel model)
        {
            ProductEntity p = _appContext.Products.SingleOrDefault(x => x.Id == model.Id);
            if (p!=null)
            {
                _appContext.Products.Remove(p);
                _appContext.SaveChanges();
            }
           
            return "ok";
        }
    }
}
