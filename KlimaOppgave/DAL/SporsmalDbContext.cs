using KlimaOppgave.Models;
using Microsoft.EntityFrameworkCore;

namespace KlimaOppgave.DAL
{
    public class SporsmalDbContext : DbContext
    {
        public SporsmalDbContext(DbContextOptions<SporsmalDbContext> options) : base(options)
        {
        }

        public DbSet<Innlegg> Innlegg { get; set; }
        public DbSet<Svar> Svar { get; set; }
    }
}
