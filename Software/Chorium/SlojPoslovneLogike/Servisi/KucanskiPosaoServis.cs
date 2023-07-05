using Microsoft.Toolkit.Uwp.Notifications;
using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Servisi
{
    public class KucanskiPosaoServis
    {
        private readonly SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("stanko.smrcek2329@gmail.com", "sjpgyjxvtdbzfbjy"),
            EnableSsl = true,
        };
        public void PostaviSlanjeMaila()
        {
            var datumSada = DateTime.Now;
            var datumUjutro = new DateTime(datumSada.Year, datumSada.Month, datumSada.Day, 8, 0, 0);
            var datumNavecer = new DateTime(datumSada.Year, datumSada.Month, datumSada.Day, 21, 0, 0);
            PostaviTimer(datumUjutro, true);
            PostaviTimer(datumNavecer, false);
        }

        private void PostaviTimer(DateTime datum, Boolean jutro)
        {
            var datumSada = DateTime.Now;

            TimeSpan ts;
            if (datum > datumSada)
                ts = datum - datumSada;
            else
            {
                datum = datum.AddDays(1);
                ts = datum - datumSada;
            }

            Task.Delay(ts).ContinueWith((x) =>
            {

                if (jutro)
                {
                    PostaviUjutro();
                }
                else
                {
                    PostaviNavecer();
                }

                PostaviTimer(datum.AddDays(1), jutro);
            });

        }

        private void PostaviNavecer()
        {
            List<Kucanski_posao> kucanskiPoslovi;
            var servis = new KorisnikServis();
            List<Korisnik> korisnici = servis.DohvatiKorisnike();

            foreach (Korisnik korisnik in korisnici)
            {

                using (var repo = new KucanskiPosaoRepozitorij())
                {
                    kucanskiPoslovi = repo.DohvatiPosloveNavecer(DateTime.Now, korisnik).ToList();

                    if (kucanskiPoslovi.Count > 0)
                    {
                        StringBuilder porukaBuilder = new StringBuilder();
                        StringBuilder porukaObavljeniBuilder = new StringBuilder("<html><body><h3>Obavljeni poslovi:</h3><ul>");
                        StringBuilder porukaNeobavljeniBuilder = new StringBuilder("<h3>Neobavljeni poslovi:</h3><ul>");
                        foreach (Kucanski_posao kucanskiPosao in kucanskiPoslovi)
                        {
                            if (kucanskiPosao.ID_status == 1)
                            {
                                porukaObavljeniBuilder.Append("<li><b>").Append(kucanskiPosao.Naziv).Append("</b></li>");
                            }
                            else
                            {
                                porukaNeobavljeniBuilder.Append("<li><b>").Append(kucanskiPosao.Naziv).Append("</b> napravi do ").Append(kucanskiPosao.Datum_kraja.ToString("HH:mm")).Append("</li>");
                            }
                        }
                        porukaObavljeniBuilder.Append("</ul>");
                        porukaNeobavljeniBuilder.Append("</ul></body></html>");
                        porukaBuilder.Append(porukaObavljeniBuilder).Append(porukaNeobavljeniBuilder);
                        var mail = new MailMessage
                        {
                            From = new MailAddress("stanko.smrcek2329@gmail.com"),
                            Subject = "Popis poslova za " + DateTime.Now.ToString("d.M.yyyy") + " - Chorium",
                            Body = porukaBuilder.ToString(),
                            IsBodyHtml = true,
                        };
                        mail.To.Add(korisnik.Email);

                        smtpClient.Send(mail);
                    }

                }
            }
        }

        private void PostaviUjutro()
        {
            List<Kucanski_posao> kucanskiPoslovi;
            var servis = new KorisnikServis();
            List<Korisnik> korisnici = servis.DohvatiKorisnike();

            foreach (Korisnik korisnik in korisnici)
            {

                using (var repo = new KucanskiPosaoRepozitorij())
                {
                    kucanskiPoslovi = repo.DohvatiPosloveUjutro(DateTime.Now, korisnik).ToList();

                    if (kucanskiPoslovi.Count > 0)
                    {
                        StringBuilder porukaBuilder = new StringBuilder("<html><body><h3>Poslovi koje trebaš obaviti:</h3><ul>");
                        foreach (Kucanski_posao kucanskiPosao in kucanskiPoslovi)
                        {
                            porukaBuilder.Append("<li><b>").Append(kucanskiPosao.Naziv).Append("</b> napravi do ").Append(kucanskiPosao.Datum_kraja.ToString("HH:mm")).Append("</li>");
                        }
                        porukaBuilder.Append("</ul></body></html>");
                        var mail = new MailMessage
                        {
                            From = new MailAddress("stanko.smrcek2329@gmail.com"),
                            Subject = "Popis poslova za " + DateTime.Now.ToString("d.M.yyyy") + " - Chorium",
                            Body = porukaBuilder.ToString(),
                            IsBodyHtml = true,
                        };
                        mail.To.Add(korisnik.Email);

                        smtpClient.Send(mail);
                    }

                }
            }
        }

        public List<Kucanski_posao> PrikaziPoslove()
        {
            using (var repo = new KucanskiPosaoRepozitorij())
            {
                List<Kucanski_posao> poslovi = new List<Kucanski_posao>();
                poslovi = repo.DajSve().ToList();
                return poslovi;
            }

        }

        public List<Kucanski_posao> PrikaziPoslove(Korisnik korisnik, Status status, Kategorija kategorija)
        {
            using (var repo = new KucanskiPosaoRepozitorij())
            {
                List<Kucanski_posao> poslovi = new List<Kucanski_posao>();
                poslovi = repo.DohvatiPosloveKorisnika(korisnik).ToList();
                poslovi = poslovi.FindAll(e => e.Status.ID == status.ID);
                poslovi = poslovi.FindAll(e => e.Kategorija.ID == kategorija.ID);
                return poslovi;
            }

        }

        public List<Kucanski_posao> PrikaziPoslove(Korisnik korisnik)
        {
            using (var repo = new KucanskiPosaoRepozitorij())
            {
                List<Kucanski_posao> poslovi = new List<Kucanski_posao>();
                poslovi = repo.DohvatiPosloveKorisnika(korisnik).ToList();
                return poslovi;
            }

        }

        public List<KorisnikPosloviTablica> GenerirajPopisPoslova(DateTime datum)
        {
            List<KorisnikPosloviTablica> popisPoslova = new List<KorisnikPosloviTablica>();
            var servis = new KorisnikServis();
            List<Korisnik> korisnici = servis.DohvatiKorisnike();
            foreach (Korisnik korisnik in korisnici)
            {
                using (var repo = new KucanskiPosaoRepozitorij())
                {
                    var poslovi = repo.DohvatiObavljenePosloveKorisnika(datum, korisnik);
                    foreach (var posao in poslovi)
                    {
                        KorisnikPosloviTablica korisnikPosloviTablica = new KorisnikPosloviTablica(korisnik.Korisnicko_ime, posao.Naziv, posao.Datum_pocetka, posao.Korisnik.Korisnicko_ime, posao.Kategorija.Naziv);
                        popisPoslova.Add(korisnikPosloviTablica);
                    }
                }
            }
            return popisPoslova;
        }

        public List<KorisnikPoslovi> GenerirajGraf(DateTime datum)
        {
            List<KorisnikPoslovi> listaKorisnika = new List<KorisnikPoslovi>();
            var servis = new KorisnikServis();
            List<Korisnik> korisnici = servis.DohvatiKorisnike();
            foreach (Korisnik korisnik in korisnici)
            {
                using (var repo = new KucanskiPosaoRepozitorij())
                {
                    var poslovi = repo.DohvatiObavljenePosloveKorisnika(datum, korisnik).Count();
                    KorisnikPoslovi korisnikPoslovi = new KorisnikPoslovi(korisnik.Korisnicko_ime, poslovi);
                    listaKorisnika.Add(korisnikPoslovi);
                }
            }
            return listaKorisnika.OrderBy(k => k.BrojPoslova).ToList();
        }
        public void PostaviObavijest(DateTime vrijeme, Kucanski_posao posao)
        {
            TimeSpan ts = vrijeme - DateTime.Now;
            Task.Delay(ts).ContinueWith((x) =>
            {

                new ToastContentBuilder()
                    .AddText("Vrijeme je da napraviš "+posao.Naziv+"!")
                    .Show();
            });
        }

        public bool DodajKucanskiPosao(Kucanski_posao posao)
        {
            bool uspjeh = false;
            using (var repo = new KucanskiPosaoRepozitorij())
            {
                int redovi = repo.Dodaj(posao);
                uspjeh = redovi > 0;
            }
            return uspjeh;
        }

        public bool RijesiPosao(Kucanski_posao posao)
        {
            bool uspjeh = false;

            using (var repo = new KucanskiPosaoRepozitorij())
            {
                int redovi = repo.Rijesi(posao);
                uspjeh = redovi > 0;
            }

            return uspjeh;
        }

        public string ProvjeriStatusPosla(Kucanski_posao posao)
        {
            return posao.Status.Naziv;
        }

        public bool StaviPosaoNaCekanje(Kucanski_posao posao)
        {
            bool uspjeh = false;

            using (var repo = new KucanskiPosaoRepozitorij())
            {
                int redovi = repo.StaviNaCekanje(posao);
                uspjeh = redovi > 0;
            }

            return uspjeh;
        }

        public bool ObrisiPosao(Kucanski_posao posao)
        {
            bool uspjeh = false;

            using (var repo = new KucanskiPosaoRepozitorij())
            {
                int redovi = repo.Izbrisi(posao);
                uspjeh = redovi > 0;
            }

            return uspjeh;
        }

        public bool AzurirajPosao(Kucanski_posao posao)
        {
            bool uspjeh = false;

            using (var repo = new KucanskiPosaoRepozitorij())
            {
                int redovi = repo.Azuriraj(posao);
                uspjeh = redovi > 0;
            }

            return uspjeh;
        }
    }
}
