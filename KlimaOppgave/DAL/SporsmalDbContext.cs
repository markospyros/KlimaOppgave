using KlimaOppgave.Models;
using Microsoft.EntityFrameworkCore;

namespace KlimaOppgave.DAL
{
    public class SporsmalDbContext : DbContext
    {
        public SporsmalDbContext(DbContextOptions<SporsmalDbContext> options) : base(options)
        {
        }

        public DbSet<Sporsmal> Sporsmal { get; set; }
    }
}
