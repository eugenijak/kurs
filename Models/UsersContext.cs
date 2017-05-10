using Microsoft.EntityFrameworkCore;

namespace Kurs.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UsersGPX> Gpx { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }
    }
}
