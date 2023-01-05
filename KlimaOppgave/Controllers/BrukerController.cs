using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
        private readonly SporsmalDbContext _db;

        public BrukerController(SporsmalDbContext db)
        {
            _db = db;
        }



        /*        [HttpGet]
                public async Task<ActionResult<IEnumerable<Bruker>>> HentKunder()
                {
                    return await _db.Brukere.ToListAsync();
                }*/


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
        public async Task<ActionResult<Bruker>> LagBruker([FromBody]Bruker bruker)
        {
            var nyBruker = new Brukere();
            nyBruker.Brukernavn = bruker.Brukernavn;
            var passord = bruker.Passord;
            byte[] salt = LagSalt();
            byte[] hash = LagHash(passord, salt);
            nyBruker.Passord = hash;
            nyBruker.Salt = salt;
            _db.Brukere.Add(nyBruker);
            await _db.SaveChangesAsync();

            return Ok();
        }


        public async Task<ActionResult> LoggInn([FromBody]Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                // sjekk passordet
                if (funnetBruker == null)
                {
                    return BadRequest(false);
                }

                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return Ok(funnetBruker.BrukerId);
                }
                return BadRequest(false);
            }
            catch (Exception e)
            {
                // _log.LogInformation(e.Message);
                return BadRequest(false);
            }
        }
    }
}
