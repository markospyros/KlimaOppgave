using System.Globalization;
using System;

namespace KlimaOppgave.Models
{
    public class Svar
    {
        public string SvarId { get; set; } = Guid.NewGuid().ToString();

        public string BrukerId { get; set; }

        public virtual Brukere Bruker { get; set; }

        public string Innhold { get; set;}

        public string Dato { get; set; } = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO"));

        public string TimeStamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

        public string InnleggId { get; set; }
        public virtual Innlegg Innlegg { get; set; }
    }
}
