using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml;

namespace Labo5_UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AjouterUtilisation_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument dom = new XmlDocument();
            dom.Load("residence.xml");
            var racine = dom.SelectSingleNode("/residence/utilisations");
            XmlElement noeudUtilisation = dom.CreateElement("utilisation");
            XmlElement noeudNbHeures = dom.CreateElement("nbHeures");
            XmlElement noeudNom = dom.CreateElement("nom");
            XmlElement noeudPuissanceWatts = dom.CreateElement("puissanceWatts");
            noeudNbHeures.AppendChild(dom.CreateTextNode("1.5"));
            noeudNom.AppendChild(dom.CreateTextNode("Ampoule"));
            noeudPuissanceWatts.AppendChild(dom.CreateTextNode("60"));
            noeudUtilisation.AppendChild(noeudNbHeures);
            noeudUtilisation.AppendChild(noeudNom);
            noeudUtilisation.AppendChild(noeudPuissanceWatts);
            racine.AppendChild(noeudUtilisation);
            dom.Save("residence.xml");
        }

        private void CalculerDureeAmpoules_Click(object sender, RoutedEventArgs e)
        {
            using (XmlReader reader = XmlReader.Create("residence.xml"))
            {
                string chaine = "";
                string nom = "";
                decimal nbHeures = 0.0m;
                decimal total = 0.0m;

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            break;
                        case XmlNodeType.Text:
                            chaine = reader.Value.Trim();
                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Name.Equals("nom"))
                                nom = chaine;
                            if (reader.Name.Equals("nbHeures"))
                                nbHeures = Decimal.Parse(chaine);
                            if (reader.Name.Equals("utilisation"))
                            {
                                if (nom == "Ampoule")
                                {
                                    total += nbHeures;
                                }
                            }
                            break;
                    }
                }
                tbDureeTotale.Text = total.ToString();
            }
        }
    }
}