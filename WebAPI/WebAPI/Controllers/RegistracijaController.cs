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
    }
}
