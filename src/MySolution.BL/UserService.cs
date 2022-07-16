using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySolution.Abstratcion;
using MySolution.BL.Interfaces;
using MySolution.DAL;
using MySolution.DAL.Entities;

namespace MySolution.BL
{
    public class UserService : IUserService
    {
        private readonly UserContext _context;
        private static bool _methodIsNotBusy = true;
        private static string _lastAddLogin;

        public UserService(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод возвращает информацию об пользователе
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Информация о пользователе</returns>
        public async Task<User> GetUserAsync(string login)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
            if (existUser != null)
            {
                return new User
                {
                    Id = existUser.Id,
                    Login = existUser.Login,
                    CreatedDate = existUser.CreatedDate,
                    UserGroupId = existUser.UserGroupId,
                    UserStateId = existUser.UserStateId,
                };
            }

            return null;
        }

        /// <summary>
        /// Метод проверяет существование пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Признак существования</returns>
        public async Task<bool> CheckExistsUser(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login)) != null;
        }

        /// <summary>
        /// Метод возвращает всех пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public IEnumerable<User> GetAllUser()
        {
            return _context.Users.Select(item => new User
            {
                Id = item.Id,
                Login = item.Login,
                CreatedDate = item.CreatedDate,
                UserGroupId = item.UserGroupId,
                UserStateId = item.UserStateId
            });
        }

        /// <summary>
        /// Метод создает нового пользователя с заданными параметрами
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Признак создания пользователя</returns>
        public async Task<bool> CreateNewUserAsync(string login, string password)
        {
            if (_methodIsNotBusy && _lastAddLogin != login)
            {
                _methodIsNotBusy = false;
                _lastAddLogin = login;
                var activeCode = await _context.UserStates.FirstOrDefaultAsync(us => us.Code.Equals("Active"));
                var userGroup = await _context.UserGroups.FirstOrDefaultAsync(ug => ug.Code.Equals("User"));
                Thread.Sleep(5000);
                try
                {
                    await _context.Users.AddAsync(new UserRecord
                    {
                        Login = login,
                        Password = password,
                        CreatedDate = DateTime.Now,
                        UserGroupId = userGroup.Id,
                        UserStateId = activeCode.Id
                    });
                    await _context.SaveChangesAsync();
                    _methodIsNotBusy = true;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> CreateNewAdminAsync()
        {
            return false;
        }

        /// <summary>
        /// Метод блокирует пользователя по логину
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Признак блокировки</returns>
        public async Task<bool> BlockUserAsync(string login)
        {
            try
            {
                var blockedCode = await _context.UserStates.FirstOrDefaultAsync(us => us.Code.Equals("Blocked"));
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
                if (user != null)
                {
                    user.UserStateId = blockedCode.Id;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
                
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Метод разблокирует пользователя по логину
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns>Признак разблокировки</returns>
        public async Task<bool> UnblockUserAsync(string login)
        {
            try
            {
                var activeCode = await _context.UserStates.FirstOrDefaultAsync(us => us.Code.Equals("Active"));
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
                if (user != null)
                {
                    user.UserStateId = activeCode.Id;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
