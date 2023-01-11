using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using KlimaOppgave.Models;

namespace KlimaOppgave.DAL
{
    public class BrukerRepository : IBrukerRepository
    {
        private SporsmalDbContext _db;

        private ILogger<BrukerRepository> _log;

        public BrukerRepository(SporsmalDbContext db, ILogger<BrukerRepository> log)
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
        public async Task<int> LagBruker(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);

                if (funnetBruker == null)
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

                    return 0;
                }

                return 1;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return 2;
            }
        }


        public async Task<int> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                // sjekk passordet
                if (funnetBruker == null)
                {
                    return 1;
                }

                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return 0;
                }
                return 2;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return 3;
            }
        }
    }
}
