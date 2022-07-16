using System.Collections.Generic;
using System.Threading.Tasks;
using MySolution.Abstratcion;

namespace MySolution.BL.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string login);

        Task<bool> CheckExistsUser(string login);

        IEnumerable<User> GetAllUser();

        Task<bool> CreateNewUserAsync(string login, string password);

        Task<bool> CreateNewAdminAsync();

        Task<bool> BlockUserAsync(string login);

        Task<bool> UnblockUserAsync(string login);
    }
}