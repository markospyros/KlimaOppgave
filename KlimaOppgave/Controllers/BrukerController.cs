using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOppgave.Controllers
{
    [Route("/[action]")]
    public class BrukerController : ControllerBase
    {
        private IBrukerRepository _db;

        private ILogger<BrukerController> _log;

        private const string _loggetInn = "loggetInn";

        public BrukerController(IBrukerRepository db, ILogger<BrukerController> log)
        {
            _db = db;
            _log = log;

        }

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        [HttpPost]
        public async Task<ActionResult> LagBruker([FromBody]Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagBruker(bruker);
                if (!returOK)
                {
                    _log.LogInformation("Bruker kunne ikke lagres!");
                    HttpContext.Session.SetString(_loggetInn, "");
                    return BadRequest("Bruker kunne ikke lagres");
                }
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok("Bruker lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }


        public async Task<ActionResult> LoggInn([FromBody]Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker" + bruker.Brukernavn);
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }
    }
}
