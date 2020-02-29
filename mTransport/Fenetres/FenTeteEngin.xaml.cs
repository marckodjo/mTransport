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
    /// Logique d'interaction pour FenTeteEngin.xaml
    /// </summary>
    public partial class FenTeteEngin : Window
    {
        public List<TeteEngin> ListTeteEngin { get; set; }
        public int Id = 0;
        public FenTeteEngin()
        {
            InitializeComponent();
            Loaded += FenTeteEngin_Loaded;
        }

        private void FenTeteEngin_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListTeteEngin = new List<TeteEngin>();
                GriserChamps();
                LoadTabTeteEngin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DegriserChamps();
                txtMatricule.Text = Outils.Generator("TeteEngins");
                BtnModifier.IsEnabled = false;
                BtnSupprimer.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Griser dégriser
        private void GriserChamps()
        {
            InitChamps();
            txtMatricule.IsEnabled = false;
            txtModele.IsEnabled = false;
            txtMarque.IsEnabled = false;
            txtNbrePlace.IsEnabled = false;
            txtNoChassis.IsEnabled = false;
            txtAnneeAchat.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnSupprimer.IsEnabled = false;
        }

        private void DegriserChamps()
        {
           // txtMatricule.IsEnabled = true;
            txtModele.IsEnabled = true;
            txtMarque.IsEnabled = true;
            txtNbrePlace.IsEnabled = true;
            txtNoChassis.IsEnabled = true;
            txtAnneeAchat.IsEnabled = true;
            BtnValider.IsEnabled = true;
            //BtnAnnuler.IsEnabled = true;
            //BtnSupprimer.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtMatricule.Text = "";
            txtModele.Text = "";
            txtMarque.Text = "";
            txtNoChassis.Text = "";
            txtNbrePlace.Text = "";
            txtAnneeAchat.Text = "";
            Id = 0;
        }

        private void LoadTabTeteEngin()
        {
            ListTeteEngin = TeteEngin.getAll();
            TabTeteEngin.ItemsSource = ListTeteEngin;
        }
         //Validation des données
        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMatricule.Text) ||
                    string.IsNullOrWhiteSpace(txtMarque.Text) ||
                    string.IsNullOrWhiteSpace(txtModele.Text) ||
                    string.IsNullOrWhiteSpace(txtNbrePlace.Text) ||
                    string.IsNullOrWhiteSpace(txtAnneeAchat.Text) ||
                    string.IsNullOrWhiteSpace(txtNoChassis.Text))
                {
                    Outils.BoxMessage("C");
                }
                else
                {
                   

                    TeteEngin T = new TeteEngin();
                    T.Matricule = txtMatricule.Text;
                    T.NbrePlaces = int.Parse(txtNbrePlace.Text);
                    T.Marque = txtMarque.Text;
                    T.AnneeAchat = int.Parse(txtAnneeAchat.Text);
                    T.Modele = txtModele.Text;
                    T.NoChassis = txtNoChassis.Text;

                    if (Id>0)
                    {
                        T.Id = Id;
                        T.Update();
                        Outils.BoxMessage("M");
                        LoadTabTeteEngin();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        if (Outils.VerifNoChassis(txtNoChassis.Text))
                        {
                            MessageBox.Show("Le No de chassis: " + txtNoChassis.Text + " existe déjà !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                            txtNoChassis.Focus();
                            return;
                        }
                        //T.Matricule = Outils.GenMatricule();
                        T.Insert();
                        ListTeteEngin.Add(T);
                        TabTeteEngin.Items.Refresh();
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

        private void TabTeteEngin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(TabTeteEngin.SelectedIndex != -1)
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

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TeteEngin T = TabTeteEngin.SelectedItem as TeteEngin;
                Id = T.Id;
                txtMatricule.Text = T.Matricule;
                txtModele.Text = T.Modele;
                txtMarque.Text = T.Marque;
                txtNoChassis.Text = T.NoChassis;
                txtNbrePlace.Text = T.NbrePlaces.ToString();
                txtAnneeAchat.Text = T.AnneeAchat.ToString();
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
                if (TabTeteEngin.SelectedIndex != -1)
                {
                    TeteEngin Tab = (TeteEngin)TabTeteEngin.SelectedItem;
                    var T = TeteEngin.getTeteEngin(Tab.Id);
                    T.Supprime = true;
                    T.Delete();
                    LoadTabTeteEngin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtNbrePlace_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Outils.CheckNumber(txtNbrePlace.Text))
            {
                MessageBox.Show("Ce champs ne prends que des valeurs numériques !!!");
            }
        }

        private void TxtAnneeAchat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Outils.CheckNumber(txtAnneeAchat.Text))
            {
                MessageBox.Show("Ce champs ne prends que des valeurs numériques !!!");
            }
        }

        private void BtnRechercher_Click(object sender, RoutedEventArgs e)
        {
            List<TeteEngin> listRecherche = new List<TeteEngin>();
            if (txtRecherche.Text != "")
            {
                listRecherche = ListTeteEngin.Where(o => o.Matricule.Contains(txtRecherche.Text) || o.Marque.Contains(txtRecherche.Text) || o.NoChassis.Contains(txtRecherche.Text)).ToList();
                TabTeteEngin.ItemsSource = listRecherche;
            }
            //else
            //{
            //    MessageBox.Show("Choisissez une date de recherche !!!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void BtnImprimer_Click(object sender, RoutedEventArgs e)
        {
            using (Report report = new Report()) {
                string AppFolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "EnteteEtat.frx"));
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "rptTeteEngins.frx"));
                report.RegisterData(TabTeteEngin.ItemsSource, "LstTeteEngin");
                report.GetDataSource("LstTeteEngin").Enabled = true;
                List<RaisonSociale> LstRs = new List<RaisonSociale>();
                LstRs = RaisonSociale.getAll();
                report.RegisterData(LstRs, "rs");
                report.GetDataSource("rs").Enabled = true;
                var x = report.Pages[0].ChildObjects[3];
                ((DataBand)x).DataSource = report.GetDataSource("rs");
                ((DataBand)x).DataSource = report.GetDataSource("LstTeteEngin");
                if (RaisonSociale.getRaisonSociale().Logo != null)
                {
                    byte[] logo = Hex.FromHexString(RaisonSociale.getRaisonSociale().Logo);
                    var bLogo = ImageConverter.FromArray(logo);
                    report.SetParameterValue("Logo", bLogo);
                }
                report.SetParameterValue("ParamTitre", "LISTE DES TETES ENGINS");
                report.Show();
            }
        }
    }
}
