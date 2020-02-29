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
    /// Logique d'interaction pour FenBenneEngin.xaml
    /// </summary>
    public partial class FenHistoriquePanne : Window
    {
        public static List<HistoriquePanne> ListHistorique { get; set; }
        public static List<LoadCombo> ListVehicules { get; set; }
        public static List<LoadCombo> ListMateriel { get; set; }
        public int Id = 0;

        public FenHistoriquePanne()
        {
            InitializeComponent();
            Loaded += FenHistoriquePanne_Loaded;
        }

        private void FenHistoriquePanne_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListHistorique = new List<HistoriquePanne>();
                ListVehicules = new List<LoadCombo>();
                ListMateriel = new List<LoadCombo>();
                GriserChamps();
                txtDateReparation.SelectedDate = DateTime.Now;
                LoadVehicules();
                LoadCmbMateriel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTabHistorique()
        {
            ListHistorique = HistoriquePanne.getAll();
            TabHistorique.ItemsSource = ListHistorique;
        }
        



        private void GriserChamps()
        {
            InitChamps();
            txtDateReparation.IsEnabled = false;
            cmbMateriel.IsEnabled = false;
            cmbVehicule.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnAnnuler.IsEnabled = false;

        }

        private void DegriserChamps()
        {
            txtDateReparation.IsEnabled = true;
            cmbMateriel.IsEnabled = true;
            cmbVehicule.IsEnabled = true;
            BtnValider.IsEnabled = true;
            BtnAnnuler.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtDateReparation.SelectedDate = DateTime.Now;
            cmbMateriel.SelectedIndex = -1;
            cmbVehicule.SelectedIndex = -1;
        }

        private void LoadVehicules()
        {
            var l = Vehicule.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.TeteEngin.Matricule + " " +item.BenneEngin.Matricule;
                ListVehicules.Add(lCombo);
            }
            cmbVehicule.ItemsSource = ListVehicules;
        }

        //Charger la combo Matériel; la liste est dans le fichier app.config
        private void LoadCmbMateriel()
        {
            var l = Materiel.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.Designation;
                ListMateriel.Add(lCombo);
            }
            cmbMateriel.ItemsSource = ListMateriel;
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
                if (txtDateReparation.SelectedDate == null ||
                    cmbVehicule.SelectedIndex == -1 ||
                    cmbMateriel.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("Veuillez remplir tous les champs !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var mat = cmbMateriel.SelectedItem as LoadCombo;
                    var veh = cmbVehicule.SelectedItem as LoadCombo;

                    HistoriquePanne T = new HistoriquePanne();
                    T.IdMateriel = mat.Id;
                    T.IdVehicule = veh.Id;
                    T.DateReparation = txtDateReparation.SelectedDate;
                    T.Description = txtDescription.Text;

                    
                    if (Id > 0)
                    {
                        T.Id = Id;
                        var k = Materiel.getMateriel(mat.Id);
                        T.Materiel = k;
                        var m = Vehicule.getVehicule(veh.Id);
                        //T.V = m;
                        T.Update();
                        MessageBox.Show("Modification effectuée !");
                        LoadTabHistorique();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        T.Insert();
                        var k = Materiel.getMateriel(mat.Id);
                        T.Materiel = k;
                        var m = Vehicule.getVehicule(veh.Id);
                        //T.V = m;
                        ListHistorique.Add(T);
                        TabHistorique.Items.Refresh();
                        MessageBox.Show("Enregistrement effectué !");
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
                HistoriquePanne Tab = TabHistorique.SelectedItem as HistoriquePanne;
                var T = HistoriquePanne.getHistoriquePanne(Tab.Id);
                Id = T.Id;
                txtDateReparation.SelectedDate = T.DateReparation.Value;
                var c = ListMateriel.SingleOrDefault(a => a.Id == T.IdMateriel);
                int val = ListMateriel.IndexOf(c);
                cmbMateriel.SelectedIndex = val;
                var k = ListVehicules.SingleOrDefault(a => a.Id == T.IdVehicule);
                int l = ListVehicules.IndexOf(k);
                cmbVehicule.SelectedIndex = l;
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
            InitChamps();
            BtnModifier.IsEnabled = false;
            BtnSupprimer.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnValider.IsEnabled = false;
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TabHistorique.SelectedIndex != -1)
                {
                    HistoriquePanne Tab = (HistoriquePanne)TabHistorique.SelectedItem;
                    var T = Vehicule.getVehicule(Tab.Id);
                    T.Supprime = true;
                    T.Delete();
                    LoadTabHistorique();
                    InitChamps();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnImprimer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
