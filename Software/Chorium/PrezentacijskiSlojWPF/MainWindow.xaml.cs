using PrezentacijskiSlojWPF.prozori;
using SlojEntiteta.Entiteti;
using SlojPoslovneLogike.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrezentacijskiSlojWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var servis = new KucanskiPosaoServis();
            servis.PostaviSlanjeMaila();
        }

        private void btnRegistriraj(object sender, RoutedEventArgs e)
        {
            sadrzaj.Content = new FormaZaRegistraciju(sadrzaj);
        }

        private void btnKalendar_Click(object sender, RoutedEventArgs e)
        {
            sadrzaj.Content = new FormaZaPrikazKalendara(sadrzaj);
        }

        private void btnIzvjestaj_Click(object sender, RoutedEventArgs e)
        {
            sadrzaj.Content = new FormaZaIzvjesca();
        }

        private void btnTestDodavanjaPosla_Click(object sender, RoutedEventArgs e)
        {
            KorisnikServis servis = new KorisnikServis();
            if (servis.ProvjeriKorisnika())
            {
                sadrzaj.Content = new FormaZaDodavanjePosla(sadrzaj);
            }
            else
            {
                MessageBox.Show("Samo korisnik tipa roditelj može dodavati poslove.");
            }
        }

        private void btnTestUredivanjePosla_Click(object sender, RoutedEventArgs e)
        {
            KorisnikServis servis = new KorisnikServis();

            Status probniStatus = new Status
            {
                ID = 0,
                Naziv = "nedovrsen"
            };

            Kategorija probnaKategorija = new Kategorija
            {
                ID = 2,
                Naziv = "Pospremanje",
                Dobna_granica = 12
            };

            Korisnik probniKorisnik = servis.DohvatiTrenutnogKorisnika();

            List<Korisnik> probniKorisniciLista = new List<Korisnik>();
            probniKorisniciLista.Add(probniKorisnik);

            Kucanski_posao testniPosao = new Kucanski_posao {
                ID = 7,
                Naziv = "qwertyuio",
                Datum_pocetka = DateTime.Parse("2023-01-16 16:45:34.400"),
                Datum_kraja = DateTime.Parse("2023-01-18 01:01:01.000"),
                Status = probniStatus,
                Kategorija = probnaKategorija,
                Korisnik = probniKorisnik,
                Korisnik1 = probniKorisniciLista,
                ID_kategorija = 2,
                ID_korisnik_dodao = 14,
                ID_status = 0
            };

            if (servis.ProvjeriKorisnika())
            {
                sadrzaj.Content = new FormaZaUredivanjeBrisanje(sadrzaj, testniPosao);
            }
            else
            {
                MessageBox.Show("Samo korisnik tipa roditelj može uređivati poslove.");
            }
        }

        private void btnPrijava_click(object sender, RoutedEventArgs e)
        {
            sadrzaj.Content = new FormaZaPrijavu(sadrzaj);
        }

        
        private void btnKucanskiPoslovi_click(object sender, RoutedEventArgs e)
        {
            sadrzaj.Content = new FormaZaPrikazPoslova(sadrzaj);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sadrzaj.Content = new FormaZaPrikazPoslova(sadrzaj);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                sadrzaj.Content = new HelpProzor();
            }
        }
    }
}
