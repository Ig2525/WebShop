using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebShop.Data;
using WebShop.Data.Entities;
using WebShop.Helpers;
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
                foreach (var id in model.Images)
                {
                    var productImage = _appContext.ProductImages.SingleOrDefault(x => x.Id == id);
                    productImage.ProductId = product.Id;
                    _appContext.SaveChanges();
                }
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
            if (p != null)
            {
                _appContext.Products.Remove(p);
                _appContext.SaveChanges();
            }
           
            return "ok";
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var p = _appContext.Products
                 .Include(c => c.Category)
                .Include(x=>x.ProductImages)
                .SingleOrDefault(x => x.Id == id);

            var model = _mapper.Map<ProductDetailsViewModel>(p);

            return View(model);
        }

        [HttpPost]
        public async Task<ProductImageItemViewModel> Upload(ProductImageCreateViewModel model)
        {
            string fileName = string.Empty;
            if (model.File != null)
            {
                var fileExp = Path.GetExtension(model.File.FileName);

                //вставити
                var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "images");
                fileName = Path.GetRandomFileName() + fileExp;
               
                using(var ms= new MemoryStream())
                {
                    model.File.CopyTo(ms);
                    Bitmap bmp =new Bitmap(Image.FromStream(ms));
                    var saveImage = ImageWorker.CompressImage(bmp, 1200, 1200, false);
                    if(saveImage.Height>saveImage.Width)
                        saveImage.RotateFlip(RotateFlipType.Rotate270FlipNone); //Автоповорот фото
                    saveImage.Save(Path.Combine(dirPath,fileName), ImageFormat.Jpeg);
                }


                //using (var stream = System.IO.File.Create(Path.Combine(dirPath, fileName)))
                //{
                //    await model.File.CopyToAsync(stream);
                //}
            }
            ProductImageEntity productImage = new ProductImageEntity();
            productImage.Name = fileName;
            _appContext.ProductImages.Add(productImage);
            _appContext.SaveChanges();
            return new ProductImageItemViewModel { Id = productImage.Id, Name = productImage.Name };
        }

    }
}
