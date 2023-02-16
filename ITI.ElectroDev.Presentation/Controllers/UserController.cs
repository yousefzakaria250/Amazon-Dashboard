using ITI.ElectroDev.Models;
using ITI.ElectroDev.Presentation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using System.Dynamic;
using System.Xml.Linq;

namespace ITI.ElectroDev.Presentation
{

    public class UserController : Controller
    {
        Context c;
        UserManager<User> UserManager;
        SignInManager<User> SignInManager;
        RoleManager<IdentityRole> RoleManager;
        public UserController(
            UserManager<User> usermanager,
            SignInManager<User> signInManager,
            Context _c,
            RoleManager<IdentityRole> roleManager
            )

        {
            UserManager = usermanager;
            SignInManager = signInManager;
            this.c = _c;
            RoleManager = roleManager;
        }

        [HttpGet]

        [Authorize(Roles = "Admin")]
        public IActionResult SignUp()
        {
            ViewBag.Title = "Sign Up";

            ViewBag.Roles = RoleManager.Roles.Select(i => new SelectListItem(i.Name, i.Name));
            return View();
        }




        [HttpPost]

        
		[HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {

            if (ModelState.IsValid == false)
            {

                return View();

            }
            else
            {
                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.UserName,

                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded == false)
                {
                    result.Errors.ToList().ForEach(i =>
                    {
                        ModelState.AddModelError("", i.Description);

                    });
                    return View();

                }
                else
                {
                    await UserManager.AddToRoleAsync(user, model.Role);

                    return RedirectToAction("UsersDetails", "User");
                }
            }
        }
        [HttpGet]
        public IActionResult SignIn(string ReturnUrl = null)
        {
            ViewBag.UserName = c.Users.Select(i => new SelectListItem(i.UserName, i.UserName));
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.Title = "Sign In";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {

            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                User user = new User()
                {
                    UserName = model.UserName

                };
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded == false)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View();

                }
                else
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
        }
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            TempData["AlertMessage"] = "You Signed Out Successfully";

            return RedirectToAction("SignIn", "User");

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult UsersDetails()
        {
            ViewBag.Title = "All users";
            var users = c.Users.ToList();

            return View(users);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["AlertMessage"] = "User Not Found";
                return View("Error");

            }
            else
            {
                TempData["AlertMessage"] = "User Deleted Successfully";
                var result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("UsersDetails");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View("UsersDetails");

            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["AlertMessage"] = "User Not Found";
                return View("Error");

            }
            //var userRoles = await UserManager.GetRolesAsync(user);
            //var userClaims = await UserManager.GetClaimsAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                //Roles = userRoles,
                //Claims = userClaims.Select(i => i.Value).ToList()

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                TempData["AlertMessage"] = "User Not Found";
                return View("Error");

            }
            else
            {

                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["AlertMessage"] = "User Informations updated successfully";

                    return RedirectToAction("UsersDetails");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}