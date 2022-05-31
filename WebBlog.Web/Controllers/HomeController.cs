using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.Web.Models;
using WebBlog.DAL.EF;
using System.Net;
using Newtonsoft.Json;

namespace WebBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("user"))
            {
                return RedirectToAction("Index", "User");
            }
            else
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("IndexAdmin", "User");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;

            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]:  Сатусный код: " + statusCode.ToString();

            if (statusCode == 403)
            {
                logMessage += "  Запрещено";
            }
            else if (statusCode == 404)
            {
                logMessage += "  Не найден";
            }
            else if (statusCode == 500)
            {
                logMessage += "  Внутренняя ошибка сервера";
            }

            Program.Logger.Error(logMessage);
            return View("ErrorSomethingWentWrong");
        }
    }
}
