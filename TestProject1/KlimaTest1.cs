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
            var innlegg1 = new Innlegg { 
                InnleggId = 1
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), 
                Tittel = "Tittel1", 
                Innhold = "Innhold1",
            };

            var innlegg2 = new Innlegg
            {
                InnleggId = 2
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel2",
                Innhold = "Innhold2",
            };

            var innlegg3 = new Innlegg
            {
                InnleggId = 3
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel3",
                Innhold = "Innhold3",
            };

            var innleggListe = new List<Innlegg>();
            innleggListe.Add(innlegg1);
            innleggListe.Add(innlegg2);
            innleggListe.Add(innlegg3);

            mockRep.Setup(k => k.HentInnlegg()).ReturnsAsync(innleggListe);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.HentInnlegg() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Innlegg>>((List<Innlegg>)resultat.Value, innleggListe);
        }

    }
}