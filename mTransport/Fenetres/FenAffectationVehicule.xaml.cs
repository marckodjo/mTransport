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
    /// Logique d'interaction pour FenVehicule.xaml
    /// </summary>
    public partial class FenAffectationVehicule : Window
    {
        public int Id = 0;

        public List<AffectationVehicule> ListAffectationVehicule { get; set; }

        public List<LoadCombo> ListVehicules { get; set; }

        public List<LoadCombo> ListChauffeurs { get; set; }

        public void ListVehicule()
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
        public void ListChauffeur()
        {
            var l = Chauffeur.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.NomChauffeur+" "+ item.PrenomChauffeur;
                ListChauffeurs.Add(lCombo);
            }
            cmbChauffeur.ItemsSource = ListChauffeurs;
        }

        public FenAffectationVehicule()
        {
            InitializeComponent();
            Loaded += FenAffectationVehicule_Loaded; ;
        }

        private void FenAffectationVehicule_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListAffectationVehicule = new List<AffectationVehicule>();
                ListVehicules = new List<LoadCombo>();
                ListChauffeurs = new List<LoadCombo>();
                GriserChamps();
                txtDateDbuAffectation.SelectedDate = DateTime.Now;
                txtDateFinAffectation.SelectedDate = DateTime.Now;
                ListChauffeur();
                ListVehicule();
                LoadTabAffectationVehicule();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GriserChamps()
        {
            InitChamps();
            cmbChauffeur.IsEnabled = false;
            cmbVehicule.IsEnabled = false;
            txtDateDbuAffectation.IsEnabled = false;
            txtDateFinAffectation.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnSupprimer.IsEnabled = false;
        }

        private void DegriserChamps()
        {
            cmbChauffeur.IsEnabled = true;
            cmbVehicule.IsEnabled = true;
            txtDateDbuAffectation.IsEnabled = true;
            txtDateFinAffectation.IsEnabled = true;
            BtnValider.IsEnabled = true;
            //BtnAnnuler.IsEnabled = true;
            //BtnSupprimer.IsEnabled = true;
        }

        private void InitChamps()
        {
            cmbVehicule.SelectedIndex = -1;
            cmbChauffeur.SelectedIndex = -1;
            txtDateDbuAffectation.SelectedDate = DateTime.Now;
            txtDateFinAffectation.SelectedDate = DateTime.Now;
            Id = 0;
        }

        private void LoadTabAffectationVehicule()
        {
            ListAffectationVehicule = AffectationVehicule.getAll();
            TabAffectationVehicule.ItemsSource = ListAffectationVehicule;
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
                if (string.IsNullOrWhiteSpace(txtDateDbuAffectation.Text) ||
                    string.IsNullOrWhiteSpace(txtDateFinAffectation.Text) ||
                    cmbChauffeur.SelectedIndex == -1 ||
                    cmbVehicule.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("Veuillez remplir tous les champs !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if(Outils.VerifDate((DateTime)txtDateDbuAffectation.SelectedDate, (DateTime)txtDateFinAffectation.SelectedDate, "date début affectation", "date fin affectation"))
                    {
                        return;
                    }

                    AffectationVehicule T = new AffectationVehicule();
                    T.DateDebutAffectation = (DateTime)txtDateDbuAffectation.SelectedDate;
                    T.DateFinAffectation = txtDateFinAffectation.SelectedDate;
                    var ch = cmbChauffeur.SelectedItem as LoadCombo;
                    T.IdChauffeur = ch.Id;
                    var ve = cmbVehicule.SelectedItem as LoadCombo;
                    T.IdVehicule = ve.Id;

                    if (Outils.VerifDateExist(ch.Id, ve.Id, (DateTime)txtDateDbuAffectation.SelectedDate))
                    {
                        MessageBox.Show("Ce véhicule est déjà affecté au chauffeur sur la même période!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    if (Id > 0)
                    {
                        T.Id = Id;
                        var k = Vehicule.getVehicule(ve.Id);
                        T.Vehicule = k;
                        var m = Chauffeur.getUnChauffeur(ch.Id);
                        T.Chauffeur = m;
                        T.Update();
                        MessageBox.Show("Modification effectuée !");
                        LoadTabAffectationVehicule();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        T.Insert();
                        var k = Vehicule.getVehicule(ve.Id);
                        T.Vehicule = k;
                        var m = Chauffeur.getUnChauffeur(ch.Id);
                        T.Chauffeur = m;
                        ListAffectationVehicule.Add(T);
                        TabAffectationVehicule.Items.Refresh();
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
                AffectationVehicule Tab = TabAffectationVehicule.SelectedItem as AffectationVehicule;
                var T = AffectationVehicule.getAffectationVehicule(Tab.Id);
                Id = T.Id;
                txtDateDbuAffectation.SelectedDate = T.DateDebutAffectation;
                txtDateFinAffectation.SelectedDate = T.DateFinAffectation;
                var c = ListChauffeurs.SingleOrDefault(a => a.Id == T.IdChauffeur);
                int val = ListChauffeurs.IndexOf(c);
                cmbChauffeur.SelectedIndex = val;
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
                if (TabAffectationVehicule.SelectedIndex != -1)
                {
                    Vehicule Tab = (Vehicule)TabAffectationVehicule.SelectedItem;
                    var T = Vehicule.getVehicule(Tab.Id);
                    T.Supprime = true;
                    T.Delete();
                    LoadTabAffectationVehicule();
                    InitChamps();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabVehicule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TabAffectationVehicule.SelectedIndex != -1)
                {
                    BtnModifier.IsEnabled = true;
                    BtnSupprimer.IsEnabled = true;
                    BtnAnnuler.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
