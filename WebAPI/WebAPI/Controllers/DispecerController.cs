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
    public class DispecerController : ApiController
    {
        public bool Put(int id, [FromBody]Dispecer korisnik)
        {
            Dispeceri dispeceri = (Dispeceri)HttpContext.Current.Application["dispeceri"];
            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Dispeceri.txt";
            foreach (var item in dispeceri.dispecers)
            {
                if (item.Id == id)
                {
                    item.KorisnickoIme = korisnik.KorisnickoIme;
                    item.Lozinka = korisnik.Lozinka;
                    item.Ime = korisnik.Ime;
                    item.Prezime = korisnik.Prezime;
                    item.JMBG = korisnik.JMBG;
                    item.Pol = korisnik.Pol;
                    item.Uloga = korisnik.Uloga;
                    item.Telefon = korisnik.Telefon;
                    item.Email = korisnik.Email;
                    item.Voznja = korisnik.Voznja;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(item.Id + ";" + item.KorisnickoIme + ";" + item.Lozinka + ";" + item.Ime + ";" + item.Prezime + ";" + item.Pol + ";" + item.JMBG + ";" + item.Telefon + ";" + item.Email + ";" + item.Uloga + ";" + item.Voznja + "\n");
                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[item.Id - 1] = sb.ToString();
                    File.WriteAllLines(path, arrLine);
                    File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                    dispeceri = new Dispeceri("~/App_Data/Dispeceri.txt");
                    HttpContext.Current.Application["dispeceri"] = dispeceri;
                    return true;
                }
            }
            return false;
        }

        [Route("api/Dispecer/PostD")]
        public bool PostD([FromBody]Voznja voznja)  // Dodavanje voznje iz Dispecera
        {
            Voznje voznje = (Voznje)HttpContext.Current.Application["voznje"];

            voznja.IdVoznje = voznje.voznje.Count + 1;

            voznja.VremePorudzbine = DateTime.Now;

            voznja.Status = StatusVoznje.Status.FORMIRANA;

            voznja.Odrediste = new Lokacija("", "", "", "", "");
            voznja.Komentar = new Komentar("", DateTime.Now.ToString(), "", voznja.IdVoznje.ToString(), "0");

            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Voznje.txt";
            StringBuilder sb = new StringBuilder();

            Vozaci vozaci = (Vozaci)HttpContext.Current.Application["vozaci"];
            string pathV = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Vozaci.txt";
            foreach (var item in vozaci.vozaci)
            {
                if(item.KorisnickoIme == voznja.Vozac)
                {
                    //voznja.Automobil = item.Automobil.Tip;
                    item.Zauzet = true;
                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append(item.Id + ";" + item.KorisnickoIme + ";" + item.Lozinka + ";" + item.Ime + ";" + item.Prezime + ";" + item.Pol + ";" + item.JMBG + ";" + item.Telefon + ";" + item.Email + ";" + item.Uloga + ";" + item.Voznja + ";" + item.Lokacija.X + ";" + item.Lokacija.Y + ";" + item.Lokacija.Adresa.UlicaBroj + ";" + item.Lokacija.Adresa.NaseljenoMjesto + ";" + item.Lokacija.Adresa.PozivniBroj + ";" + item.Automobil.Vozac + ";" + item.Automobil.GodisteAutomobila + ";" + item.Automobil.BrojRegistarskeOznake + ";" + item.Automobil.BrojTaksiVozila + ";" + item.Automobil.Tip + ";" + item.Zauzet + "\n");
                    string[] arrLine = File.ReadAllLines(pathV);
                    arrLine[item.Id - 1] = sb1.ToString();
                    File.WriteAllLines(pathV, arrLine);
                    File.WriteAllLines(pathV, File.ReadAllLines(pathV).Where(l => !string.IsNullOrWhiteSpace(l)));
                    vozaci = new Vozaci("~/App_Data/Vozaci.txt");
                    HttpContext.Current.Application["vozaci"] = vozaci;
                    break;
                }
            }

            sb.Append(voznja.IdVoznje + ";" + voznja.VremePorudzbine + ";" + voznja.Lokacija.X + ";" + voznja.Lokacija.Y + ";" + voznja.Lokacija.Adresa.UlicaBroj + ";" + voznja.Lokacija.Adresa.NaseljenoMjesto + ";" + voznja.Lokacija.Adresa.PozivniBroj + ";" + voznja.Automobil + ";" + voznja.Musterija + ";" + voznja.Odrediste.X + ";" + voznja.Odrediste.Y + ";" + voznja.Odrediste.Adresa.UlicaBroj + ";" + voznja.Odrediste.Adresa.NaseljenoMjesto + ";" + voznja.Odrediste.Adresa.PozivniBroj + ";" + voznja.Dispecer + ";" + voznja.Vozac + ";" + voznja.Iznos + ";" + voznja.Komentar.Opis + ";" + voznja.Komentar.DatumObjave + ";" + voznja.Komentar.KorisnickoIme + ";" + voznja.Komentar.IdVoznje + ";" + voznja.Komentar.OcenaVoznje + ";" + voznja.Status + "\n");

            voznje.voznje.Add(voznja);

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
