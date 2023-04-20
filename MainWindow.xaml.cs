using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Tanulo> adatok;
        public MainWindow()
        {
            InitializeComponent();
      

        }


        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {

            adatok = new List<Tanulo>();
            foreach (var item in File.ReadAllLines("naplo.txt", Encoding.UTF8))
            {
                adatok.Add(new Tanulo(item));
            }
            dataGrid.ItemsSource = adatok;
        }


            internal class Tanulo
        {
            public string Nev { get; set; }
            public string Osztaly { get; set; }
            public char Nem { get; set; }
            public double Atlag { get; set; }

            public Tanulo(string sor)
            {
                string[] resz = sor.Split(';');
                Nev = resz[0];
                Osztaly = resz[1];
                Nem = Convert.ToChar(resz[2]);
                Atlag = Convert.ToDouble(resz[3]);
            }
        }

        private void btnOsszatlag_Click(object sender, RoutedEventArgs e)
        {
            double osszeg = 0;
            int darab = 0;
            foreach (Tanulo tanulo in adatok)
            {
                osszeg += tanulo.Atlag;
                darab += 1;
            }

            lbOsszatlag.Content= osszeg / darab;
        }

        private void btnLetszam_Click(object sender, RoutedEventArgs e)
        {
            int letszam = 0;
            foreach (Tanulo tanulo in adatok)
            {
                if (txtOsztaly.Text==tanulo.Osztaly)
                {
                    letszam += 1;
                }
            }
            lbLetszam.Content = letszam;
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter("Figyelmeztetesek.txt", append: true);
            foreach (Tanulo tanulo in adatok)
            {
                if (tanulo.Atlag <2.0)
                {
                    sw.WriteLine(tanulo.Nev + "" + tanulo.Atlag);
                }
            }
        }

        private void btnKivalo_Click(object sender, RoutedEventArgs e)
        {
            int darab = 0;
            foreach (Tanulo tanulo in adatok)
            {
                if (tanulo.Atlag == 5.0)
                {
                    lbxOtos.Items.Add(tanulo.Nev);
                    darab += 1;
                }
            }
            lbOtosSzam.Content = darab;
        }
    }
}
