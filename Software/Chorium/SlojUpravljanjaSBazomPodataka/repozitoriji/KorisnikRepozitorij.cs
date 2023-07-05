using SlojEntiteta.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.repozitoriji
{
    public class KorisnikRepozitorij : Repozitori<Korisnik>
    {
        public KorisnikRepozitorij() : base(new ChoriumModel()) { }

        public override int Azuriraj(Korisnik entitet)
        {
            throw new NotImplementedException();
        }

        public bool ProvjeriKorisnikoIme(string korisnickoIme)
        {
            var query = from k in Entiteti
                        where k.Korisnicko_ime == korisnickoIme
                        select k;
            bool korinskPostoji = query.Count() > 0;
            return korinskPostoji;
        }

        public Korisnik DohvatiKorisnika(string korime)
        {
            Korisnik korisnik;
            var query = from k in Entiteti
                        where k.Korisnicko_ime == korime
                        select k;
            if (query.ToList().Count > 0)
            {
                korisnik = query.ToList()[0];
                return korisnik;
            }
            else return null;
            
        }
    }
}
