using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike
{
    public class KorisnikPoslovi
    {
        public string KorisnickoIme { get; set; }
        public int BrojPoslova { get; set; }

        public KorisnikPoslovi(string korisnickoIme, int brojPoslova)
        {
            KorisnickoIme = korisnickoIme;
            BrojPoslova = brojPoslova;
        }
    }
}
