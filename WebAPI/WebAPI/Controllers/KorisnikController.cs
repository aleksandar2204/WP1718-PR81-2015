﻿using System;
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
    public class KorisnikController : ApiController
    {
        public bool Put(int id, [FromBody]Korisnik korisnik)
        {
            Korisnici k = (Korisnici)HttpContext.Current.Application["korisnici"];
            string path = @"C:\Users\Aleksandar\Desktop\WEB_projekat\WP1718-PR81-2015\WebAPI\WebAPI\App_Data\Korisnici.txt";
            foreach (var item in k.korisnici)
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