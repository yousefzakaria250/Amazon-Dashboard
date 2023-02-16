using ITI.ElectroDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ITI.ElectroDev.Presentation
{
    [Authorize(Roles = "Admin,Editor")]

    public class CategoryController : Controller
    {
        private Context db;
        public CategoryController(Context _db)
        {
            db = _db; 
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = db.Category.ToList();
            return View(categories);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]

        public IActionResult Add()
        {
            
            return View();
        }

        [HttpPost]

        public IActionResult Add(CategoryCreateModel model)
        {

            if (ModelState.IsValid == false)
            {
                var errors =
                     ModelState.SelectMany(i => i.Value.Errors.Select(x => x.ErrorMessage));

                foreach (string err in errors)
                    ModelState.AddModelError("", err);

                return View();
            }
            else
            {
                db.Category.Add(new Category { Name = model.Name });
                db.SaveChanges();
                return View();
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]

        public IActionResult Edit(int id, string name)
        {
            
            dynamic category = new ExpandoObject();
            category.Name = name;
            category.Id = id;
            return View(category);
        }
        [HttpGet]

        public IActionResult SaveEdit(int id,string name)
        {
            var category = db.Category.FirstOrDefault(i => i.Id == id);
            category.Name = name;
            category.Id = id;
            db.Category.Update(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]

        public IActionResult Delete(int id)
        {
            var category = db.Category.FirstOrDefault(i => i.Id == id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
