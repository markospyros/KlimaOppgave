using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOppgave.Controllers
{
    [Route("/[action]")]
    public class InnleggController : ControllerBase
    {
        private IInnleggRepository _db;

        private ILogger<InnleggController> _log;

        private const string _loggetInn = "loggetInn";

        public InnleggController(IInnleggRepository db, ILogger<InnleggController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost]
        public async Task<ActionResult> LeggInnlegg([FromBody] Innlegg innlegg)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LeggInnlegg(innlegg);
                if (!returOK)
                {
                    _log.LogInformation("Innlegg kunne ikke lagres!");
                    return BadRequest("Innlegg kunne ikke lagres");
                }
                return Ok("Innlegg lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpGet]
        public async Task<ActionResult> HentInnlegg()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Innlegg> alleInnlegg = await _db.HentInnlegg();
            return Ok(alleInnlegg);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> HentEnInnlegg(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                Innlegg innlegg = await _db.HentEnInnlegg(id);
                if (innlegg == null)
                {
                    _log.LogInformation("Fant ikke Innlegg");
                    return NotFound("Fant ikke Innlegg");
                }
                return Ok(innlegg);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettInnlegg(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettInnlegg(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av Innlegg ble ikke utført");
                return NotFound("Sletting av Innlegg ble ikke utført");
            }
            return Ok("Innlegg slettet");
        }

        [HttpPost]
        public async Task<ActionResult> EndreInnlegg([FromBody] Innlegg innlegg)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreInnlegg(innlegg);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av innlegg kunne ikke utføres");
                }
                return Ok("Innlegg endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPost]
        public async Task<ActionResult> LeggSvar([FromBody] Svar svar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LeggSvar(svar);
                if (!returOK)
                {
                    _log.LogInformation("Svar kunne ikke lagres!");
                    return BadRequest("Svar kunne ikke lagres");
                }
                return Ok(svar);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpGet]
        public async Task<ActionResult> HentSvar()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Svar> alleSvar = await _db.HentSvar();
            return Ok(alleSvar);
        }

        [HttpPost]
        public async Task<ActionResult> EndreSvar([FromBody] Svar svar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreSvar(svar);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av svar kunne ikke utføres");
                }
                return Ok("Svar endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettSvar(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettSvar(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av svar ble ikke utført");
                return NotFound("Sletting av svar ble ikke utført");
            }
            return Ok("Svar slettet");
        }
    }
}
