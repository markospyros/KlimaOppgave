using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            catch(Exception e)
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
                    .Include(x => x.Svar)
                    .ToListAsync();
            }
            catch(Exception e)
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
                    .Include(x => x.Svar)
                    .FirstOrDefaultAsync(x => x.InnleggId == id);

                return Ok(innlegg);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
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
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
