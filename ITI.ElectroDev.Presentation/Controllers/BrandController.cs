using ITI.ElectroDev.Models;
using ITI.ElectroDev.Presentation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Xml.Linq;
using X.PagedList;

namespace ITI.ElectroDev.Presentation
{
    [Authorize(Roles ="Admin,Editor")]
    public class BrandController : Controller
    {
        Context dbcontext;
        public BrandController(Context _dbContext)
        {
            dbcontext = _dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Brands = dbcontext.Brands.ToList();
            return View(Brands);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Title = "Brand Details";
            Brand brand = dbcontext.Brands.FirstOrDefault(b => b.Id == id);
            return View(brand);
        }

        [HttpGet]
        public IActionResult AddNewBrand()
        {
            ViewBag.Title = "Add New Brand";
            ViewBag.Categories = dbcontext.Category
                .Select(b => new SelectListItem(b.Name, b.Id.ToString()));
            return View();
        }

        [HttpPost]

        public IActionResult AddNewBrand(BrandModel brand)
        {
            //string image;

            if (ModelState.IsValid == false)
            {
                var errors =
                    ModelState.SelectMany(i => i.Value.Errors.Select(x => x.ErrorMessage));

                foreach (string err in errors)
                    ModelState.AddModelError("", err);
                ViewBag.Success = false;
                return View();
            }
            else
            {
                Brand Brand = new Brand
                {
                    Name = brand.Name,
                    CategoryId = brand.CategoryId
                };
                dbcontext.Brands.Add(Brand);
                dbcontext.SaveChanges();
                ViewBag.Success = true;
                return View();
            }
        }

        
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Brand";
            ViewBag.Categories = dbcontext.Category
                .Select(b => new SelectListItem(b.Name, b.Id.ToString()));
            Brand brand = dbcontext.Brands.FirstOrDefault(i => i.Id == id);
            BrandModel brandModel = new BrandModel
            {
                CategoryId = brand.CategoryId,
                Id = brand.Id,
                Name = brand.Name
            };
            return View(brandModel);
        }
                
        [HttpPost]
        public IActionResult Edit(BrandModel model)
        {
            Brand brand = dbcontext.Brands.FirstOrDefault(i => i.Id == model.Id);
            brand.Name = model.Name;
            brand.CategoryId=model.CategoryId;
            dbcontext.Brands.Update(brand);
            dbcontext.SaveChanges();
            return RedirectToAction("AllBrand"); 
        }

        [HttpGet]
        public IActionResult Delete(Brand brand)
        {
            dbcontext.Brands.Remove(brand);
            dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
