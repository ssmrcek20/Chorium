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
using System.Windows.Shapes;

namespace PrezentacijskiSlojWPF.prozori
{
    /// <summary>
    /// Interaction logic for FormaZaPrikazPoslova.xaml
    /// </summary>
    public partial class FormaZaPrikazPoslova : UserControl
    {
        DataGrid dataGridGlavni = new DataGrid();
        private ContentControl _sadrzaj;
        public FormaZaPrikazPoslova(ContentControl sadrzaj)
        {
            InitializeComponent();
            _sadrzaj = sadrzaj;
        }

        private void DgvPoslovi_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            KorisnikServis korisnikServis = new KorisnikServis();
            Korisnik korisnik = korisnikServis.DohvatiTrenutnogKorisnika();
            if (korisnik != null)
            {
                prikazUlogiraniKorisnik(korisnik);
                popuniComboBoxeveVrijeme();
            }
            else
            {
                filtarStackPanel.Visibility = Visibility.Hidden;
                var korisnici = korisnikServis.DohvatiKorisnike();
                foreach(var k in korisnici)
                {
                    generirajDataGridove(k);
                }
                spUpravljanjePoslovima.Visibility = Visibility.Hidden;
            }
        }

        private void BtnFiltriraj_Click(object sender, RoutedEventArgs e)
        {
            DataGrid datagrid = myStackPanel.Children[0] as DataGrid;
            datagrid.ItemsSource = new KucanskiPosaoServis().PrikaziPoslove(CmbKorisnik.SelectedItem as Korisnik, CmbStanje.SelectedItem as Status, CmbKategorije.SelectedItem as Kategorija);
            dgLoaded(sender, e, datagrid);
        }

        private void dgLoaded(object sender, RoutedEventArgs e, DataGrid data)
        {
            data.Columns[0].Visibility = Visibility.Hidden;
            data.Columns[4].Visibility = Visibility.Hidden;
            data.Columns[5].Visibility = Visibility.Hidden;
            data.Columns[6].Visibility = Visibility.Hidden;
            data.Columns[10].Visibility = Visibility.Hidden;
            data.Columns[2].Header = "Datum početka";
            data.Columns[3].Header = "Datum kraja";
        }

        private void popuniComboBoxeve()
        {
            CmbKorisnik.ItemsSource = new KorisnikServis().DohvatiKorisnike();
            CmbKorisnik.SelectedIndex = 0;
            CmbKategorije.ItemsSource = new KategorijaServis().DohvatiKategorije();
            CmbKategorije.SelectedIndex = 0;
            CmbStanje.ItemsSource = new StatusServis().DohvatiStatuse();
            CmbStanje.SelectedIndex = 0;
        }

        private void popuniComboBoxeveVrijeme()
        {
            cmbSati.Items.Add("Sati");
            cmbSati.SelectedIndex = 0;
            cmbMinute.Items.Add("Min");
            cmbMinute.SelectedIndex = 0;
            for(int i = 0; i<24; i++)
            {
                cmbSati.Items.Add(i.ToString());
            }
            for (int i = 0; i<60; i++)
            {
                cmbMinute.Items.Add(i.ToString());
            }
        }

        private void generirajDataGridove(Korisnik korisnik)
        {
            StackPanel novipanel = new StackPanel();
            DataGrid dataGrid = new DataGrid();
            Label labela = new Label();
            novipanel.Orientation = Orientation.Vertical;
            dataGrid.Width = 400;  
            labela.Content = korisnik.ToString();
            dataGrid.ItemsSource = new KucanskiPosaoServis().PrikaziPoslove(korisnik);
            dataGrid.Loaded += new RoutedEventHandler((sender, e) => dgLoaded(sender, e, dataGrid));
            novipanel.Children.Add(labela);
            novipanel.Children.Add(dataGrid);
            myStackPanel.Children.Add(novipanel);
        }

        private void prikazUlogiraniKorisnik(Korisnik korisnik)
        {
            popuniComboBoxeve();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis();
            if(myStackPanel.Children.Count == 0)
            {
                
                myStackPanel.Children.Add(dataGridGlavni);
                dataGridGlavni.ItemsSource = kucanskiPosaoServis.PrikaziPoslove(korisnik);
                dataGridGlavni.Loaded += new RoutedEventHandler((sender, e) => dgLoaded(sender, e, dataGridGlavni));
                return;
            }
            else
            {
                dataGridGlavni.ItemsSource = kucanskiPosaoServis.PrikaziPoslove(korisnik);
                return;
            }
            
        }

        private void BtnResetiraj_Click(object sender, RoutedEventArgs e)
        {
            prikazUlogiraniKorisnik(new KorisnikServis().DohvatiTrenutnogKorisnika());
            dgLoaded(sender, e, dataGridGlavni);
        }

        private void btnObavijest_Click(object sender, RoutedEventArgs e)
        {
            if(cmbMinute.SelectedIndex != 0 && cmbSati.SelectedIndex != 0 && dataGridGlavni.SelectedItem as Kucanski_posao != null)
            {
                var vrijeme = DateTime.Today.AddHours(double.Parse((string)cmbSati.SelectedItem)).AddMinutes(double.Parse((string)cmbMinute.SelectedItem));
                if(vrijeme > DateTime.Now)
                {
                    var servis = new KucanskiPosaoServis();
                    servis.PostaviObavijest(vrijeme, (Kucanski_posao)dataGridGlavni.SelectedItem);
                    MessageBox.Show("Obavijest je postavljena!");
                }
                else
                {
                    MessageBox.Show("To vrijeme je već prošlo!");
                }
                
            }
            else
            {
                MessageBox.Show("Odaberi posao i vrijeme!");
            }
        }

        private void btnRijesiPosao_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridGlavni.SelectedItem != null)
            {
                KorisnikServis servis = new KorisnikServis();
                KucanskiPosaoServis KPservis = new KucanskiPosaoServis();
                Kucanski_posao posao = dataGridGlavni.SelectedItem as Kucanski_posao;
                if (servis.ProvjeriKorisnika())
                {
                    KPservis.RijesiPosao(posao);
                    _sadrzaj.Content = new FormaZaPrikazPoslova(_sadrzaj);
                }
                else
                {
                    string status = KPservis.ProvjeriStatusPosla(posao);
                    switch (status)
                    {
                        case "na_cekanju":
                            {
                                MessageBox.Show("Korisnik tipa dijete ne može dovršiti posao koji je na čekanju.");
                                break;
                            }
                        case "nedovrsen":
                            {
                                KPservis.StaviPosaoNaCekanje(posao);
                                _sadrzaj.Content = new FormaZaPrikazPoslova(_sadrzaj);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("Greška kod rješavanja posla.");
                                break;
                            }
                    }
                }
            }
            else
            {
                MessageBox.Show("Prvo odaberite posao!");
            }
        }

        private void btnDodajPosao_Click(object sender, RoutedEventArgs e)
        {
            KorisnikServis servis = new KorisnikServis();
            if (servis.ProvjeriKorisnika())
            {
                _sadrzaj.Content = new FormaZaDodavanjePosla(_sadrzaj);
            }
            else
            {
                MessageBox.Show("Samo korisnik tipa roditelj može dodavati poslove.");
            }
        }

        private void btnUrediBrisi_Click(object sender, RoutedEventArgs e)
        {

            if (dataGridGlavni.SelectedItem != null)
            {
                KorisnikServis servis = new KorisnikServis();
                Kucanski_posao posao = dataGridGlavni.SelectedItem as Kucanski_posao;
                if (servis.ProvjeriKorisnika())
                {
                    _sadrzaj.Content = new FormaZaUredivanjeBrisanje(_sadrzaj, posao);
                }
                else
                {
                    MessageBox.Show("Samo korisnik tipa roditelj može uređivati/brisati poslove.");
                }
            }
            else
            {
                MessageBox.Show("Prvo odaberite posao!");
            }
        }
    }
}
