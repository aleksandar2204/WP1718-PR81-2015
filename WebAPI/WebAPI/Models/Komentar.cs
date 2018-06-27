using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Komentar
    {
        public Komentar() { }
        public Komentar(string Opis, string datumObjave, string KorisnickoIme, string IdVoznje, string Ocena)
        {
            this.Opis = Opis;
            if (datumObjave.Equals(""))
            {
                DatumObjave = null;
            }
            else
            { 
                this.DatumObjave = DateTime.Parse(datumObjave);
            }
            
            this.KorisnickoIme = KorisnickoIme;
            try
            {
                this.IdVoznje = Int32.Parse(IdVoznje);
            }
            catch
            {
                this.IdVoznje = 0;
            }
            OcenaVoznje = Int32.Parse(Ocena);
        }
        public string Opis { get; set; }
        public DateTime? DatumObjave { get; set; }
        public string KorisnickoIme { get; set; }
        public int OcenaVoznje { get; set; }
        public int IdVoznje { get; set; }
    }
}