using System;
using System.Collections.Generic;
using System.Globalization;

namespace KlimaOppgave.Models
{
    public class Innlegg
    {
        public int InnleggId { get; set; }

        public string Dato { get; set; } = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO"));

        public string TimeStamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

        public string Tittel { get; set; }

        public string Innhold { get; set; }

        public string Brukernavn { get; set; }

        public virtual ICollection<Svar> Svar { get; set; }
    }
}
