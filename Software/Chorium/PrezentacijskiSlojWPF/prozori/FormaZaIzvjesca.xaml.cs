using SlojPoslovneLogike;
using SlojPoslovneLogike.Servisi;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for FormaZaIzvjesca.xaml
    /// </summary>
    public partial class FormaZaIzvjesca : UserControl
    {
        public FormaZaIzvjesca()
        {
            InitializeComponent();
        }

        private void btnGenerirajIzvjestaj_Click(object sender, RoutedEventArgs e)
        {
            if((int)cmbMjesec.SelectedItem <= DateTime.Now.Month || (int)cmbGodina.SelectedItem < DateTime.Now.Year)
            {
                rvIzvjestaj.LocalReport.ReportEmbeddedResource = "PrezentacijskiSlojWPF.MjesecniIzvjestaj.rdlc";
                rvIzvjestaj.LocalReport.DataSources.Clear();
                rvIzvjestaj.RefreshReport();

                var servis = new KucanskiPosaoServis();
                DateTime datum = new DateTime(int.Parse(cmbGodina.SelectedItem.ToString()), int.Parse(cmbMjesec.SelectedItem.ToString()), 1);
                var listaKorisnika = servis.GenerirajGraf(datum);
                var popisPoslova = servis.GenerirajPopisPoslova(datum);

                ucitajPodatke(listaKorisnika, popisPoslova);
            }
            else
            {
                MessageBox.Show("Odabrani mjesec nije validan!");
            }

            

        }

        private void ucitajPodatke(List<KorisnikPoslovi> listaKorisnika, List<KorisnikPosloviTablica> popisPoslova)
        {
            var izvorZaGraf = new Microsoft.Reporting.WinForms.ReportDataSource();
            izvorZaGraf.Name = "DsKorisnikPoslovi";
            izvorZaGraf.Value = listaKorisnika;
            var izvorZaTablicu = new Microsoft.Reporting.WinForms.ReportDataSource();
            izvorZaTablicu.Name = "DsKorisnikPosloviTablica";
            izvorZaTablicu.Value = popisPoslova;
            string trenutniMjesec = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(cmbMjesec.SelectedItem.ToString()));
            string datumSada = trenutniMjesec + " " + cmbGodina.SelectedItem.ToString() + ".";
            var parDatum = new Microsoft.Reporting.WinForms.ReportParameter("ParDatum", datumSada);


            rvIzvjestaj.LocalReport.DataSources.Add(izvorZaGraf);
            rvIzvjestaj.LocalReport.DataSources.Add(izvorZaTablicu);
            rvIzvjestaj.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { parDatum });
            rvIzvjestaj.RefreshReport();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = DateTime.Now.Year; i >= 1970; i--)
            {

                cmbGodina.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {

                cmbMjesec.Items.Add(i);
            }
            cmbGodina.SelectedIndex = 0;
            cmbMjesec.SelectedIndex = 0;
        }
    }
}
