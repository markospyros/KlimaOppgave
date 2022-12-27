using System.Globalization;
using System;

namespace KlimaOppgave.Models
{
    public class Svar
    {
        public int SvarId { get; set; }

        public string Innhold { get; set;}

        public string Dato { get; set; } = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO"));

        public string InnleggId { get; set; }
        public virtual Innlegg Innlegg { get; set; }
    }
}
