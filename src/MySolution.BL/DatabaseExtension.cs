using MySolution.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MySolution.BL
{
    public static class DatabaseExtensions
    {
        public static void AddDbExtension (this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<UserContext>(options=>options.UseSqlite(connectionString));
        }
    }
}