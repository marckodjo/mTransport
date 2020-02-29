using FastReport;
using mTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public partial class FenBenneEngin : Window
    {
        public List<LoadCombo> ListCmbTypeBenneEngins { get; set; }
        public List<BenneEngin> ListBenneEngin { get; set; }
        public int Id = 0;
        public FenBenneEngin()
        {
            InitializeComponent();
            Loaded += FenBenneEngin_Loaded;
           
        }

        private void FenBenneEngin_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                ListCmbTypeBenneEngins = new List<LoadCombo>();
                ListBenneEngin = new List<BenneEngin>();
                ListTypeBenneEngin();
                GriserChamps();
                LoadTabBenneEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ListTypeBenneEngin()
        {           
            var l = TypeBenneEngin.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.Libelle;
                ListCmbTypeBenneEngins.Add(lCombo);
            }
            cmbTypeBenne.ItemsSource = ListCmbTypeBenneEngins;
        }

        private void GriserChamps()
        {
            InitChamps();
            txtMatricule.IsEnabled = false;
            txtMarque.IsEnabled = false;
            txtAnneeAchat.IsEnabled = false;
            txtCapacite.IsEnabled = false;
            cmbTypeBenne.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnSupprimer.IsEnabled = false;
        }

        private void DegriserChamps()
        {
            //txtMatricule.IsEnabled = true;
            txtMarque.IsEnabled = true;
            txtAnneeAchat.IsEnabled = true;
            txtCapacite.IsEnabled = true;
            cmbTypeBenne.IsEnabled = true;
            BtnValider.IsEnabled = true;
            //BtnAnnuler.IsEnabled = true;
            //BtnSupprimer.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtMatricule.Text = "";
            txtCapacite.Text = "";
            txtMarque.Text = "";
            txtAnneeAchat.Text = "";
            cmbTypeBenne.SelectedIndex = -1;
            Id = 0;
        }

        private void BtnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DegriserChamps();
                InitChamps();
                txtMatricule.Text = Outils.Generator("BenneEngins");
                BtnModifier.IsEnabled = false;
                BtnSupprimer.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTabBenneEngin()
        {
            ListBenneEngin = BenneEngin.getAll();
            TabBenneEngin.ItemsSource = ListBenneEngin;
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtMatricule.Text) ||
                        string.IsNullOrWhiteSpace(txtMarque.Text) ||
                        string.IsNullOrWhiteSpace(txtCapacite.Text) ||
                        string.IsNullOrWhiteSpace(txtAnneeAchat.Text))
                    {
                        MessageBox.Show("Veuillez remplir tous les champs !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        BenneEngin T = new BenneEngin();
                        T.Matricule = txtMatricule.Text;
                        T.Marque = txtMarque.Text;
                        T.AnneeAchat = int.Parse(txtAnneeAchat.Text);
                        T.Capacite = int.Parse(txtCapacite.Text);
                        var c = cmbTypeBenne.SelectedItem as LoadCombo;
                        T.IdTypeBenne = c.Id;
                        if (Id > 0)
                        {
                            T.Id = Id;
                            T.Update();
                            MessageBox.Show("Modification effectuée !");
                            LoadTabBenneEngin();
                            GriserChamps();
                            Id = 0;
                        }
                        else
                        {
                            T.Insert();
                            var b = TypeBenneEngin.getUnTypeBenneEngin(c.Id);
                            T.TypeBenneEngin = b;
                            ListBenneEngin.Add(T);
                            TabBenneEngin.Items.Refresh();
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
                BenneEngin Tab = TabBenneEngin.SelectedItem as BenneEngin;
                var T = BenneEngin.getUnBenneEngin(Tab.Id);
                Id = T.Id;
                txtMatricule.Text = T.Matricule;
                txtMarque.Text = T.Marque;
                txtCapacite.Text = T.Capacite.ToString();
                txtAnneeAchat.Text = T.AnneeAchat.ToString();
                var c = ListCmbTypeBenneEngins.SingleOrDefault(a => a.Id == T.IdTypeBenne);
                int val = ListCmbTypeBenneEngins.IndexOf(c);
                cmbTypeBenne.SelectedIndex = val;
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
                if (TabBenneEngin.SelectedIndex != -1)
                {
                    BenneEngin Tab = (BenneEngin)TabBenneEngin.SelectedItem;
                    var T = BenneEngin.getUnBenneEngin(Tab.Id);
                    T.Supprime = true;
                    T.Delete();
                    LoadTabBenneEngin();
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
            using (Report report = new Report())
            {
                string AppFolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "EnteteEtat.frx"));
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "rptBenneEngins.frx"));
                report.RegisterData(ListBenneEngin, "LstData");
                report.GetDataSource("LstData").Enabled = true;
                List<RaisonSociale> LstRs = new List<RaisonSociale>();
                LstRs = RaisonSociale.getAll();
                report.RegisterData(LstRs, "rs");
                report.GetDataSource("rs").Enabled = true;
                var x = report.Pages[0].ChildObjects[3];
                ((DataBand)x).DataSource = report.GetDataSource("rs");
                ((DataBand)x).DataSource = report.GetDataSource("LstData");

                if (RaisonSociale.getRaisonSociale().Logo != null)
                {
                    byte[] logo = Hex.FromHexString(RaisonSociale.getRaisonSociale().Logo);
                    var bLogo = ImageConverter.FromArray(logo);
                    report.SetParameterValue("Logo", bLogo);
                } 
                report.SetParameterValue("ParamTitre", "LISTE DES BENNES ENGINS");
                report.Show();
            }
        }

        private void TabBenneEngin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TabBenneEngin.SelectedIndex != -1)
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
