using KlimaOppgave.Controllers;
using KlimaOppgave.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace KlimaOppgave.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();

            var db = serviceScope.ServiceProvider.GetService<SporsmalDbContext>();

            // lag en påoggingsbruker
            var bruker = new Brukere();
            bruker.Brukernavn = "Admin";
            var passord = "Admin123";
            byte[] salt = BrukerController.LagSalt();
            byte[] hash = BrukerController.LagHash(passord, salt);
            bruker.Passord = hash;
            bruker.Salt = salt;

            db.Brukere.Add(bruker);

            db.SaveChanges();
        }
    }
}
