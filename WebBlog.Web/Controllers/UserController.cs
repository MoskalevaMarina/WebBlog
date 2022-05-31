using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebBlog.BLL.Services;
using WebBlog.DAL.Entities;
using WebBlog.Web.Models;

namespace WebBlog.Web.Controllers
{
    public class UserController : Controller
    {
        private UserService service;
        private RoleService roleService;
        private IMapper _mapper;
        IWebHostEnvironment _appEnvironment;

        public UserController(UserService service1, IMapper mapper, RoleService roleService1, IWebHostEnvironment _appEnvironment1)
        {
            service = service1;
            _mapper = mapper;
            roleService = roleService1;
            _appEnvironment = _appEnvironment1;
        }

       public IActionResult Index()
        {
            return View("IndexUser");
        }


        [HttpGet]
        public IActionResult IndexAdmin()
        {
            return View(GetUsers());
        }

        [HttpGet]
        public IActionResult IndexUser(int iduser)
        {
            if (iduser != 0)
            {
                User user = service.GetUser(iduser);
                if (user != null)
                {
                    return View(GetUser(iduser));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Profile(int id)
        {
            if (id != 0)
            {
                User user = service.GetUser(id);
                if (user != null)
                {
                    var r1 = _mapper.Map<UserViewModel>(user);
                    return View(r1);
                }
            }
            return NotFound();
        }


        public IActionResult Profile1()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = service.GetUserbyEmail(User.Identity.Name);
                if (user != null)
                {
                    var r1 = _mapper.Map<UserViewModel>(user);
                    return View("Profile", r1);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            AddUserViewModel model = new AddUserViewModel();
            model.sproles = roleService.GetRoles().ToList();
            return View("CreateAdmin", model);
        }

        //sozdanie usera adminom
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddUserViewModel user,  IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                string path;
                if (upload != null)
                {
                    // путь к папке Files
                    path = "/images/images_user/" + upload.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        upload.CopyTo(fileStream);
                    }
                }
                else
                {
                    path = "/images/images_user/avatar7.jpg";
                }

                user.Avatar = path;
                var r1 = _mapper.Map<User>(user);
                service.AddUser(r1);
                int gg = service.GetUsers().Where(m => m.Email == r1.Email).LastOrDefault().Id;

                if (user.Selectedrole != null)
                {
                    foreach (var item in user.Selectedrole)
                    {
                        service.AddRolebyUser(gg, item);
                    }
                }
                service.UpdateUser(gg, r1);
                return RedirectToAction("IndexAdmin", "User");
            }
            else return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                User user = service.GetUser(id);
                if (user != null)
                {
                    var r1 = _mapper.Map<UserViewModel>(user);
                    r1.sproles = roleService.GetRoles().ToList();
                    return View(r1);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel user, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                string path;
                if (upload != null)
                {
                    // путь к папке Files
                    path = "/images/images_user/" + upload.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        upload.CopyTo(fileStream);
                    }
                    user.Avatar = path;
                }

                var r1 = _mapper.Map<User>(user);
                r1.Avatar = user.Avatar;
                if (user.Selectedrole != null)
                {
                    foreach (var item in user.Selectedrole)
                    {
                        service.AddRolebyUser(user.Id, item);
                    }
                }
                service.UpdateUser(user.Id, r1);
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("IndexAdmin", "User");
                }
                else
                {
                    return RedirectToAction("Profile1", "User");
                }
            }
            else return View(user);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                User user = service.GetUser(id);
                if (user != null)
                {
                    service.DeleteUser(user);
                    return RedirectToAction("IndexAdmin", "User");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = service.GetUserbyEmail(model.Email);
                if (user == null)
                {
                    user = new User { Email = model.Email, Password = model.Password };
                    user.Avatar = "/images/images_user/avatar7.jpg";
                    user.FirstName = "Неизвестный";
                    user.LastName = "Неизвестный";
                    service.AddUser(user);
                    Authenticate(user); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = service.GetUserbyEmailandPassword(model.Email, model.Password);
                if (user != null)
                {
                    Authenticate(user); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        private void Authenticate(User user)
        {           
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Roles.FirstOrDefault().Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// получение списка всех пользователей
        /// </summary>
        /// <returns> список пользователей</returns>
        private IEnumerable<UserViewModel> GetUsers()
        {
            var sp = _mapper.Map<IEnumerable<UserViewModel>>(service.GetUsers());
            return sp;
        }

        /// <summary>
        /// получение списка пользователей по роли
        /// </summary>
        /// <param name="idrole">индитификатор роли</param>
        /// <returns>список пользователей</returns>
        private IEnumerable<UserViewModel> GetUsers(int idrole)
        {
            var sp = _mapper.Map<IEnumerable<UserViewModel>>(service.GetUsersbyRole(idrole));
            return sp;
        }

        /// <summary>
        /// получение пользователя по идентификатору
        /// </summary>
        /// <param name="iduser"></param>
        /// <returns>пользователь</returns>
        private UserViewModel GetUser(int iduser)
        {
            var sp = _mapper.Map<UserViewModel>(service.GetUser(iduser));
            return sp;
        }

    }
}
