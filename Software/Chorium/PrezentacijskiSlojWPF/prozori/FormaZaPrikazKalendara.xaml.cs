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

namespace PrezentacijskiSlojWPF.prozori
{
    /// <summary>
    /// Interaction logic for FormaZaPrikazKalendara.xaml
    /// </summary>
    public partial class FormaZaPrikazKalendara : UserControl
    {
        private ContentControl _sadrzaj;
        public FormaZaPrikazKalendara(ContentControl sadrzaj)
        {
            InitializeComponent();
            _sadrzaj = sadrzaj;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgPopisAktivnosti.ItemsSource = DohvatiAktivnosti(DateTime.Now);

            Aktivnost odabranaAktivnost = dgPopisAktivnosti.Items[0] as Aktivnost;
            if (odabranaAktivnost != null)
            {
                PrikaziKorisnikeAktivnosti(odabranaAktivnost);
            }
            cvKalendarAktivnosti.SelectedDate = DateTime.Now;
        }

        private List<Aktivnost> DohvatiAktivnosti(DateTime datum)
        {
            AktivnostServis servis = new AktivnostServis();
            var listaAktivnosti = servis.DajAktivnostiZaDan(datum);
            return listaAktivnosti;
        }

        private void dgPopisAktivnosti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Aktivnost odabranaAktivnost = dgPopisAktivnosti.CurrentItem as Aktivnost;
            if (odabranaAktivnost != null)
            {
                PrikaziKorisnikeAktivnosti(odabranaAktivnost);
            }
        }

        private void PrikaziKorisnikeAktivnosti(Aktivnost odabranaAktivnost)
        {
            lvKorisniciAktivnosti.Items.Clear();
            foreach (var korisnik in odabranaAktivnost.Korisnik)
            {
                lvKorisniciAktivnosti.Items.Add(korisnik.Korisnicko_ime);
            }
        }

        private void cvKalendarAktivnosti_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            OsvjeziAktivnostiZaOdabraniDan();
        }

        private void btnNovaAktivnost_Click(object sender, RoutedEventArgs e)
        {
            _sadrzaj.Content = new FormaZaDodavanjeAktivnosti(_sadrzaj);
        }

        private void btnDodajUPostojecuAktivnost_Click(object sender, RoutedEventArgs e)
        {
            KorisnikServis korisnikServis = new KorisnikServis();
            AktivnostServis aktivnostServis = new AktivnostServis();

            Korisnik trenutniKorisnik = korisnikServis.DohvatiTrenutnogKorisnika();
            Aktivnost trenutnaAktivnost = dgPopisAktivnosti.SelectedItem as Aktivnost;

            if (trenutnaAktivnost != null)
            {
                if (!ProvjeriKorisnikeAktivnosti(trenutnaAktivnost, trenutniKorisnik))
                {
                    bool uspjeh = aktivnostServis.DodajKorisnikaUAktivnost(trenutnaAktivnost, trenutniKorisnik);
                    if (uspjeh)
                    {
                        OsvjeziAktivnostiZaOdabraniDan();
                        MessageBox.Show("Korisnik dodan u aktivnost.");
                    }
                }
                else
                {
                    MessageBox.Show("Korisnik je već uključen u aktivnost.");
                }
            }
            else
            {
                MessageBox.Show("Molim odaberite aktivnost u koju se želite dodati.");
            }
        }

        private bool ProvjeriKorisnikeAktivnosti(Aktivnost aktivnost, Korisnik korisnik)
        {
            bool postoji = false;
            foreach (var kor in aktivnost.Korisnik)
            {
                if (kor.ID == korisnik.ID) postoji = true;
            }
            return postoji;
        }

        private void OsvjeziAktivnostiZaOdabraniDan()
        {
            lvKorisniciAktivnosti.Items.Clear();
            DateTime odabraniDatum = (DateTime)cvKalendarAktivnosti.SelectedDate;
            dgPopisAktivnosti.ItemsSource = DohvatiAktivnosti(odabraniDatum);
        }

        private void dgPopisAktivnosti_Loaded(object sender, RoutedEventArgs e)
        {
            dgPopisAktivnosti.Columns[0].Visibility = Visibility.Hidden;
            dgPopisAktivnosti.Columns[4].Visibility = Visibility.Hidden;
            dgPopisAktivnosti.Columns[1].Header = "Naziv Aktivnosti";
            dgPopisAktivnosti.Columns[2].Header = "Datum početka";
            dgPopisAktivnosti.Columns[3].Header = "Datum završetka";
        }
    }
}
