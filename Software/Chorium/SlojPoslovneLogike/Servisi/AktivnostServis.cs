using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Servisi
{
    public class AktivnostServis
    {
        public List<Aktivnost> DajAktivnostiZaDan(DateTime dan)
        {
            using (var repo = new AktivnostiRepozitorij())
            {
                List<Aktivnost> aktivnosti = repo.DajAktivnostiZaDan(dan).ToList();
                return aktivnosti;
            }
        }

        public bool DodajKorisnikaUAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            bool uspjeh = false;
            using (var repo = new AktivnostiRepozitorij())
            {
                int redovi = repo.DodajKorisnikaUAktivnost(aktivnost, korisnik);
                uspjeh = redovi > 0;
            }
            return uspjeh;
        }

        public bool DodajAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            bool uspjeh = false;
            using (var repo = new AktivnostiRepozitorij())
            {
                int redovi = repo.DodajAktivnost(aktivnost, korisnik);
                uspjeh = redovi > 0;
            }
            return uspjeh;
        }
    }
}
