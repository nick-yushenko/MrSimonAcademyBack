using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MrSimonAcademy2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MrSimonAcademy2.Controllers
{
    // Данный контроллер доступен только для авторизованных пользователей
    [Authorize]
    public class ManageController : Controller
    {
        // Менеджер пользователей для взаимодействия с конкретным пользователем
        private ApplicationUserManager _userManager;

        // Конструктор
        public ManageController()
        {
        }

        // Конструктор по менеджеру пользователей
        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        // Получение менеджера пользователей
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Manage/Index
        public ActionResult Index()
        {


            var userId = User.Identity.GetUserId();
            var user = new User();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.Find(userId);
            }
            var model = new IndexViewModel
            {
                //HasPassword = HasPassword(),
                //PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                //TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                //Logins = await UserManager.GetLoginsAsync(userId),
                //BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
                UserFName = user.UserFName,
                UserLName = user.UserLName,
                Birthday = user.Birthday, 
                Email = user.Email
            };
            return View(model);
        }

        public async Task<ActionResult> Edit()
        {
            User user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                EditModel model = new EditModel { UserFName = user.UserFName, UserLName = user.UserLName };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            User user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.UserFName = model.UserFName;
                user.UserLName = model.UserLName;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Manage");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
    }
}