using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebAPI.Models
{
    public class Vozaci
    {
        public List<Vozac> vozaci { get; set; }

        public Vozaci() { }

        public Vozaci(string path)
        {
            path = HostingEnvironment.MapPath(path);
            vozaci = new List<Vozac>();
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';', '-');
                string[] tokensLastPart = tokens[11].Split('|');
                Vozac v = new Vozac(Int32.Parse(tokens[0]), tokens[1], tokens[2], tokens[3], tokens[4], tokens[5], tokens[6], tokens[7], tokens[8], tokens[9], tokens[10], tokensLastPart[0], tokensLastPart[1], tokensLastPart[2],
                                    tokensLastPart[3], tokensLastPart[4], tokensLastPart[5], tokensLastPart[6], tokensLastPart[7], tokensLastPart[8], tokensLastPart[9]);
                vozaci.Add(v);
            }
            sr.Close();
            stream.Close();
        }
    }
}