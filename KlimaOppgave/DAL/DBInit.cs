using KlimaOppgave.Controllers;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Reflection;

namespace KlimaOppgave.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();

            var db = serviceScope.ServiceProvider.GetService<SporsmalDbContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var innlegg1 = new Innlegg
            {
                InnleggId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Tittel = "Tittel1",
                Innhold = "Innhold1"
            };

            var svar1 = new Svar
            {
                SvarId = 1,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar1",
                InnleggId = innlegg1.InnleggId
            };

            var svar2 = new Svar
            {
                SvarId = 2,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar2",
                InnleggId = innlegg1.InnleggId
            };

            var svar3 = new Svar
            {
                SvarId = 3,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar3",
                InnleggId = innlegg1.InnleggId
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
                Innhold = "Innhold2"
            };

            var svar4 = new Svar
            {
                SvarId = 4,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar4",
                InnleggId = innlegg2.InnleggId
            };

            var svar5 = new Svar
            {
                SvarId = 5,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar5",
                InnleggId = innlegg2.InnleggId
            };

            var svar6 = new Svar
            {
                SvarId = 6,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar6",
                InnleggId = innlegg2.InnleggId
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
                Innhold = "Innhold3"
            };

            var svar7 = new Svar
            {
                SvarId = 7,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar7",
                InnleggId = innlegg3.InnleggId
            };

            var svar8 = new Svar
            {
                SvarId = 8,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar8",
                InnleggId = innlegg3.InnleggId
            };

            var svar9 = new Svar
            {
                SvarId = 9,
                Dato = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO")),
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                Innhold = "Svar9",
                InnleggId = innlegg3.InnleggId
            };

            var nyeSvar3 = new List<Svar>();
            nyeSvar3.Add(svar7);
            nyeSvar3.Add(svar8);
            nyeSvar3.Add(svar9);

            innlegg3.Svar = nyeSvar3;

            db.Innlegg.Add(innlegg1);
            db.Innlegg.Add(innlegg2);
            db.Innlegg.Add(innlegg3);

            // lag en påoggingsbruker
            var bruker = new Brukere();
            bruker.Brukernavn = "Admin";
            var passord = "Test11";
            byte[] salt = BrukerController.LagSalt();
            byte[] hash = BrukerController.LagHash(passord, salt);
            bruker.Passord = hash;
            bruker.Salt = salt;
            db.Brukere.Add(bruker);

            db.SaveChanges();
        }
    }
}
