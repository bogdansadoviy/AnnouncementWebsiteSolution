using AnnouncementWebsite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementWebsite.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Announcements> Announcements { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
