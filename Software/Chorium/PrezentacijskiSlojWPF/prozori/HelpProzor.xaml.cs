using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps.Packaging;

namespace PrezentacijskiSlojWPF.prozori
{
    /// <summary>
    /// Interaction logic for HelpProzor.xaml
    /// </summary>
    public partial class HelpProzor : UserControl
    {
        public HelpProzor()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            XpsDocument xps = new XpsDocument(Directory.GetCurrentDirectory() + @"\dokumentacija\korisnicka_dokumentacija.xps", System.IO.FileAccess.Read);

            dokument.Document = xps.GetFixedDocumentSequence();
        }
    }
}
