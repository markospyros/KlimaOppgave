using System;
using System.Collections.Generic;
using System.Globalization;

namespace KlimaOppgave.Models
{
    public class Innlegg
    {
        public string InnleggId { get; set; } = Guid.NewGuid().ToString();

        public string Dato { get; set; } = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm", CultureInfo.CreateSpecificCulture("nb-NO"));

        public string Innhold { get; set; }

        public virtual ICollection<Svar> Svar { get; set; }
    }
}
