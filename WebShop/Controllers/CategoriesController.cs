using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Data.Entities;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly MyAppContext _appContext;
        private readonly IMapper _mapper;

        public CategoriesController(MyAppContext appContext, IMapper mapper)
        {
            _appContext = appContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var list = _appContext.Categories.Select(x => _mapper.Map<CategoryItemViewModel>(x)).ToList();
            return View(list);
        }
        //
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            string fileName = string.Empty;
            if (model.UploadImage != null)
            {
                var fileExp = Path.GetExtension(model.UploadImage.FileName);
                var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "images");
                fileName = Path.GetRandomFileName() + fileExp;
                using (var stream = System.IO.File.Create(Path.Combine(dirPath, fileName)))
                {
                    await model.UploadImage.CopyToAsync(stream);
                }
            }
            CategoryEntity entity = _mapper.Map<CategoryEntity>(model);
            entity.Image = fileName;
            _appContext.Categories.Add(entity);
            _appContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
