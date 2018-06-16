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
    public class VozacController : ApiController
    {
        public bool Post([FromBody]Vozac vozac)
        {
            Vozaci v = (Vozaci)HttpContext.Current.Application["vozaci"];
            foreach (var item in v.vozaci)
            {
                if (item.KorisnickoIme == vozac.KorisnickoIme)
                {
                    return false;
                }
            }

            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Vozaci.txt";

            vozac.Id = v.vozaci.Count;
            v.vozaci.Add(vozac);
            StringBuilder sb = new StringBuilder();
            
            sb.Append(vozac.Id + ";" + vozac.KorisnickoIme + ";" + vozac.Lozinka + ";" + vozac.Ime + ";" + vozac.Prezime + ";" + vozac.Pol + ";" + vozac.JMBG + ";" + vozac.Telefon + ";" + vozac.Email + ";" + vozac.Uloga + ";" + vozac.Voznja + "-" + vozac.Lokacija.X + "|" + vozac.Lokacija.Y + "|" + vozac.Lokacija.Adresa.UlicaBroj + "|" + vozac.Lokacija.Adresa.NaseljenoMjesto + "|" + vozac.Lokacija.Adresa.PozivniBroj + "|" + vozac.Automobil.Vozac + "|" + vozac.Automobil.GodisteAutomobila + "|" + vozac.Automobil.BrojRegistarskeOznake + "|" + vozac.Automobil.BrojTaksiVozila + "|" + vozac.Automobil.Tip + "\n");
            if (!File.Exists(path))
            {
                File.WriteAllText(path, sb.ToString());
            }
            else
            {
                File.AppendAllText(path, sb.ToString());
            }

            v = new Vozaci(@"~/App_Data/Vozaci.txt");
            HttpContext.Current.Application["vozaci"] = v;
            return true;
        }

        public bool Put(int id, [FromBody]Vozac korisnik)
        {
            Vozaci vozaci = (Vozaci)HttpContext.Current.Application["vozaci"];
            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Vozaci.txt";
            foreach (var item in vozaci.vozaci)
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
                    item.Lokacija.X = korisnik.Lokacija.X;
                    item.Lokacija.Y = korisnik.Lokacija.Y;
                    item.Lokacija.Adresa.UlicaBroj = korisnik.Lokacija.Adresa.UlicaBroj;
                    item.Lokacija.Adresa.NaseljenoMjesto = korisnik.Lokacija.Adresa.NaseljenoMjesto;
                    item.Lokacija.Adresa.PozivniBroj = korisnik.Lokacija.Adresa.PozivniBroj;
                    item.Automobil.BrojRegistarskeOznake = korisnik.Automobil.BrojRegistarskeOznake;
                    item.Automobil.BrojTaksiVozila = korisnik.Automobil.BrojTaksiVozila;
                    item.Automobil.GodisteAutomobila = korisnik.Automobil.GodisteAutomobila;
                    item.Automobil.Tip = korisnik.Automobil.Tip;
                    item.Automobil.Vozac = korisnik.Automobil.Vozac;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(item.Id + ";" + item.KorisnickoIme + ";" + item.Lozinka + ";" + item.Ime + ";" + item.Prezime + ";" + item.Pol + ";" + item.JMBG + ";" + item.Telefon + ";" + item.Email + ";" + item.Uloga + ";" + item.Voznja + "-" + item.Lokacija.X + "|" + item.Lokacija.Y + "|" + item.Lokacija.Adresa.UlicaBroj + "|" + item.Lokacija.Adresa.NaseljenoMjesto + "|" + item.Lokacija.Adresa.PozivniBroj + "|" + item.Automobil.Vozac + "|" + item.Automobil.GodisteAutomobila + "|" + item.Automobil.BrojRegistarskeOznake + "|" + item.Automobil.BrojTaksiVozila + "|" + item.Automobil.Tip + "\n");
                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[item.Id] = sb.ToString();
                    File.WriteAllLines(path, arrLine);
                    File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));
                    return true;
                }
            }
            return false;
        }
    }
}
