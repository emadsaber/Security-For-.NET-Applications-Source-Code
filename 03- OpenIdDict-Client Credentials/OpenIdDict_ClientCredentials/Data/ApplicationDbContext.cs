using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace OpenIdDict_ClientCredentials.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
