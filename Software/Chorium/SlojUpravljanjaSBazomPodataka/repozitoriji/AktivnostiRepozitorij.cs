using SlojEntiteta.Entiteti;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.repozitoriji
{
    public class AktivnostiRepozitorij : Repozitori<Aktivnost>
    {
        public AktivnostiRepozitorij() : base(new ChoriumModel())
        {
        }

        public IQueryable<Aktivnost> DajAktivnostiZaDan(DateTime dan)
        {
            var query = from a in Entiteti.Include("Korisnik")
                        where DbFunctions.TruncateTime(a.Datum_pocetka) == DbFunctions.TruncateTime(dan)
                        select a;
            return query;
        }

        public int DodajKorisnikaUAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            Entiteti.Attach(aktivnost);
            Context.Entry(korisnik).State = EntityState.Unchanged;
            aktivnost.Korisnik.Add(korisnik);
            return SpremiPromjene();
        }

        public int DodajAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            Context.Entry(korisnik).State = EntityState.Unchanged;
            aktivnost.Korisnik.Add(korisnik);
            Entiteti.Add(aktivnost);
            return SpremiPromjene();
        }

        public override int Azuriraj(Aktivnost entitet)
        {
            throw new NotImplementedException();
        }
    }
}
