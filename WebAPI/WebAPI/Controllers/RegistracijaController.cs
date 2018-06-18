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
    public class RegistracijaController : ApiController
    {
        public bool Post([FromBody]Korisnik korisnik)
        {
            Korisnici users = (Korisnici)HttpContext.Current.Application["korisnici"];
            foreach (var item in users.korisnici)
            {
                if(item.KorisnickoIme == korisnik.KorisnickoIme)
                {
                    return false;
                }
            }

            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Korisnici.txt";
            if (users.korisnici.Count == 0)
                korisnik.Id = 1;
            else
                korisnik.Id = users.korisnici.Count;

            users.korisnici.Add(korisnik);
            StringBuilder sb = new StringBuilder();
            sb.Append(korisnik.Id + ";" + korisnik.KorisnickoIme + ";" + korisnik.Lozinka + ";" + korisnik.Ime + ";" + korisnik.Prezime + ";" + korisnik.Pol + ";" + korisnik.JMBG + ";" + korisnik.Telefon + ";" + korisnik.Email + ";" + korisnik.Uloga + ";" + korisnik.Voznja + "\n");
            if(!File.Exists(path))
            {
                File.WriteAllText(path, sb.ToString());
            }
            else
            {
                File.AppendAllText(path, sb.ToString());
            }

            users = new Korisnici(@"~/App_Data/Korisnici.txt");
            HttpContext.Current.Application["korisnici"] = users;

            return true;
        }

        [Route("api/Registration/Post")]
        public bool Post([FromBody]Voznja voznja)
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];

            foreach (var item in voznje.voznje) // Nece se dodati za istu Musteriju voznja koja je kreirana i na cekanju
            {                                   // jer klijent moze imati samo jednu voznju koja je na cekanju
                if(item.Status == StatusVoznje.Status.KREIRANA_NA_CEKANJU && item.Musterija == voznja.Musterija)
                {
                    return false;
                }
            }

            if (voznje.voznje.Count == 0)
                voznja.IdVoznje = 1;
            else
                voznja.IdVoznje = voznje.voznje.Count;

            voznja.VremePorudzbine = DateTime.Now;

            voznja.Status = StatusVoznje.Status.KREIRANA_NA_CEKANJU;

            voznja.Odrediste = new Lokacija("", "", "", "", "");
            voznja.Komentar = new Komentar("", DateTime.Now.ToString(), "", voznja.IdVoznje.ToString(), "0");

            voznje.voznje.Add(voznja);
            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Voznje.txt";
            StringBuilder sb = new StringBuilder();

            sb.Append(voznja.IdVoznje + ";" + voznja.VremePorudzbine + ";" + voznja.Lokacija.X + ";" + voznja.Lokacija.Y + ";" + voznja.Lokacija.Adresa.UlicaBroj + ";" + voznja.Lokacija.Adresa.NaseljenoMjesto + ";" + voznja.Lokacija.Adresa.PozivniBroj + ";" + voznja.Automobil + ";" + voznja.Musterija + ";" + voznja.Odrediste.X + ";" + voznja.Odrediste.Y + ";" + voznja.Odrediste.Adresa.UlicaBroj + ";" + voznja.Odrediste.Adresa.NaseljenoMjesto + ";" + voznja.Odrediste.Adresa.PozivniBroj + ";" + voznja.Dispecer + ";" + voznja.Vozac + ";" + voznja.Iznos + ";" + voznja.Komentar.Opis + ";" + voznja.Komentar.DatumObjave + ";" + voznja.Komentar.KorisnickoIme + ";" + voznja.Komentar.IdVoznje + ";" + voznja.Komentar.OcenaVoznje + ";" + voznja.Status + "\n");

            if (!File.Exists(path))
                File.WriteAllText(path, sb.ToString());
            else
                File.AppendAllText(path, sb.ToString());

            voznje = new Voznje("~/App_Data/Voznje.txt");
            HttpContext.Current.Application["voznje"] = voznje;

            return true;
        }
    }
}
