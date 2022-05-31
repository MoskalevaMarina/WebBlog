using API.Models.Roles;
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
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RoleService rs;
        private IMapper _mapper;

        public RolesController(RoleService service, IMapper mapper)
        {
            rs = service;
            _mapper = mapper;
        }


        /// <summary>
        /// Просмотр списка всех ролей
        /// </summary>
        [HttpGet]
        // [Route("")]
        public IEnumerable<ViewRole> GetAllRoles()
        {
            var roles = _mapper.Map<IEnumerable<ViewRole>>(rs.GetRoles());
            return roles;
        }

        /// <summary>
        /// Просмотр роли по Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ViewRole> GetRoleById([FromRoute] int id)
        {
            var role = _mapper.Map<ViewRole>(rs.GetRole(id));
            if (role == null)
                return StatusCode(400, $"Ошибка: Роль с идентификатором {id} не существует.");
            return role;
        }

        /// <summary>
        /// Добавление роли
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddRole([FromBody] AddRoleRequest request)
        {
            var existingRole = rs.GetRole(request.Name);
            if (existingRole == null)
            {
                var newRole = _mapper.Map<AddRoleRequest, Role>(request);
                rs.AddRole(newRole);
                return StatusCode(201, $"Роль {request.Name} добавлена!");
            }
            return StatusCode(409, $"Ошибка: Роль {request.Name} уже существует.");
        }

        /// <summary>
        /// Обновление существующей роли
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditRoleById(
            [FromRoute] int id,
            [FromBody] EditRoleRequest request)
        {
            var role = rs.GetRole(id);
            if (role == null)
                return StatusCode(400, $"Ошибка: Роль с идентификатором {id} не существует.");
            else
            {
                var newRole = _mapper.Map<EditRoleRequest, Role>(request);
                rs.UpdateRole(id, newRole);
                return StatusCode(200, $"Роль обновлена! Имя - {role.Name}, Описание - {role.Description} ");

            }

        }
        /// <summary>
        /// Удаление роли
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var role = rs.GetRole(id);
            if (role == null)
                return StatusCode(400, $"Ошибка: Роль с идентификатором {id} не существует.");
            rs.DeleteRole(role);

            return StatusCode(200, $"Роль удалена! Имя - {role.Name}");
        }

    }
}
