using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySolution.BL.Interfaces;
using MySolution.Web.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace MySolution.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IUserService service;

        public MainController(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Предоставляет информацию об одном пользователе
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet("{login}")]
        [SwaggerResponse(200, "Вернулась сущность пользователя", typeof(User))]
        [SwaggerResponse(404, "Пользователь не был найден")]
        public async Task<ActionResult<User>> GetUserAsync(string login)
        {
            var user = await service.GetUserAsync(login);
            if (user != null)
            {
                return new User
                {
                    Id = user.Id,
                    Login = user.Login,
                    CreatedDate = user.CreatedDate,
                    UserGroupId = user.UserGroupId,
                    UserStateId = user.UserStateId,
                };
            }

            return NotFound();
        }

        /// <summary>
        /// Предоставляет информацию о всех пользователях
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(200, "Список пользователей было прочитаны. Может вернуть пустой массив")]
        public IEnumerable<User> GetAllUsers()
        {
            return service.GetAllUser()
                .Select(item => new User
                {
                    Id = item.Id,
                    Login = item.Login,
                    CreatedDate = item.CreatedDate,
                    UserGroupId = item.UserGroupId,
                    UserStateId = item.UserStateId,
                });
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(204, "Был создан новый пользователь")]
        [SwaggerResponse(500, "Внутреняя ошибка сервера")]
        public async Task<IActionResult> CreateNewUserAsync([FromBody] NewUser newUser)
        {
            if (ModelState.IsValid && !await service.CheckExistsUser(newUser.Login))
            {
                if (await service.CreateNewUserAsync(newUser.Login, newUser.Password))
                {
                    return StatusCode(204);
                }
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Производит блокирование пользователя в системе
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        [HttpPatch("block/{login}")]
        [SwaggerResponse(204, "Пользователь был заблокирован")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task<IActionResult> BlockUserAsync(string login)
        {
            if (await service.CheckExistsUser(login) && await service.BlockUserAsync(login))
            {
                return StatusCode(204);
            }

            return StatusCode(500);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPatch("unlock/{login}")]
        [SwaggerResponse(204, "Пользователь был разблокирован")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task<IActionResult> UnblockUser(string login)
        {
            if (await service.UnblockUserAsync(login))
            {
                return StatusCode(204);
            }

            return StatusCode(500);
        }
    }
}