using KlimaOppgave.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlimaOppgave.DAL
{
    public interface IInnleggRepository
    {
        Task<bool> LeggInnlegg(Innlegg innlegg);

        Task<List<Innlegg>> HentInnlegg();

        Task<Innlegg> HentEnInnlegg(string id);

        Task<bool> SlettInnlegg(string id);

        Task<bool> EndreInnlegg(Innlegg innlegg);

        Task<bool> LeggSvar(Svar svar);

        Task<List<Svar>> HentSvar();

        Task<bool> SlettSvar(string id);
    }
}