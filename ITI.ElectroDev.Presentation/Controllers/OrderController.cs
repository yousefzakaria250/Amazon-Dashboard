using ITI.ElectroDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace ITI.ElectroDev.Presentation
{
    [Authorize(Roles = "Admin,Editor")]

    public class OrderController : Controller
    {
        Context context;
        public OrderController(Context _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Orders List";
            List<OrderDetails> orders=context.OrderDetails.ToList();
            return View(orders);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]

        public IActionResult OrderInfo(int id)
        {
            ViewBag.Title = "Order Information";
            var order = context.OrderDetails.FirstOrDefault(x => x.Id == id);
            return View(order);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]

        public IActionResult Add()
        {
            ViewBag.Title = "Add New Order";
            ViewBag.Products = context.Product
                .Select(p => new SelectListItem(p.Name, p.Id.ToString()));
            ViewBag.Users = context.Users
                .Select(u => new SelectListItem(u.FirstName + u.LastName, u.Id)); //Id of user is string
            return View();
        }

        [HttpPost]
        public IActionResult Add(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = context.Product
                .Select(p => new SelectListItem(p.Name, p.Id.ToString()));
                ViewBag.Users = context.Users
                    .Select(u => new SelectListItem(u.FirstName + u.LastName, u.Id)); //Id of user is string
                return View();
            }
            else
            {
                context.OrderDetails.Add(new OrderDetails
                {
                    UserId = model.UserId,
                    Status = model.Status,
                    PaymentMethod = model.Type,
                    TotalPrice = model.TotalPrice
                });
                context.SaveChanges();
                context.OrderItems.Add(new OrderItems
                {
                    ProductId = model.ProductId,
                    //CreatedAt = model.CreatedAt
                });
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]

        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit";
            ViewBag.Products = context.Product
                .Select(p => new SelectListItem(p.Name, p.Id.ToString()));
            ViewBag.Users = context.Users
                .Select(u => new SelectListItem(u.FirstName + u.LastName, u.Id));
            OrderDetails order = context.OrderDetails.FirstOrDefault(o => o.Id == id);
            OrderViewModel orderModel = new OrderViewModel
            {
                Status = order.Status,
                Type = order.PaymentMethod
            };
            return View(orderModel);
        }
        [HttpPost]
        public IActionResult Edit(OrderViewModel model)
        {
            OrderDetails order = context.OrderDetails.FirstOrDefault(o => o.Id == model.Id);
            order.Status = model.Status;
            order.PaymentMethod = model.Type;
            context.OrderDetails.Update(order);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
