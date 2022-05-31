using API.Models.Users;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlog.BLL.Services;
using WebBlog.DAL.Entities;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private RoleService roleService;
        private UserService userService;
        private IMapper _mapper;

        public UsersController(RoleService service, UserService service1, IMapper mapper)
        {
            roleService = service;
            userService = service1;
            _mapper = mapper;
        }

        /// <summary>
        /// Просмотр списка всех пользователей
        /// </summary>
        [HttpGet]
        [Route("")]
        public IEnumerable<ViewUser> GetAllUsers()
        {
            var users = _mapper.Map<IEnumerable<ViewUser>>(userService.GetUsers());
            return users;
        }

        /// <summary>
        /// Просмотр пользователя по Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ViewUser> GetUserById([FromRoute] int id)
        {
            var user = _mapper.Map<ViewUser>(userService.GetUser(id));
            if (user == null)
                return StatusCode(400, $"Ошибка: Пользователя с идентификатором {id} не существует.");
            return user;
        }

        /// <summary>
        /// Просмотр  пользователем своего профиля
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ViewUserProfile> GetUserProfileById([FromRoute] int id)
        {
            var user = _mapper.Map<ViewUserProfile>(userService.GetUser(id));
            if (user == null)
                return StatusCode(400, $"Ошибка: Пользователя с идентификатором {id} не существует.");
            return user;
        }

        /// <summary>
        /// Добавление пользователя админом (все поля)
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddUser([FromBody] AddUserRequest request)
        {
            var user = userService.GetUserbyEmail(request.Email);
            if (user == null)
            {
                var newuser = _mapper.Map<AddUserRequest, User>(request);
                userService.AddUser(newuser);
                return StatusCode(201, $"Пользователь {request.UserName} добавлен!");
            }

            return StatusCode(409, $"Ошибка: Пользователь {request.Email} уже существует.");
        }

        /// <summary>
        /// Регистрация пользователя 
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
        {
            var user = userService.GetUserbyEmail(request.Email);
            if (user == null)
            {
                var newuser = _mapper.Map<RegisterUserRequest, User>(request);
                newuser.Avatar = "/images/images_user/avatar7.jpg";
                newuser.FirstName = "Неизвестный";
                newuser.LastName = "Неизвестный";
                userService.AddUser(newuser);

                return StatusCode(201, $"Пользователь {request.UserName} зарегистрирован!");
            }

            return StatusCode(409, $"Ошибка: Пользователь {request.Email} уже существует.");
        }

        /// <summary>
        /// Обновление существующего пользователя админом
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditUserByIdAdmin(
            [FromRoute] int id,
            [FromBody] EditUserRequest request)
        {
            var user = userService.GetUser(id);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя с таким индетификатором {id} нет.");

            var newuser = _mapper.Map<EditUserRequest, User>(request);
            userService.UpdateUser(id, newuser);
            return StatusCode(200, $"Пользователь обновлен! Никнейм - {newuser.UserName}");
        }

        /// <summary>
        /// Обновление профиля пользователя самим пользователем
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditProfileByIdUser(
            [FromRoute] int id,
            [FromBody] EditUserProfileRequest request)
        {
            var user = userService.GetUser(id);
            if (user == null)
                return StatusCode(400, $"Ошибка:Пользователя с таким индетификатором {id} нет.");

            var newuser = _mapper.Map<EditUserProfileRequest, User>(request);
            userService.UpdateUser(id, newuser);
            return StatusCode(200, $"Пользователь обновлен! Никнейм - {newuser.UserName}");
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var user = userService.GetUser(id);
            if (user == null)
                return StatusCode(400, $"Ошибка: Пользователь с идентификатором {id} не существует.");
            userService.DeleteUser(user);
            return StatusCode(200, $"Пользователь удален! НикНейм - {user.UserName}");
        }

        /// <summary>
        /// Добавление роли пользователю
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddRoleInUser([FromBody] AddRoleinUser request)
        {
            var role = roleService.GetRole(request.IdRole);
            if (role == null)
                return StatusCode(400, $"Ошибка: Роль с идентификатором {request.IdRole} не существует.");

            var user = userService.GetUser(request.IdUser);
            if (user == null)
                return StatusCode(400, $"Ошибка: Пользователь с идентификатором {request.IdUser} не существует.");

            userService.AddRolebyUser(request.IdUser, request.IdRole);
            return StatusCode(201, $"Роль к пользователю добавлена !");
        }

    }
}
