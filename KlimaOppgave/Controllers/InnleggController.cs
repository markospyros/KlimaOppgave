using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlimaOppgave.Controllers
{
    [Route("/[action]")]
    public class InnleggController : ControllerBase
    {
        private readonly SporsmalDbContext _db;

        private ILogger<InnleggController> _log;

        public InnleggController(SporsmalDbContext db, ILogger<InnleggController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost]
        public async Task<ActionResult<Innlegg>> LeggInnlegg([FromBody] Innlegg innlegg)
        {
            try
            {
                _db.Innlegg.Add(innlegg);
                await _db.SaveChangesAsync();
                return Ok(innlegg);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Innlegg>>> HentInnlegg()
        {
            try
            {
                _log.LogInformation("hei");
                return await _db.Innlegg
                    .Include(x => x.Bruker)
                    .Include(y => y.Svar)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Innlegg>> HentEnInnlegg(string id)
        {
            try
            {
                var innlegg = await _db.Innlegg
                    .Include(x => x.Bruker)
                    .Include(y => y.Svar)
                    .FirstOrDefaultAsync(x => x.InnleggId == id);

                return Ok(innlegg);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettInnlegg(string id)
        {
            var innlegg = await _db.Innlegg
                .Include(x => x.Svar)
                .FirstOrDefaultAsync(x => x.InnleggId == id);
            
            var svar = _db.Svar.Where(x => x.InnleggId == id);

            _db.Innlegg.Remove(innlegg);
            _db.Svar.RemoveRange(svar);

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Innlegg>> EndreInnlegg([FromBody] Innlegg innlegg)
        {
            _db.Innlegg.Update(innlegg);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Svar>> LeggSvar([FromBody] Svar svar)
        {
            try
            {
                _db.Svar.Add(svar);

                await _db.SaveChangesAsync();

                return Ok(svar);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Svar>>> HentSvar()
        {
            try
            {
                return await _db.Svar
                    .Include(x => x.Innlegg)
                    .Include(y => y.Bruker)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettSvar(string id)
        {
            var svar = await _db.Svar.FindAsync(id);
            _db.Svar.Remove(svar);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
