using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class VoznjaController : ApiController
    {
        public List<Voznja> Get()
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];
            return voznje.voznje;
        }
        [Route("api/Voznja/Put/{id:int}")]
        public bool Put(int id,[FromBody]Voznja voznja) // izmena voznje iz Musterije
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];
            foreach (var item in voznje.voznje)
            {
                if (item.IdVoznje == id)
                {
                    item.Lokacija.X = voznja.Lokacija.X;
                    item.Lokacija.Y = voznja.Lokacija.Y;
                    item.Lokacija.Adresa.UlicaBroj = voznja.Lokacija.Adresa.UlicaBroj;
                    item.Lokacija.Adresa.NaseljenoMjesto = voznja.Lokacija.Adresa.NaseljenoMjesto;
                    item.Lokacija.Adresa.PozivniBroj = voznja.Lokacija.Adresa.PozivniBroj;
                    item.Automobil = voznja.Automobil;
                    UpisUTxt(item);
                    return true;
                }
            }
            return false;
        }

        [Route("api/Voznja/PutOtkaz/{id:int}")]
        public bool PutOtkaz(int id, [FromBody]Voznja voznja) // Kad se voznja otkazuje iz musterije
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];
           
            foreach (Voznja item in voznje.voznje)
            {
                if (item.IdVoznje == id)
                {
                    item.Komentar.Opis = voznja.Komentar.Opis;
                    item.Komentar.DatumObjave = DateTime.Now;
                    item.Komentar.KorisnickoIme = voznja.Musterija;
                    item.Komentar.IdVoznje = item.IdVoznje;
                    item.Komentar.OcenaVoznje = voznja.Komentar.OcenaVoznje;
                    item.Status = StatusVoznje.Status.OTKAZANA;
                    UpisUTxt(item);
                    return true;
                }
            }
            return false;
        }

        public void UpisUTxt(Voznja v)
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];
            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Voznje.txt";
            StringBuilder sb = new StringBuilder();
            sb.Append(v.IdVoznje + ";" + v.VremePorudzbine + ";" + v.Lokacija.X + ";" + v.Lokacija.Y + ";" + v.Lokacija.Adresa.UlicaBroj + ";" + v.Lokacija.Adresa.NaseljenoMjesto + ";" + v.Lokacija.Adresa.PozivniBroj + ";" + v.Automobil + ";" + v.Musterija + ";" + v.Odrediste.X + ";" + v.Odrediste.Y + ";" + v.Odrediste.Adresa.UlicaBroj + ";" + v.Odrediste.Adresa.NaseljenoMjesto + ";" + v.Odrediste.Adresa.PozivniBroj + ";" + v.Dispecer + ";" + v.Vozac + ";" + v.Iznos + ";" + v.Komentar.Opis + ";" + v.Komentar.DatumObjave + ";" + v.Komentar.KorisnickoIme + ";" + v.Komentar.IdVoznje + ";" + v.Komentar.OcenaVoznje + ";" + v.Status + "\n");
            string[] arrLine = File.ReadAllLines(path);
            arrLine[v.IdVoznje - 1] = sb.ToString();
            File.WriteAllLines(path, arrLine);
            File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));
            voznje = new Voznje("~/App_Data/Voznje.txt");
            HttpContext.Current.Application["voznje"] = voznje;
        }
    }
}
