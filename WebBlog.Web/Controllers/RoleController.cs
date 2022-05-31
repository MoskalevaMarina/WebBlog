using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.BLL.Services;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;
using WebBlog.DAL.Interfaces;
using WebBlog.DAL.Repositories;
using WebBlog.Web.Models;

namespace WebBlog.Web.Controllers
{
    public class RoleController : Controller
    {
        private RoleService rs;
        private IMapper _mapper;

        public RoleController( RoleService rs1, IMapper mapper)
        {
            rs = rs1;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sp = _mapper.Map<IEnumerable<RoleViewModel>>(rs.GetRoles());
            return View(sp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var r1 = _mapper.Map<Role>(role);
                rs.AddRole(r1);
                return RedirectToAction("Index", "Role");
            }
            else return View(role);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Role role = rs.GetRole(id);
                if (role != null)
                {
                    var r1 = _mapper.Map<RoleViewModel>(role);
                    return View(r1);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                var r1 = _mapper.Map<Role>(role);
                rs.UpdateRole(id, r1);
                return RedirectToAction("Index", "Role");
            }
            else return View(role);
           
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                Role role = rs.GetRole(id);  
                if (role != null)
                {
                    rs.DeleteRole(role);
                    return RedirectToAction("Index", "Role");
                }
            }
            return NotFound();
        }

    }
}
