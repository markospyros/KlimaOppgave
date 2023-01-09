using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KlimaOppgave.Models
{
    public class Brukere
    {
        [Key]
        public int BrukerId { get; set; }
        public string Brukernavn { get; set; } 
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }
}
