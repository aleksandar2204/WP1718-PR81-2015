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
        public bool Put([FromBody]Voznja voznja)
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];
            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Voznje.txt";
            foreach (var item in voznje.voznje)
            {
                if (voznja.Musterija == item.Musterija && item.Status == StatusVoznje.Status.KREIRANA_NA_CEKANJU)
                {
                    item.Lokacija.X = voznja.Lokacija.X;
                    item.Lokacija.Y = voznja.Lokacija.Y;
                    item.Lokacija.Adresa.UlicaBroj = voznja.Lokacija.Adresa.UlicaBroj;
                    item.Lokacija.Adresa.NaseljenoMjesto = voznja.Lokacija.Adresa.NaseljenoMjesto;
                    item.Lokacija.Adresa.PozivniBroj = voznja.Lokacija.Adresa.PozivniBroj;
                    item.Automobil = voznja.Automobil;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(item.IdVoznje + ";" + item.VremePorudzbine + ";" + item.Lokacija.X + ";" + item.Lokacija.Y + ";" + item.Lokacija.Adresa.UlicaBroj + ";" + item.Lokacija.Adresa.NaseljenoMjesto + ";" + item.Lokacija.Adresa.PozivniBroj + ";" + item.Automobil + ";" + item.Musterija + ";" + item.Odrediste.X + ";" + item.Odrediste.Y + ";" + item.Odrediste.Adresa.UlicaBroj + ";" + item.Odrediste.Adresa.NaseljenoMjesto + ";" + item.Odrediste.Adresa.PozivniBroj + ";" + item.Dispecer + ";" + item.Vozac + ";" + item.Iznos + ";" + item.Komentar.Opis + ";" + item.Komentar.DatumObjave + ";" + item.Komentar.KorisnickoIme + ";" + item.Komentar.IdVoznje + ";" + item.Komentar.OcenaVoznje + ";" + item.Status + "\n");
                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[item.IdVoznje - 1] = sb.ToString();
                    File.WriteAllLines(path, arrLine);
                    File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));
                    return true;
                }
            }
            return false;
        }

        [Route("api/Voznja/PUT")]
        public bool PUT([FromBody]Voznja voznja)
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["korisnici"];
            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Voznje.txt";

            foreach (var item in voznje.voznje)
            {
                if(item.Status == StatusVoznje.Status.KREIRANA_NA_CEKANJU && voznja.Musterija == item.Musterija && item.Dispecer == 0)
                {
                    item.Komentar.Opis = voznja.Komentar.Opis;
                    item.Komentar.DatumObjave = DateTime.Now;
                    foreach (var korisnik in korisnici.korisnici)
                    {
                        if (item.Musterija == korisnik.Id)
                        {
                            item.Komentar.KorisnickoIme = korisnik.KorisnickoIme;
                            break;
                        }
                    }
                    item.Komentar.IdVoznje = item.IdVoznje;
                    item.Komentar.OcenaVoznje = voznja.Komentar.OcenaVoznje;
                    item.Status = StatusVoznje.Status.OTKAZANA;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(item.IdVoznje + ";" + item.VremePorudzbine + ";" + item.Lokacija.X + ";" + item.Lokacija.Y + ";" + item.Lokacija.Adresa.UlicaBroj + ";" + item.Lokacija.Adresa.NaseljenoMjesto + ";" + item.Lokacija.Adresa.PozivniBroj + ";" + item.Automobil + ";" + item.Musterija + ";" + item.Odrediste.X + ";" + item.Odrediste.Y + ";" + item.Odrediste.Adresa.UlicaBroj + ";" + item.Odrediste.Adresa.NaseljenoMjesto + ";" + item.Odrediste.Adresa.PozivniBroj + ";" + item.Dispecer + ";" + item.Vozac + ";" + item.Iznos + ";" + item.Komentar.Opis + ";" + item.Komentar.DatumObjave + ";" + item.Komentar.KorisnickoIme + ";" + item.Komentar.IdVoznje + ";" + item.Komentar.OcenaVoznje + ";" + item.Status + "\n");
                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[item.IdVoznje - 1] = sb.ToString();
                    File.WriteAllLines(path, arrLine);
                    File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));
                    return true;
                }
            }
            return false;
        }
    }
}
