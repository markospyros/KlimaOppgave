using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlimaOppgave.Controllers
{
    [Route("/[action]")]
    public class InnleggController : ControllerBase
    {
        private readonly SporsmalDbContext _db;

        public InnleggController(SporsmalDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult<Innlegg>> LeggInnlegg([FromBody] Innlegg innlegg)
        {
            _db.Innlegg.Add(innlegg);

            await _db.SaveChangesAsync();

            return Ok(innlegg);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Innlegg>>> HentInnlegg()
        {
            return await _db.Innlegg
                .Include(x => x.Svar)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Innlegg>> HentEnInnlegg(string id)
        {
            var innlegg = await _db.Innlegg
                .Include(x => x.Svar)
                .FirstOrDefaultAsync(x => x.InnleggId == id);

            return Ok(innlegg);
        }


        [HttpPost]
        public async Task<ActionResult<Svar>> LeggSvar([FromBody] Svar svar)
        {
            _db.Svar.Add(svar);

            await _db.SaveChangesAsync();

            return Ok(svar);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Svar>>> HentSvar()
        {
            return await _db.Svar
                .Include(x => x.Innlegg)
                .ToListAsync();
        }
        //
    }
}
