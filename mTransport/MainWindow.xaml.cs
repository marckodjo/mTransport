using mTransport.Fenetres;
using mTransport.Models;
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

namespace mTransport
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

        private void mnuMateriel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenMateriel F = new FenMateriel();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void mnuTypeDepense_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenTypeDepense F = new FenTypeDepense();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuTeteEngin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenTeteEngin F = new FenTeteEngin();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MnuBenneEngin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenBenneEngin F = new FenBenneEngin();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuChauffeur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenChauffeur F = new FenChauffeur();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuVoiture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenVehicule F = new FenVehicule();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuAffectationVehicule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenAffectationVehicule F = new FenAffectationVehicule();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuVoyage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenVoyage F = new FenVoyage();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuRaisonSociale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenRaisonSociale F = new FenRaisonSociale();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuPanne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenHistoriquePanne F = new FenHistoriquePanne();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuTypeBenne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenTypeBenneEngin F = new FenTypeBenneEngin();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenUtilisateur F = new FenUtilisateur();
                F.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
