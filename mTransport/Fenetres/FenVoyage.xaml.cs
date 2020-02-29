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
using System.Windows.Shapes;

namespace mTransport.Fenetres
{
    /// <summary>
    /// Logique d'interaction pour FenVoyage.xaml
    /// </summary>
    public partial class FenVoyage : Window
    {
        public int Id = 0;
        public List<LoadCombo> ListVehicules { get; set; }
        public List<Voyage> ListVoyage { get; set; }
        public FenVoyage()
        {
            InitializeComponent();
            Loaded += FenVoyage_Loaded;
        }
        private void FenVoyage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListVehicules = new List<LoadCombo>();
                ListVoyage = new List<Voyage>();
                ListCmbVehicules();
                LoadTabVoyage();
                GriserChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void ListCmbVehicules()
        {

            var l = Vehicule.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.Libelle;
                ListVehicules.Add(lCombo);
            }
            cmbVehicule.ItemsSource = ListVehicules;
        }
        private void LoadTabVoyage()
        {
            ListVoyage = Voyage.getAll();
            TabVoyage.ItemsSource = ListVoyage;
        }
        private void GriserChamps()
        {
            InitChamps();
            txtDesignation.IsEnabled = false;
            txtDepart.IsEnabled = false;
            txtDestination.IsEnabled = false;
            txtDateDepartPrevue.IsEnabled = false;
            txtDateArriveePrevue.IsEnabled = false;
            txtDescription.IsEnabled = false;
            cmbVehicule.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnSupprimer.IsEnabled = false;
            BtnDepenseVoyage.IsEnabled = false;
        }
        private void DegriserChamps()
        {
            txtDesignation.IsEnabled = true;
            txtDepart.IsEnabled = true;
            txtDestination.IsEnabled = true;
            txtDateDepartPrevue.IsEnabled = true;
            txtDateArriveePrevue.IsEnabled = true;
            txtDescription.IsEnabled = true;
            cmbVehicule.IsEnabled = true;
            BtnValider.IsEnabled = true;
            //BtnAnnuler.IsEnabled = true;
            //BtnSupprimer.IsEnabled = true;
        }
        private void InitChamps()
        {
            txtDesignation.Text = "";
            txtDepart.Text = "";
            txtDestination.Text = "";
            txtDateDepartPrevue.SelectedDate = DateTime.Now;
            txtDateArriveePrevue.SelectedDate = DateTime.Now;
            txtDescription.Text = "";
            cmbVehicule.SelectedIndex = -1;
            Id = 0;
        }
        private void BtnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DegriserChamps();
                InitChamps();
                BtnModifier.IsEnabled = false;
                BtnSupprimer.IsEnabled = false;
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
                    string.IsNullOrWhiteSpace(txtDescription.Text) ||
                    string.IsNullOrWhiteSpace(txtDestination.Text) ||
                    string.IsNullOrWhiteSpace(txtDateArriveePrevue.Text) ||
                    string.IsNullOrWhiteSpace(txtDateDepartPrevue.Text) ||
                    string.IsNullOrWhiteSpace(txtDepart.Text) ||
                    cmbVehicule.SelectedIndex == -1)
                {
                    Outils.BoxMessage("C");
                }
                else
                {
                    if (Outils.VerifDate((DateTime)txtDateDepartPrevue.SelectedDate, (DateTime)txtDateArriveePrevue.SelectedDate, "date départ prévue", "date"))
                    {
                        return;
                    }

                    

                    Voyage T = new Voyage();
                    T.Designation = txtDesignation.Text;
                    T.Depart=txtDepart.Text;
                    T.Destination=txtDestination.Text;
                    T.DateDepartPrevue = (DateTime)txtDateDepartPrevue.SelectedDate;
                    T.DateArriveePrevue = (DateTime)txtDateArriveePrevue.SelectedDate;
                    T.Description = txtDescription.Text;
                    var c = cmbVehicule.SelectedItem as LoadCombo;
                    T.IdVehicule = c.Id;

                    if (FenVoyage.checkTravelDateOfVehicleSelected((DateTime)txtDateDepartPrevue.SelectedDate, (DateTime)txtDateArriveePrevue.SelectedDate,c.Id))
                    {
                        MessageBox.Show("Le véhicule sélectionné est déjà enregistrer sur un voyage pour les dates du " + txtDateDepartPrevue.SelectedDate + " au " + txtDateArriveePrevue.SelectedDate + " !!!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (Id > 0)
                    {
                        T.Id = Id;
                        T.Update();
                        Outils.BoxMessage("M");
                        LoadTabVoyage();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        T.Insert();
                        var b = Vehicule.getVehicule(c.Id);
                        T.Vehicule = b;
                        ListVoyage.Add(T);
                        TabVoyage.Items.Refresh();
                        Outils.BoxMessage("A");
                        GriserChamps();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Voyage Tab = TabVoyage.SelectedItem as Voyage;
                var T = Voyage.getVoyage(Tab.Id);
                Id = T.Id;
                txtDesignation.Text = T.Designation;
                txtDepart.Text = T.Depart;
                txtDestination.Text = T.Destination;
                txtDateDepartPrevue.SelectedDate = T.DateDepartPrevue;
                txtDateArriveePrevue.SelectedDate = T.DateArriveePrevue;
                txtDescription.Text = T.Description;
                var c = ListVehicules.SingleOrDefault(a => a.Id == T.IdVehicule);
                int val = ListVehicules.IndexOf(c);
                cmbVehicule.SelectedIndex = val;
                BtnNouveau.IsEnabled = false;
                DegriserChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InitChamps();
                BtnModifier.IsEnabled = false;
                BtnSupprimer.IsEnabled = false;
                BtnNouveau.IsEnabled = true;
                BtnValider.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TabVoyage.SelectedIndex != -1)
                {
                    Voyage Tab = (Voyage)TabVoyage.SelectedItem;
                    var T = Voyage.getVoyage(Tab.Id);
                    T.Supprime = true;
                    T.Delete();
                    LoadTabVoyage();
                    GriserChamps();
                    InitChamps();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TabVoyage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TabVoyage.SelectedIndex != -1)
                {
                    BtnModifier.IsEnabled = true;
                    BtnSupprimer.IsEnabled = true;
                    BtnAnnuler.IsEnabled = true;
                    BtnDepenseVoyage.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnRechercher_Click(object sender, RoutedEventArgs e)
        {
            List<Voyage> listRecherche = new List<Voyage>() ;

            if (txtDateDepartRecherche.Text != "" && txtDateArriveRecherche.Text != "")
            {
                listRecherche = ListVoyage.Where(o => o.DateDepartPrevue >= txtDateDepartRecherche.SelectedDate && o.DateArriveePrevue <= txtDateArriveRecherche.SelectedDate).ToList();
                TabVoyage.ItemsSource = listRecherche;
            }else if (txtDateDepartRecherche.Text != "" && txtDateArriveRecherche.Text == "")
            {
                listRecherche = ListVoyage.Where(o => o.DateDepartPrevue == txtDateDepartRecherche.SelectedDate).ToList();
                TabVoyage.ItemsSource = listRecherche;
            }else if (txtDateDepartRecherche.Text == "" && txtDateArriveRecherche.Text != "")
            {
                listRecherche = ListVoyage.Where(o => o.DateArriveePrevue == txtDateArriveRecherche.SelectedDate).ToList();
                TabVoyage.ItemsSource = listRecherche;
            }
            else
            {
                MessageBox.Show("Choisissez une date de recherche !!!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void BtnDepenseVoyage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Voyage Tab = TabVoyage.SelectedItem as Voyage;
                var T = Voyage.getVoyage(Tab.Id);

                FenDepenseVoyage FenDepenseVoyage = new FenDepenseVoyage();
                FenDepenseVoyage.getVehiculeId(T.Id);
                FenDepenseVoyage.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static bool checkTravelDateOfVehicleSelected(DateTime dateDepart, DateTime dateArrivee, int idVehicule)
        {
            bool boul = false;
            using (DB db = new DB())
            {
                //dateDepart>dateDépartDB ET dateDepart<dateArriveeDB ET dateArrivee<dateArriveeDB
                var data = db.Voyages.Where(o => o.IdVehicule == idVehicule);
                var data1 = data.Where(e => e.DateDepartPrevue <= dateDepart && dateDepart < e.DateArriveePrevue && dateArrivee < e.DateArriveePrevue);
                if (data1 == null)
                {
                    var data2 = data.Where(o => o.DateDepartPrevue <= dateDepart && dateArrivee < o.DateArriveePrevue);
                    if (data2 == null)
                    {
                        boul  = false;
                    }
                    boul = false;
                }
                return boul;
            }
        }
    }
}
