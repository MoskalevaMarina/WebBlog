﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.BLL.Services;
using WebBlog.DAL.EF;
using WebBlog.DAL.Entities;
using WebBlog.Web.Models;

namespace WebBlog.Web.Controllers
{
    public class TagController : Controller
    {
        public TagService rs;
        private IMapper _mapper;
        private UserService us;

        public TagController(TagService rs1, IMapper mapper, UserService us1)
        {
            rs = rs1;
            _mapper = mapper;
            us = us1;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (User.IsInRole("user"))
            {

                User user = us.GetUserbyEmail(User.Identity.Name);
                RedirectToAction("IndexUserTag", user.Id);
            }
            var sp = rs.GetTagAlll();

            var k = _mapper.Map<IEnumerable<TagViewModel>>(sp);
            return View(k);

        }

        [HttpGet]
        public IActionResult IndexUserTag(int id)
        {
            var u1 = us.GetUser(id);
            var k = _mapper.Map<IEnumerable<TagViewModel>>(rs.GetTagbyUser(u1));
            return View(k);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TagViewModel com)
        {
            var r1 = _mapper.Map<Tag>(com);
           
            User user = us.GetUserbyEmail(User.Identity.Name);

            r1.UserId = user.Id;

            rs.AddTag(r1);
            return RedirectToAction("Index", "Tag");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Tag com = rs.GetTag(id);
                if (com != null)
                {
                    var r1 = _mapper.Map<TagViewModel>(com);
                    return View(r1);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TagViewModel com)
        {
            var r1 = _mapper.Map<Tag>(com);
            rs.UpdateTag(id, r1);
            return RedirectToAction("Index", "Tag");

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                Tag com = rs.GetTag(id);
                if (com != null)
                {
                    rs.DeleteTag(com);
                    return RedirectToAction("Index", "Tag");
                }
            }
            return NotFound();
        }
    }
}