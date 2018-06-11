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
        public bool Post([FromBody]Korisnik korisnik)
        {
            Korisnici users = (Korisnici)HttpContext.Current.Application["korisnici"];
            foreach (var item in users.korisnici)
            {
                if(item.KorisnickoIme == korisnik.KorisnickoIme && item.Lozinka == korisnik.Lozinka)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
