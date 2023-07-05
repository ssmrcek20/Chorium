using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike
{
    public class KorisnikPosloviTablica
    {
        public string KorisnikNapravio { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumPocetka { get; set; }
        public string KorisnikDodao { get; set; }
        public string Kategorija { get; set; }

        public KorisnikPosloviTablica(string korisnikNapravio, string naziv, DateTime datumPocetka, string korisnikDodao, string kategorija)
        {
            KorisnikNapravio=korisnikNapravio;
            Naziv=naziv;
            DatumPocetka=datumPocetka;
            KorisnikDodao=korisnikDodao;
            Kategorija=kategorija;
        }
    }
}
