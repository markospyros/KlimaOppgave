using KlimaOppgave.Controllers;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace KlimaOppgave.DAL
{
    public class InnleggRepository : IInnleggRepository
    {
        private readonly SporsmalDbContext _db;

        private ILogger<InnleggRepository> _log;

        public InnleggRepository(SporsmalDbContext db, ILogger<InnleggRepository> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost]
        public async Task<bool> LeggInnlegg(Innlegg innlegg)
        {
            try
            {
                _db.Innlegg.Add(innlegg);
                await _db.SaveChangesAsync();
                return true;        
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        [HttpGet]
        public async Task<List<Innlegg>> HentInnlegg()
        {
            try
            {
                return await _db.Innlegg
                    //.Include(x => x.Bruker)
                    .Include(y => y.Svar)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Innlegg> HentEnInnlegg(string id)
        {
            try
            {
                var innlegg = await _db.Innlegg
                    //.Include(x => x.Bruker)
                    .Include(y => y.Svar)
                    .FirstOrDefaultAsync(x => x.InnleggId == id);

                return innlegg;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> SlettInnlegg(string id)
        {
            try
            {
                var innlegg = await _db.Innlegg
                .Include(x => x.Svar)
                .FirstOrDefaultAsync(x => x.InnleggId == id);

                var svar = _db.Svar.Where(x => x.InnleggId == id);

                _db.Innlegg.Remove(innlegg);
                _db.Svar.RemoveRange(svar);

                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> EndreInnlegg(Innlegg innlegg)
        {
            try
            {
                _db.Innlegg.Update(innlegg);
                await _db.SaveChangesAsync();

                return true;        
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> LeggSvar(Svar svar)
        {
            try
            {
                _db.Svar.Add(svar);

                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        [HttpGet]
        public async Task<List<Svar>> HentSvar()
        {
            try
            {
                return await _db.Svar
                    .Include(x => x.Innlegg)
                    //.Include(y => y.Bruker)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> SlettSvar(string id)
        {
            try
            {
                var svar = await _db.Svar.FindAsync(id);
                _db.Svar.Remove(svar);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}
