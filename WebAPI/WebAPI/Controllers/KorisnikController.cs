using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class KorisnikController : ApiController
    {
        public List<Korisnik> Get()
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["korisnici"];
            return korisnici.korisnici;
        }
        public bool Put(int id, [FromBody]Korisnik korisnik)
        {
            Korisnici k = (Korisnici)HttpContext.Current.Application["korisnici"];
            string path = "~/App_Data/Korisnici.txt";
            path = HostingEnvironment.MapPath(path);
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
                    sb.Append(item.Id + ";" + item.KorisnickoIme + ";" + item.Lozinka + ";" + item.Ime + ";" + item.Prezime + ";" + item.Pol + ";" + item.JMBG + ";" + item.Telefon + ";" + item.Email + ";" + item.Uloga + ";" + item.Voznja + ";" + item.Banovan + "\n");
                    string[] arrLine = File.ReadAllLines(path);
                    arrLine[item.Id - 1] = sb.ToString();
                    File.WriteAllLines(path, arrLine);
                    File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));
                    k = new Korisnici("~/App_Data/Korisnici.txt");
                    HttpContext.Current.Application["korisnici"] = k;
                    return true;
                }
            }
            return false;
        }
        [Route("api/Korisnik/PutBanovanje/{id:int}")]
        public bool PutBanovanje(int id, [FromBody]Korisnik value)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["korisnici"];

            //Validacija
            if (korisnici.korisnici == null)
                korisnici.korisnici = new List<Korisnik>();


            foreach (var item in korisnici.korisnici)
            {
                if(item.Id == id)
                {
                    if(item.Banovan != value.Banovan)
                    {
                        item.Banovan = value.Banovan;
                        string path = "~/App_Data/Korisnici.txt";
                        path = HostingEnvironment.MapPath(path);
                        StringBuilder sb = new StringBuilder();
                        sb.Append(item.Id + ";" + item.KorisnickoIme + ";" + item.Lozinka + ";" + item.Ime + ";" + item.Prezime + ";" + item.Pol + ";" + item.JMBG + ";" + item.Telefon + ";" + item.Email + ";" + item.Uloga + ";" + item.Voznja + ";" + item.Banovan + "\n");
                        string[] arrLine = File.ReadAllLines(path);
                        arrLine[item.Id - 1] = sb.ToString();
                        File.WriteAllLines(path, arrLine);
                        File.WriteAllLines(path, File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)));

                        korisnici = new Korisnici("~/App_Data/Korisnici.txt");
                        HttpContext.Current.Application["korisnici"] = korisnici;
                        return true;
                    }
                }
            }

            return true;
        }
    }
}
