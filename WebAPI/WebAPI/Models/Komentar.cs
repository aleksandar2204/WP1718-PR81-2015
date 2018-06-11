using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Komentar
    {
        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public Korisnik KorisnikKojiJeOstavioKomentar { get; set; }
        public int OcenaVoznje { get; set; }
        public StatusVoznje statusVoznje { get; set; }
    }
}