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
    /// Interaction logic for FormaZaPrijavu.xaml
    /// </summary>
    public partial class FormaZaPrijavu : UserControl
    {
        ContentControl contentControl;
        public FormaZaPrijavu(ContentControl sadrzaj)
        {
            InitializeComponent();
            contentControl = sadrzaj;
        }

        private void BtnPrijava_Click(object sender, RoutedEventArgs e)
        {
            if (ProvjeriPostojanjePodataka())
            {
                KorisnikServis servis = new KorisnikServis();
                if(servis.ProvjeriIspravnostPodataka(TxtKorime.Text, TxtLozinka.Password))
                {
                    UspjesanLogin();
                }else MessageBox.Show("Neispravno korisničko ime ili lozinka");

            }
            else
            {
                MessageBox.Show("Unesite korisničko ime i lozinku");
            }
        }

        private bool ProvjeriPostojanjePodataka()
        {
            if (TxtKorime.Text == "" || TxtLozinka.Password == "")
            {
                return false;
            }
            else return true;
        }

        private void UspjesanLogin()
        {
            contentControl.Content = new FormaZaPrikazPoslova(contentControl);
        }

        private void BtnFaceLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var servis = new KorisnikServis();
                if (servis.TrenirajLica()) UspjesanLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska!");
            }
        }
    }
}
