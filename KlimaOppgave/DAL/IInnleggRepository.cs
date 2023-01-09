using KlimaOppgave.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlimaOppgave.DAL
{
    public interface IInnleggRepository
    {
        Task<bool> LeggInnlegg(Innlegg innlegg);

        Task<List<Innlegg>> HentInnlegg();

        Task<Innlegg> HentEnInnlegg(int id);

        Task<bool> SlettInnlegg(int id);

        Task<bool> EndreInnlegg(Innlegg innlegg);

        Task<bool> LeggSvar(Svar svar);

        Task<List<Svar>> HentSvar();

        Task<bool> EndreSvar(Svar svar);

        Task<bool> SlettSvar(int id);
    }
}