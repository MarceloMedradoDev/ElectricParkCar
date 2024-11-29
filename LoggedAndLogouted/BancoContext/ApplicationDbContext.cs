using LoggedAndLogouted.Models;
using Microsoft.EntityFrameworkCore;

namespace LoggedAndLogouted.BancoContext
{
    public class ApplicationDbContext : DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<LoginModel> Usuarios { get; set; }
    }
}
