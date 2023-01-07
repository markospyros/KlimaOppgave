using KlimaOppgave.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KlimaOppgave.DAL
{
    public interface IBrukerRepository
    {
        Task<bool> LagBruker(Bruker bruker);

        Task<bool> LoggInn(Bruker bruker);
    }
}