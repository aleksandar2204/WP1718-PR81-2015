using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class StatusVoznje
    {
        public enum Status { NA_CEKANJU, FORMIRANA, OBRADJENA, PRIHVACENA, OTKAZANA, NEUSPESNA }
    }
}