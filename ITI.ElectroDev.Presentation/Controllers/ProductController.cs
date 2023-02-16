using ITI.ElectroDev.Models;
using ITI.ElectroDev.Presentation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System.Dynamic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using X.PagedList;

namespace ITI.ElectroDev.Presentation
{

    [Authorize(Roles = "Admin,Editor")]

    public class ProductController : Controller
    {
        private IWebHostEnvironment Environment;

        private Context context;
        IConfiguration con;
        public ProductController(Context _context , IConfiguration _con , IWebHostEnvironment _environment)
        {
            context = _context;
            con = _con;
            Environment = _environment;
        }

        [HttpGet]
        public  IActionResult Index(int PageIndex = 1, int PageSize = 3)
        {
            var product =  context.Product.ToPagedList(PageIndex, PageSize);
            // ViewBag.ImagesPath = con.GetSection("ImagesPath").Value.ToString();
            string wwwPath = this.Environment.WebRootPath;
            ViewBag.contentPath = this.Environment.ContentRootPath;
            return View(product);
        }

        [HttpGet]
        public  IActionResult Create()
        {
            ViewBag.brands = context.Brands.Select(i => new SelectListItem(i.Name, i.Id.ToString()));
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductCreateModel createModel)
        {

            if (ModelState.IsValid == false)
            {
                var errors =
                    ModelState.SelectMany(i => i.Value.Errors.Select(x => x.ErrorMessage));

                foreach (string err in errors)
                    ModelState.AddModelError("", err);


                ViewBag.brands = context.Brands.Select(i => new SelectListItem(i.Name, i.Id.ToString()));

                return View();
            }
            else
            {
                List<ProductImages> productImages = new List<ProductImages>();
                foreach (IFormFile file in createModel.Images)
                {
                    // Change file Name by Generated Unigue Identifier , because when add in
                    // Images Folder perhaps is Name File The Same Name and This is not good  
                    string newName = Guid.NewGuid().ToString() + file.FileName;
                    productImages.Add(new ProductImages()
                    {
                        Path = newName
                    });


                    // to collect object in Memory
                    FileStream fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", newName), FileMode.OpenOrCreate, FileAccess.ReadWrite);

                    // to Transfer from stream to hard disk
                    file.CopyTo(fs);

                    // To Close Stream 
                    fs.Position = 0;
                }

                context.Product.Add(new Product()
                {
                    Name = createModel.Name,
                    Description = createModel.Description,
                    BrandId = createModel.BrandId,
                    Price = createModel.Price,
                    Discount = createModel.Discount,
                    DiscountPrice=createModel.Price - (createModel.Price * createModel.Discount / 100),
                    Quantity = createModel.Quantity ,
                    ProductImages = productImages,
                });




                context.SaveChanges();

                return RedirectToAction("Index");

            }





        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var prd = await context.Product.FindAsync(id);

                if (prd == null)
                return NotFound();

            var viewModel = new ProductCreateModel
            {
                id = prd.Id,
                Name = prd.Name,
                Description = prd.Description,
                BrandId = prd.BrandId,
                Price = prd.Price,
                Discount = prd.Discount,
                Quantity = prd.Quantity,
                

            };
            ViewBag.brands = context.Brands.Select(i => new SelectListItem(i.Name, i.Id.ToString()));

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateModel model)
        {
            if (ModelState.IsValid == false)
            {
                var errors =
                    ModelState.SelectMany(i => i.Value.Errors.Select(x => x.ErrorMessage));

                foreach (string err in errors)
                    ModelState.AddModelError("", err);


                ViewBag.brands = context.Brands.Select(i => new SelectListItem(i.Name, i.Id.ToString()));

                return View();
            }
            var prd = await context.Product.FindAsync(model.id);

            if (prd == null)
                return NotFound();
            prd.Name = model.Name;
            prd.Description = model.Description;
            prd.Price = model.Price;
            prd.Discount = model.Discount;
            prd.DiscountPrice = model.Price-(model.Price * model.Discount / 100);
            prd.Quantity = model.Quantity;
            prd.BrandId = model.BrandId;

            context.SaveChanges();
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var prd = await context.Product.FindAsync(id);

            if (prd == null)
                return NotFound();

            return View(prd);

        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id, string Name)
        {
            dynamic product = new ExpandoObject();
            product.Name = Name;
            product.ID = id;
            return View(product);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = context.Product.FirstOrDefault(i => i.Id == id);
            context.Product.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Search(string term, int PageIndex = 1, int PageSize = 2)
        {
            var resulte = context.Product.Where(p => p.Name.Contains(term) || p.Description.Contains(term)).ToPagedList(PageIndex, PageSize);
            
            return View("Index" , resulte);
        }


    }
}
