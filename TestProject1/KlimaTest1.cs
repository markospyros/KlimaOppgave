using Castle.Core.Logging;
using KlimaOppgave.Controllers;
using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Globalization;
using Xunit;

namespace TestProject1
{
    public class KlimaTest1
    {
        private const string _loggetInn = "loggetInn";

        private readonly Mock<IInnleggRepository> mockRep = new Mock<IInnleggRepository>();
        private readonly Mock<ILogger<InnleggController>> mockLog = new Mock<ILogger<InnleggController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task HentInnleggLoggetInnOk()
        {
            //Arrange
            var inlegg1 = new Innlegg { InnleggId = "1", Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
            TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), Tittel = "Tittel1", Innhold = "Innhold1", 
            Svar = {}
            };
        }

    }
}