using DotNetTaskAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetTaskAPI.Models
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
