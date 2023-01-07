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
        public async Task<ActionResult<Innlegg>> LeggInnlegg([FromBody] Innlegg innlegg)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
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
                return Unauthorized();
            }
            List<Innlegg> alleInnlegg = await _db.HentInnlegg();
            return Ok(alleInnlegg);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Innlegg>> HentEnInnlegg(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                Innlegg innlegg = await _db.HentEnInnlegg(id);
                if (innlegg == null)
                {
                    _log.LogInformation("Fant ikke innlegg");
                    return NotFound("Fant ikke innlegg");
                }
                return Ok(innlegg);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettInnlegg(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            bool returOK = await _db.SlettInnlegg(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av innlegg ble ikke utført");
                return NotFound("Sletting av innlegg ble ikke utført");
            }
            return Ok("Innlegg slettet");
        }

        [HttpPost]
        public async Task<ActionResult<Innlegg>> EndreInnlegg([FromBody] Innlegg innlegg)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreInnlegg(innlegg);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av kunden kunne ikke utføres");
                }
                return Ok("Innlegg endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPost]
        public async Task<ActionResult<Svar>> LeggSvar([FromBody] Svar svar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
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
                return Unauthorized();
            }
            List<Svar> alleSvar = await _db.HentSvar();
            return Ok(alleSvar);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SlettSvar(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
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
