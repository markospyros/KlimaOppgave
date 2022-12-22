using System;
using System.ComponentModel.DataAnnotations;

namespace KlimaOppgave.Models
{
    public class Sporsmal
    {
        [Key]
        public int SporsmalId { get; set; }

        public DateTime Dato = DateTime.Now;

        public string Innhold { get; set; }


    }
}
