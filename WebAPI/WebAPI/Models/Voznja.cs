using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Voznja
    {
        public DateTime VremePorudzbine { get; set; }
        public Lokacija LokacijaDolaskaTaksija { get; set; }
        public Automobil Automobil { get; set; }
        public Musterija Musterija { get; set; }
        public Lokacija Odrediste { get; set; }
        public Dispecer Dispecer { get; set; }
        public Vozac Vozac { get; set; }
        public double Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public StatusVoznje Status { get; set; }
    }
}