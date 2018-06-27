using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LogController : ApiController
    {
        //[Route("api/Log/{id:string}")]
        [HttpGet]
        public Korisnik Get(string id)
        {
            Dispeceri dispeceri = (Dispeceri)HttpContext.Current.Application["dispeceri"];
            Korisnici users = (Korisnici)HttpContext.Current.Application["korisnici"];
            Vozaci vozaci = (Vozaci)HttpContext.Current.Application["vozaci"];

            foreach (var item in users.korisnici)
            {
                if (id == item.KorisnickoIme)
                {
                    return item;
                }
            }

            foreach (var item in dispeceri.dispecers)
            {
                if (id == item.KorisnickoIme)
                {
                    return item;
                }
            }

            foreach (var item in vozaci.vozaci)
            {
                if (id == item.KorisnickoIme)
                    return item;
            }

            return null;
        }
        public string Post([FromBody]Korisnik korisnik)
        {
            Korisnici users = (Korisnici)HttpContext.Current.Application["korisnici"];
            Dispeceri dispeceri = (Dispeceri)HttpContext.Current.Application["dispeceri"];
            Vozaci vozaci = (Vozaci)HttpContext.Current.Application["vozaci"];
            foreach (var item in users.korisnici)
            {
                if(item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka && item.Banovan == Banovanje.Ban.NIJEBANOVAN)
                {
                    return "Uspesno";
                }
                else if(item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka && item.Banovan == Banovanje.Ban.BANOVAN)
                {
                    return "Banovan";
                }
            }

            foreach (var item in dispeceri.dispecers)
            {
                if (item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka)
                    return "Uspesno";
            }

            foreach (var item in vozaci.vozaci)
            {
                if (item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka && item.Banovan == Banovanje.Ban.NIJEBANOVAN)
                    return "Uspesno";
                else if(item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka && item.Banovan == Banovanje.Ban.BANOVAN)
                    return "Banovan";
            }

            return "Neuspesno";
        }

    }
}
