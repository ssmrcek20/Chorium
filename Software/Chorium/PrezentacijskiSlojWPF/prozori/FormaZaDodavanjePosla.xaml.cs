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
    /// Interaction logic for FormaZaDodavanjePosla.xaml
    /// </summary>
    public partial class FormaZaDodavanjePosla : UserControl
    {
        private ContentControl _sadrzaj;
        public FormaZaDodavanjePosla(ContentControl sadrzaj)
        {
            InitializeComponent();
            _sadrzaj = sadrzaj;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            _sadrzaj.Content = new FormaZaPrikazPoslova(_sadrzaj);
        }

        private void btnDodajPosao_Click(object sender, RoutedEventArgs e)
        {
            if (ProvjeriPodatke())
            {
                string nazivPosla = txtNazivPosla.Text;
                DateTime datumRoka = NapraviDatumRoka();
                Kategorija kategorija = cmbKategorija.SelectedItem as Kategorija;

                KorisnikServis korisnikServis = new KorisnikServis();
                Korisnik trenutniKorisnik = korisnikServis.DohvatiTrenutnogKorisnika();

                Status status = new Status
                {
                    ID = 0,
                    Naziv = "nedovrsen"
                };

                List<Korisnik> zaduzeniKorisnici = new List<Korisnik>();
                foreach (var kor in lvZaduzeniClanovi.SelectedItems)
                {
                    zaduzeniKorisnici.Add(kor as Korisnik);
                }

                if (korisnikServis.ProvjeriDobneGranice(zaduzeniKorisnici, kategorija))
                {
                    Kucanski_posao noviPosao = new Kucanski_posao
                    {
                        Naziv = nazivPosla,
                        Datum_pocetka = DateTime.Now,
                        Datum_kraja = datumRoka,
                        Status = status,
                        Korisnik = trenutniKorisnik,
                        Kategorija = kategorija,
                        Korisnik1 = zaduzeniKorisnici,
                    };

                    KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis();

                    bool uspjesnoDodan = kucanskiPosaoServis.DodajKucanskiPosao(noviPosao);
                    if (uspjesnoDodan)
                    {
                        _sadrzaj.Content = new FormaZaPrikazPoslova(_sadrzaj);
                    }

                }
                else
                {
                    MessageBox.Show("Dobna granica kategorije je previsoka za neke od odabranih korisnika.");
                }
            }
            else
            {
                MessageBox.Show("Provjerite podatke.");
            }
        }

        private bool ProvjeriPodatke()
        {
            bool ispravni = false;

            int rokH, rokM, rokS;

            if (int.TryParse(txtRokH.Text, out rokH) && int.TryParse(txtRokM.Text, out rokM) && int.TryParse(txtRokS.Text, out rokS))
            {
                if (txtNazivPosla.Text != "" && dtpDatumRoka.SelectedDate != null
                && cmbKategorija.SelectedItem != null && lvZaduzeniClanovi.SelectedItems != null
                && rokH <= 24 && rokH >= 0 && rokM <= 59 && rokM >= 0 && rokS <= 59 && rokS >= 0)
                {
                    ispravni = true;
                }
            }

            return ispravni;
        }

        private DateTime NapraviDatumRoka()
        {
            DateTime datum = (DateTime)dtpDatumRoka.SelectedDate;
            string datumString = datum.ToString("yyyy-MM-dd");

            string sati = txtRokH.Text;
            string minute = txtRokM.Text;
            string sekunde = txtRokS.Text;

            if (sati == "") sati = "00";
            if (minute == "") minute = "00";
            if (sekunde == "") sekunde = "00";

            string vrijeme = sati + ":" + minute + ":" + sekunde;
            string finalniDatum = datumString + " " + vrijeme;
            return DateTime.Parse(finalniDatum);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PopuniComboBox();
            PopuniListBox();
        }

        private void PopuniListBox()
        {
            KorisnikServis korisnikServis = new KorisnikServis();
            lvZaduzeniClanovi.ItemsSource = korisnikServis.DohvatiKorisnike();
        }

        private void PopuniComboBox()
        {
            KategorijaServis kategorijaServis = new KategorijaServis();
            cmbKategorija.ItemsSource = kategorijaServis.DohvatiKategorije();
        }

        private void cmbKategorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kategorija odabranaKategorija = cmbKategorija.SelectedItem as Kategorija;
            txtDobnaGranica.Text = odabranaKategorija.Dobna_granica.ToString();
        }
    }
}
