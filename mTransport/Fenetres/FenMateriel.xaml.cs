using mTransport.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace mTransport.Fenetres
{
    /// <summary>
    /// Logique d'interaction pour FenMateriel.xaml
    /// </summary>
    public partial class FenMateriel : Window
    {
       public static List<Materiel> ListMateriel { get; set; }

        public FenMateriel()
        {
            InitializeComponent();
            Loaded += FenMateriel_Loaded;
        }

        private void FenMateriel_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListMateriel = new List<Materiel>();
                GriserChamps();
                txtDatePeremption.SelectedDate = DateTime.Now;
                LoadCmbMateriel();
                LoadTabMateriel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GriserChamps()
        {
            InitChamps();
            txtDesignation.IsEnabled = false;
            txtDatePeremption.IsEnabled = false;
            cmbEtatMateriel.IsEnabled = false;
            txtQte.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnAnnuler.IsEnabled = false;

        }

        private void DegriserChamps()
        {
            txtDesignation.IsEnabled = true;
            txtDatePeremption.IsEnabled = true;
            cmbEtatMateriel.IsEnabled = true;
            txtQte.IsEnabled = true;
            BtnValider.IsEnabled = true;
            BtnAnnuler.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtDesignation.Text = "";
            txtDatePeremption.SelectedDate = DateTime.Now;
            cmbEtatMateriel.SelectedItem = -1;
            txtQte.Text = "";
        }

        private void LoadTabMateriel()
        {
            ListMateriel = Materiel.getAll();
            TabMateriel.ItemsSource = ListMateriel;
        }

        //Charger la combo Matériel; la liste est dans le fichier app.config
        private void LoadCmbMateriel()
        {
            var l = ConfigurationManager.AppSettings["ListMat"];
            var ListEtatMateriel =  l.Split(';').ToList();
            cmbEtatMateriel.ItemsSource = ListEtatMateriel;
        }

        private void BtnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DegriserChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDesignation.Text) ||
                    string.IsNullOrWhiteSpace(txtQte.Text) ||
                    string.IsNullOrWhiteSpace(txtDatePeremption.Text) ||
                    cmbEtatMateriel.SelectedIndex == -1)
                {
                    MessageBox.Show("Veuillez remplir tous les champs !","mTransport",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else
                {
                    Materiel M = new Materiel();
                    M.Designation = txtDesignation.Text;
                    M.QteStock = int.Parse(txtQte.Text);
                    M.Etat = cmbEtatMateriel.SelectedItem.ToString();
                    M.DatePeremption = txtDatePeremption.SelectedDate;
                    M.Insert();
                    ListMateriel.Add(M);
                    TabMateriel.Items.Refresh();
                    MessageBox.Show("Enregistrement effectué !");
                    GriserChamps();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
