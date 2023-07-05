using SlojEntiteta.Entiteti;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.repozitoriji
{
    public class KucanskiPosaoRepozitorij : Repozitori<Kucanski_posao>
    {
        public KucanskiPosaoRepozitorij() : base(new ChoriumModel()) { }

        public override int Azuriraj(Kucanski_posao entitet)
        {
            var korisnikDodao = Context.Korisnik.SingleOrDefault(k => k.ID == entitet.Korisnik.ID);
            var statusPosla = Context.Status.SingleOrDefault(s => s.ID == entitet.Status.ID);
            var kategorijaPosla = Context.Kategorija.SingleOrDefault(c => c.ID == entitet.Kategorija.ID);
            var korisniciPosla = new List<Korisnik>();
            foreach (var kor in entitet.Korisnik1)
            {
                var ko = Context.Korisnik.SingleOrDefault(k => k.ID == kor.ID);
                korisniciPosla.Add(ko);
            }

            var posao = Entiteti.SingleOrDefault(p => p.ID == entitet.ID);

            posao.Naziv = entitet.Naziv;
            posao.Kategorija = kategorijaPosla;
            posao.Status = statusPosla;
            posao.Datum_kraja = entitet.Datum_kraja;
            posao.Korisnik1.Clear();
            posao.Korisnik1 = korisniciPosla;
            posao.Korisnik = korisnikDodao;

            return SpremiPromjene();
        }

        public override int Dodaj(Kucanski_posao kucanski_Posao)
        {
            var korisnikDodao = Context.Korisnik.SingleOrDefault(k => k.ID == kucanski_Posao.Korisnik.ID);
            var statusPosla = Context.Status.SingleOrDefault(s => s.ID == kucanski_Posao.Status.ID);
            var kategorijaPosla = Context.Kategorija.SingleOrDefault(c => c.ID == kucanski_Posao.Kategorija.ID);

            var korisniciPosla = new List<Korisnik>();
            foreach (var kor in kucanski_Posao.Korisnik1)
            {
                var ko = Context.Korisnik.SingleOrDefault(k => k.ID == kor.ID);
                korisniciPosla.Add(ko);
            }

            Kucanski_posao posao = new Kucanski_posao
            {
                Naziv = kucanski_Posao.Naziv,
                Datum_pocetka = kucanski_Posao.Datum_pocetka,
                Datum_kraja = kucanski_Posao.Datum_kraja,
                Status = statusPosla,
                Korisnik = korisnikDodao,
                Kategorija = kategorijaPosla,
                Korisnik1 = korisniciPosla
            };

            Entiteti.Add(posao);

            return SpremiPromjene();
        }

        public IQueryable<Kucanski_posao> DohvatiPosloveUjutro(DateTime datum,Korisnik korisnik)
        {
            var datumPocetak = new DateTime(datum.Year, datum.Month, datum.Day, 0, 0, 0);
            var datumKraj = new DateTime(datum.Year, datum.Month, datum.Day, 23, 59, 59);
            var query = from k in Entiteti
                        where k.Korisnik1.Any(kor => kor.ID == korisnik.ID) && k.Datum_kraja > datumPocetak && k.Datum_kraja < datumKraj && k.ID_status == 0
                        orderby k.Datum_kraja
                        select k;

            return query;
        }

        public IQueryable<Kucanski_posao> DohvatiPosloveNavecer(DateTime datum, Korisnik korisnik)
        {
            var datumPocetak = new DateTime(datum.Year, datum.Month, datum.Day, 0, 0, 0);
            var datumKraj = new DateTime(datum.Year, datum.Month, datum.Day, 23, 59, 59);
            var query = from k in Entiteti
                        where k.Korisnik1.Any(kor => kor.ID == korisnik.ID) && k.Datum_kraja > datumPocetak && k.Datum_kraja < datumKraj
                        orderby k.Datum_kraja
                        select k;

            return query;
        }

        public IQueryable<Kucanski_posao> DohvatiObavljenePosloveKorisnika(DateTime datum, Korisnik korisnik)
        {
            var query = from k in Entiteti.Include("Kategorija").Include("Korisnik")
                        where k.ID_status == 1 && k.Datum_pocetka.Month == datum.Month && k.Datum_pocetka.Year == datum.Year && k.Korisnik1.Any(kor => kor.ID == korisnik.ID)
                        select k;

            return query;
        }

        public IQueryable<Kucanski_posao> DohvatiPosloveKorisnika(Korisnik korisnik)
        {
            var query = from k in Entiteti.Include("Kategorija").Include("Korisnik").Include("Status").Include("Korisnik1")
                        where k.ID_status == 0 && k.Korisnik1.Any(kor => kor.ID == korisnik.ID)
                        select k;
            return query;
        }

        public int Rijesi(Kucanski_posao kucanski_Posao)
        {
            Kucanski_posao posao = Entiteti.SingleOrDefault(kp => kp.ID == kucanski_Posao.ID);
            Status status = Context.Status.SingleOrDefault(s => s.ID == 1);

            posao.Status = status;

            return SpremiPromjene();
        }

        public int StaviNaCekanje(Kucanski_posao kucanski_Posao)
        {
            Kucanski_posao posao = Entiteti.SingleOrDefault(kp => kp.ID == kucanski_Posao.ID);
            Status status = Context.Status.SingleOrDefault(s => s.ID == 2);

            posao.Status = status;

            return SpremiPromjene();
        }
    }
}
