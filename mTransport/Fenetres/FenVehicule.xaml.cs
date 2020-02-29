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
    public partial class FenVehicule : Window
    {
        public int Id = 0;

        public string benneMatriculeMarque { get; set; }

        public string teteMatriculeModele { get; set; }

        public List<Vehicule> ListVehicule { get; set; }

        public List<LoadCombo> ListBenneEngins { get; set; }

        public List<LoadCombo> ListTeteEngins { get; set; }

        public FenVehicule()
        {
            InitializeComponent();
            Loaded += FenVehicule_Loaded;
        }

        private void FenVehicule_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBenneEngins = new List<LoadCombo>();
                ListTeteEngins = new List<LoadCombo>();
                ListBenneEngin();
                ListTeteEngin();
                GriserChamps();
                LoadTabVehicule();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void ListBenneEngin()
        {
            var l = BenneEngin.getAll();
            foreach (var item in l)
            {
                using (DB db = new DB())
                {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                    var ben = db.Vehicules.SingleOrDefault(a => a.IdBenneEngins == item.Id && a.Supprime == false);
                    if (ben == null)
                    {
                        lCombo.Libelle = item.Matricule +"/"+ item.Marque;
                        ListBenneEngins.Add(lCombo);
                    }
                
                }
                
            }
            cmbBenneEngin.ItemsSource = ListBenneEngins;
        }

        public void ListTeteEngin()
        {

            var l = TeteEngin.getAll();
            foreach (var item in l)
            {
                using (DB db = new DB())
                {
                    LoadCombo lCombo = new LoadCombo();
                    lCombo.Id = item.Id;
                    var ben = db.Vehicules.SingleOrDefault(a => a.IdTeteEngins == item.Id && a.Supprime == false);
                    if (ben == null)
                    {
                        lCombo.Libelle = item.Matricule + "/" + item.Modele;
                        ListTeteEngins.Add(lCombo);
                    }
                }
            }
            cmbTeteEngin.ItemsSource = ListTeteEngins;
        }

        private void GriserChamps()
        {
            InitChamps();
            txtLibelle.IsEnabled = false;
            cmbTeteEngin.IsEnabled = false;
            cmbBenneEngin.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnSupprimer.IsEnabled = false;
        }

        private void DegriserChamps()
        {
            txtLibelle.IsEnabled = true;
            cmbTeteEngin.IsEnabled = true;
            cmbBenneEngin.IsEnabled = true;
            BtnValider.IsEnabled = true;
            //BtnAnnuler.IsEnabled = true;
            //BtnSupprimer.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtLibelle.Text = "";
            cmbBenneEngin.SelectedIndex = -1;
            cmbTeteEngin.SelectedIndex = -1;
            Id = 0;

        }

        private void LoadTabVehicule()
        {
            ListVehicule = Vehicule.getAll();
            TabVehicule.ItemsSource = ListVehicule;
        }

        private string concatValue(int id, int cas=1)
        {
            string strValue = "";
            if (cas==1)
            {
                strValue =  BenneEngin.getUnBenneEngin(id).Matricule + ' '+ BenneEngin.getUnBenneEngin(id).Marque ;
            }
            else
            {
                strValue = TeteEngin.getTeteEngin(id).Matricule + ' ' + TeteEngin.getTeteEngin(id).Modele;
            }
            return strValue;
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
                if (string.IsNullOrWhiteSpace(txtLibelle.Text) ||
                    cmbTeteEngin.SelectedIndex == -1 ||
                    cmbBenneEngin.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("Veuillez remplir tous les champs !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Vehicule T = new Vehicule();
                    T.Libelle = txtLibelle.Text;
                    var te = cmbTeteEngin.SelectedItem as LoadCombo;
                    T.IdTeteEngins = te.Id;
                    var be = cmbBenneEngin.SelectedItem as LoadCombo;
                    T.IdBenneEngins = be.Id;
                    if (Id > 0)
                    {
                        T.Id = Id;
                        var k = BenneEngin.getUnBenneEngin(be.Id);
                        T.BenneEngin = k;
                        var m = TeteEngin.getTeteEngin(te.Id);
                        T.TeteEngin = m;
                        T.Update();
                        MessageBox.Show("Modification effectuée !");
                        LoadTabVehicule();
                        emptyCombo();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        T.Insert();
                        var k = BenneEngin.getUnBenneEngin(be.Id);
                        T.BenneEngin = k;
                        var m = TeteEngin.getTeteEngin(te.Id);
                        T.TeteEngin = m;
                        ListVehicule.Add(T);
                        TabVehicule.Items.Refresh();
                        MessageBox.Show("Enregistrement effectué !");
                        emptyCombo();
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
                Vehicule Tab = TabVehicule.SelectedItem as Vehicule;
                var T = Vehicule.getVehicule(Tab.Id);
                Id = T.Id;
                txtLibelle.Text = T.Libelle;
                var c = ListBenneEngins.SingleOrDefault(a => a.Id == T.IdBenneEngins);
                int val = ListBenneEngins.IndexOf(c);
                cmbBenneEngin.SelectedIndex = val;
                var k = ListTeteEngins.SingleOrDefault(a => a.Id == T.IdTeteEngins);
                int l = ListTeteEngins.IndexOf(k);
                cmbTeteEngin.SelectedIndex = l;
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
                if (TabVehicule.SelectedIndex != -1)
                {
                    Vehicule Tab = (Vehicule)TabVehicule.SelectedItem;
                    var T = Vehicule.getVehicule(Tab.Id);
                    T.Supprime = true;
                    T.Delete();
                    LoadTabVehicule();
                    InitChamps();
                    emptyCombo();
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
                if (TabVehicule.SelectedIndex != -1)
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

        public void emptyCombo()
        {
            cmbBenneEngin.ItemsSource = null;
            cmbTeteEngin.ItemsSource = null;
            ListBenneEngins.Clear();
            ListTeteEngins.Clear();
            ListBenneEngin();
            ListTeteEngin();
        }
    }

    
}
