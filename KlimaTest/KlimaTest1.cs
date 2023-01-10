using Castle.Core.Logging;
using KlimaOppgave.Controllers;
using KlimaOppgave.DAL;
using KlimaOppgave.Models;
using KundeAppTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class KlimaTest1
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IInnleggRepository> mockRep = new Mock<IInnleggRepository>();
        private readonly Mock<ILogger<InnleggController>> mockLog = new Mock<ILogger<InnleggController>>();

        private readonly Mock<IBrukerRepository> mockRep2 = new Mock<IBrukerRepository>();
        private readonly Mock<ILogger<BrukerController>> mockLog2 = new Mock<ILogger<BrukerController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task HentInnleggLoggetInnOk()
        {
            //Arrange
            var innlegg1 = new Innlegg
            {
                InnleggId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel1",
                Innhold = "Innhold1",
                Brukernavn="TestBruker1"
            };

            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar1",
                InnleggId = innlegg1.InnleggId,
                Innlegg= innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar2 = new Svar
            {
                SvarId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar2",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar3 = new Svar
            {
                SvarId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar3",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar1 = new List<Svar>();
            nyeSvar1.Add(svar1);
            nyeSvar1.Add(svar2);
            nyeSvar1.Add(svar3);

            innlegg1.Svar = nyeSvar1;

            var innlegg2 = new Innlegg
            {
                InnleggId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel2",
                Innhold = "Innhold2",
                Brukernavn = "TestBruker1"
            };

            var svar4 = new Svar
            {
                SvarId = 4,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar4",
                InnleggId = innlegg2.InnleggId,
                Innlegg = innlegg2,
                Brukernavn = "TestBruker1"
            };

            var svar5 = new Svar
            {
                SvarId = 5,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar5",
                InnleggId = innlegg2.InnleggId,
                Innlegg = innlegg2,
                Brukernavn = "TestBruker1"
            };

            var svar6 = new Svar
            {
                SvarId = 6,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar6",
                InnleggId = innlegg2.InnleggId,
                Innlegg = innlegg2,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar2 = new List<Svar>();
            nyeSvar2.Add(svar4);
            nyeSvar2.Add(svar5);
            nyeSvar2.Add(svar6);

            innlegg2.Svar = nyeSvar2;

            var innlegg3 = new Innlegg
            {
                InnleggId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel3",
                Innhold = "Innhold3",
                Brukernavn = "TestBruker1"
            };

            var svar7 = new Svar
            {
                SvarId = 7,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar7",
                InnleggId = innlegg3.InnleggId,
                Innlegg = innlegg3,
                Brukernavn = "TestBruker1"
            };

            var svar8 = new Svar
            {
                SvarId = 8,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar8",
                InnleggId = innlegg3.InnleggId,
                Innlegg = innlegg3,
                Brukernavn = "TestBruker1"
            };

            var svar9 = new Svar
            {
                SvarId = 9,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar9",
                InnleggId = innlegg3.InnleggId,
                Innlegg = innlegg3,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar3 = new List<Svar>();
            nyeSvar3.Add(svar7);
            nyeSvar3.Add(svar8);
            nyeSvar3.Add(svar9);

            innlegg3.Svar = nyeSvar3;

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

        [Fact]
        public async Task HentInnleggIkkeLoggetInn()
        {

            mockRep.Setup(k => k.HentInnlegg()).ReturnsAsync(It.IsAny<List<Innlegg>>());

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.HentInnlegg() as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LeggInnleggLoggetInnOK()
        {
            mockRep.Setup(k => k.LeggInnlegg(It.IsAny<Innlegg>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggInnlegg(It.IsAny<Innlegg>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Innlegg lagret", resultat.Value);
        }

        [Fact]
        public async Task LeggInnleggLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.LeggInnlegg(It.IsAny<Innlegg>())).ReturnsAsync(false);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggInnlegg(It.IsAny<Innlegg>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Innlegg kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LeggInnleggLoggetInnFeilModel()
        {
            var innlegg1 = new Innlegg
            {
                InnleggId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "",
                Innhold = "Innhold1",
                Brukernavn = "TestBruker1"
            };

            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar1",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar2 = new Svar
            {
                SvarId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar2",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar3 = new Svar
            {
                SvarId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar3",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar1 = new List<Svar>();
            nyeSvar1.Add(svar1);
            nyeSvar1.Add(svar2);
            nyeSvar1.Add(svar3);

            innlegg1.Svar = nyeSvar1;

            mockRep.Setup(k => k.LeggInnlegg(innlegg1)).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            innleggController.ModelState.AddModelError("Tittel", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggInnlegg(innlegg1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LeggInnleggIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LeggInnlegg(It.IsAny<Innlegg>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggInnlegg(It.IsAny<Innlegg>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettInnleggLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.SlettInnlegg(It.IsAny<int>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.SlettInnlegg(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Innlegg slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettInnleggLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.SlettInnlegg(It.IsAny<int>())).ReturnsAsync(false);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.SlettInnlegg(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av Innlegg ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SlettInnleggIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettInnlegg(It.IsAny<int>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.SlettInnlegg(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentEnInnleggLoggetInnOK()
        {
            // Arrange
            var innlegg1 = new Innlegg
            {
                InnleggId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "",
                Innhold = "Innhold1",
                Brukernavn = "TestBruker1"
            };

            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar1",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar2 = new Svar
            {
                SvarId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar2",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar3 = new Svar
            {
                SvarId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar3",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar1 = new List<Svar>();
            nyeSvar1.Add(svar1);
            nyeSvar1.Add(svar2);
            nyeSvar1.Add(svar3);

            innlegg1.Svar = nyeSvar1;

            mockRep.Setup(k => k.HentEnInnlegg(It.IsAny<int>())).ReturnsAsync(innlegg1);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.HentEnInnlegg(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Innlegg>(innlegg1, (Innlegg)resultat.Value);
        }

        [Fact]
        public async Task HentEnInnleggLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.HentEnInnlegg(It.IsAny<int>())).ReturnsAsync(() => null); // merk denne null setting!

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.HentEnInnlegg(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke Innlegg", resultat.Value);
        }

        [Fact]
        public async Task HentEnInnleggIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentEnInnlegg(It.IsAny<int>())).ReturnsAsync(() => null);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.HentEnInnlegg(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreInnleggLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.EndreInnlegg(It.IsAny<Innlegg>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreInnlegg(It.IsAny<Innlegg>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Innlegg endret", resultat.Value);
        }

        [Fact]
        public async Task EndreInnleggLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.EndreInnlegg(It.IsAny<Innlegg>())).ReturnsAsync(false);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreInnlegg(It.IsAny<Innlegg>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av innlegg kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreInnleggLoggetInnFeilModel()
        {
            // Arrange
            var innlegg1 = new Innlegg
            {
                InnleggId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "",
                Innhold = "Innhold1",
                Brukernavn = "TestBruker1"
            };

            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar1",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar2 = new Svar
            {
                SvarId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar2",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar3 = new Svar
            {
                SvarId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar3",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar1 = new List<Svar>();
            nyeSvar1.Add(svar1);
            nyeSvar1.Add(svar2);
            nyeSvar1.Add(svar3);

            innlegg1.Svar = nyeSvar1;

            mockRep.Setup(k => k.EndreInnlegg(innlegg1)).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            innleggController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreInnlegg(innlegg1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreInnleggIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndreInnlegg(It.IsAny<Innlegg>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreInnlegg(It.IsAny<Innlegg>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentSvarLoggetInnOk()
        {
            //Arrange
            var innlegg1 = new Innlegg
            {
                InnleggId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel1",
                Innhold = "Innhold1",
                Brukernavn = "TestBruker1"
            };

            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar1",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar2 = new Svar
            {
                SvarId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar2",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var svar3 = new Svar
            {
                SvarId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar3",
                InnleggId = innlegg1.InnleggId,
                Innlegg = innlegg1,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar1 = new List<Svar>();
            nyeSvar1.Add(svar1);
            nyeSvar1.Add(svar2);
            nyeSvar1.Add(svar3);

            innlegg1.Svar = nyeSvar1;

            var innlegg2 = new Innlegg
            {
                InnleggId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel2",
                Innhold = "Innhold2",
                Brukernavn = "TestBruker1"
            };

            var svar4 = new Svar
            {
                SvarId = 4,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar4",
                InnleggId = innlegg2.InnleggId,
                Innlegg = innlegg2,
                Brukernavn = "TestBruker1"
            };

            var svar5 = new Svar
            {
                SvarId = 5,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar5",
                InnleggId = innlegg2.InnleggId,
                Innlegg = innlegg2,
                Brukernavn = "TestBruker1"
            };

            var svar6 = new Svar
            {
                SvarId = 6,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar6",
                InnleggId = innlegg2.InnleggId,
                Innlegg = innlegg2,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar2 = new List<Svar>();
            nyeSvar2.Add(svar4);
            nyeSvar2.Add(svar5);
            nyeSvar2.Add(svar6);

            innlegg2.Svar = nyeSvar2;

            var innlegg3 = new Innlegg
            {
                InnleggId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel3",
                Innhold = "Innhold3",
                Brukernavn = "TestBruker1"
            };

            var svar7 = new Svar
            {
                SvarId = 7,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar7",
                InnleggId = innlegg3.InnleggId,
                Innlegg = innlegg3,
                Brukernavn = "TestBruker1"
            };

            var svar8 = new Svar
            {
                SvarId = 8,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar8",
                InnleggId = innlegg3.InnleggId,
                Innlegg = innlegg3,
                Brukernavn = "TestBruker1"
            };

            var svar9 = new Svar
            {
                SvarId = 9,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar9",
                InnleggId = innlegg3.InnleggId,
                Innlegg = innlegg3,
                Brukernavn = "TestBruker1"
            };

            var nyeSvar3 = new List<Svar>();
            nyeSvar3.Add(svar7);
            nyeSvar3.Add(svar8);
            nyeSvar3.Add(svar9);

            innlegg3.Svar = nyeSvar3;

            var svarListe = new List<Svar>();
            svarListe.Add(svar1);
            svarListe.Add(svar2);
            svarListe.Add(svar3);
            svarListe.Add(svar4);
            svarListe.Add(svar5);
            svarListe.Add(svar6);
            svarListe.Add(svar7);
            svarListe.Add(svar8);
            svarListe.Add(svar9);

            mockRep.Setup(k => k.HentSvar()).ReturnsAsync(svarListe);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.HentSvar() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Svar>>((List<Svar>)resultat.Value, svarListe);
        }

        [Fact]
        public async Task HentSvarIkkeLoggetInn()
        {

            mockRep.Setup(k => k.HentSvar()).ReturnsAsync(It.IsAny<List<Svar>>());

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.HentSvar() as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LeggSvarLoggetInnOK()
        {
            mockRep.Setup(k => k.LeggSvar(It.IsAny<Svar>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggSvar(It.IsAny<Svar>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(resultat.Value, resultat.Value);
        }

        [Fact]
        public async Task LeggSvarLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.LeggSvar(It.IsAny<Svar>())).ReturnsAsync(false);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggSvar(It.IsAny<Svar>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Svar kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LeggSvarLoggetInnFeilModel()
        {

            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "",
                InnleggId = 1
            };

            mockRep.Setup(k => k.LeggSvar(svar1)).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            innleggController.ModelState.AddModelError("Innhold", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggSvar(svar1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LeggSvarIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LeggSvar(It.IsAny<Svar>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.LeggSvar(It.IsAny<Svar>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreSvarLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.EndreSvar(It.IsAny<Svar>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreSvar(It.IsAny<Svar>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Svar endret", resultat.Value);
        }

        [Fact]
        public async Task EndreSvarLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.EndreSvar(It.IsAny<Svar>())).ReturnsAsync(false);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreSvar(It.IsAny<Svar>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av svar kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreSvarLoggetInnFeilModel()
        {
            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "",
                InnleggId = 1
            };

            mockRep.Setup(k => k.EndreSvar(svar1)).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            innleggController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreSvar(svar1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreSvarIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndreSvar(It.IsAny<Svar>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.EndreSvar(It.IsAny<Svar>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettSvarLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.SlettSvar(It.IsAny<int>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.SlettSvar(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Svar slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettSvarLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.SlettSvar(It.IsAny<int>())).ReturnsAsync(false);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.SlettSvar(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av svar ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SlettSvarIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettSvar(It.IsAny<int>())).ReturnsAsync(true);

            var innleggController = new InnleggController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            innleggController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await innleggController.SlettSvar(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LoggInnOK()
        {
            mockRep2.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var bruker = new Bruker { Brukernavn = "Test", Passord = "Test123" };

            // Act
            var resultat = await brukerController.LoggInn(bruker) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            mockRep2.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnInputFeil()
        {
            mockRep2.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            brukerController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagBrukerOK()
        {
            mockRep2.Setup(k => k.LagBruker(It.IsAny<Bruker>())).ReturnsAsync(0);

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            var bruker = new Bruker { Brukernavn = "Test", Passord = "Test123"};

            // Act
            var resultat = await brukerController.LagBruker(bruker) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bruker lagret", resultat.Value);
        }

        [Fact]
        public async Task LagBrukerTattBruker()
        {
            mockRep2.Setup(k => k.LagBruker(It.IsAny<Bruker>())).ReturnsAsync(1);

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            var bruker = new Bruker { Brukernavn = "Test", Passord = "Test123" };

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await brukerController.LagBruker(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Dette brukernavnet er allerede i bruk.", resultat.Value);
        }

        [Fact]
        public async Task LagBrukerIkkeOK()
        {
            mockRep2.Setup(k => k.LagBruker(It.IsAny<Bruker>())).ReturnsAsync(2);

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            var bruker = new Bruker { Brukernavn = "Test", Passord = "Test123" };

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await brukerController.LagBruker(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Bruker kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagBrukerInputFeil()
        {
            mockRep2.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            brukerController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await brukerController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }


        [Fact]
        public void GetSessionDataReturnSessionData()
        {
            // Arrange
            
            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;


            // Act
            var result = brukerController.GetSessionData() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(result.Value, result.Value);
        }

        [Fact]
        public void GetSessionDataSessionReturnNull()
        {
            // Arrange

            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            mockSession[_loggetInn] = null;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var result = brukerController.GetSessionData() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Null(result.Value);
        }

        [Fact]
        public void LoggUt()
        {
            var brukerController = new BrukerController(mockRep2.Object, mockLog2.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            brukerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            brukerController.LoggUt();

            // Assert
            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }
    }
}