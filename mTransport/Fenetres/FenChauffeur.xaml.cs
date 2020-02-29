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
    /// Logique d'interaction pour FenChauffeur.xaml
    /// </summary>
    public partial class FenChauffeur : Window
    {
        public static List<Chauffeur> ListChauffeur { get; set; }
        public int Id { get; set; }
        public FenChauffeur()
        {
            InitializeComponent();
            Loaded += FenChauffeur_Loaded;
        }

        private void FenChauffeur_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListChauffeur = new List<Chauffeur>();
                GriserChamps();
                txtDateEmbauche.SelectedDate = DateTime.Now;
                LoadTabChauffeur();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GriserChamps()
        {
            InitChamps();
            txtMatricule.IsEnabled = false;
            txtNomChauffeur.IsEnabled = false;
            txtPrenomsChauffeur.IsEnabled = false;
            txtAdresse.IsEnabled = false;
            txtTelephone1.IsEnabled = false;
            txtTelephone2.IsEnabled = false;
            txtPersonneAPrevenir.IsEnabled = false;
            txtDateEmbauche.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnAnnuler.IsEnabled = false;

        }

        private void DegriserChamps()
        {
            //txtMatricule.IsEnabled = true;
            txtNomChauffeur.IsEnabled = true;
            txtPrenomsChauffeur.IsEnabled = true;
            txtAdresse.IsEnabled = true;
            txtTelephone1.IsEnabled = true;
            txtTelephone2.IsEnabled = true;
            txtPersonneAPrevenir.IsEnabled = true;
            txtDateEmbauche.IsEnabled = true;
            BtnValider.IsEnabled = true;
            BtnAnnuler.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtMatricule.Text = "";
            txtNomChauffeur.Text = "";
            txtPrenomsChauffeur.Text = "";
            txtAdresse.Text = "";
            txtTelephone1.Text = "";
            txtTelephone2.Text = "";
            txtPersonneAPrevenir.Text = "";
            txtDateEmbauche.SelectedDate = DateTime.Now;
            Id = 0;
            txtImage.Source = null;
        }

        private void LoadTabChauffeur()
        {
            ListChauffeur = Chauffeur.getAll();
            TabChauffeur.ItemsSource = ListChauffeur;
        }


        private void BtnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DegriserChamps();
                txtMatricule.Text = Outils.Generator("Chauffeurs");
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
                if (string.IsNullOrWhiteSpace(txtMatricule.Text) ||
                    string.IsNullOrWhiteSpace(txtNomChauffeur.Text) ||
                    string.IsNullOrWhiteSpace(txtPrenomsChauffeur.Text) ||
                    string.IsNullOrWhiteSpace(txtTelephone1.Text))
                {
                    MessageBox.Show("Veuillez remplir les champs obligatoires !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Chauffeur M = new Chauffeur();
                    M.Matricule = txtMatricule.Text;
                    M.NomChauffeur = txtNomChauffeur.Text;
                    M.PrenomChauffeur = txtPrenomsChauffeur.Text;
                    M.AdresseChauffeur = txtAdresse.Text;
                    M.Telephone1Chauffeur = txtTelephone1.Text;
                    M.Telephone2Chauffeur = txtTelephone2.Text;
                    M.DateEmbauche = txtDateEmbauche.SelectedDate;
                    M.PersonneAPrevenir = txtPersonneAPrevenir.Text;
                    if (txtImage.Source == null)
                    {
                        M.PhotoChauffeur = null;
                    }
                    else
                    {
                        M.PhotoChauffeur = Hex.ToHexString(ImageConverter.ToArray(ImageConverter.FromImage((BitmapSource)txtImage.Source)));                       
                    }


                    if (Id > 0)
                    {
                        M.Id = Id;
                        M.Update();
                        MessageBox.Show("Modification effectuée !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadTabChauffeur();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        M.Insert();
                        ListChauffeur.Add(M);
                        TabChauffeur.Items.Refresh();
                        MessageBox.Show("Enregistrement effectué !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Information);
                        GriserChamps();
                    }                                      
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabChauffeur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TabChauffeur.SelectedIndex != -1)
                {
                    BtnModifier.IsEnabled = true;
                    BtnSupprimer.IsEnabled = true;
                    BtnAnnuler.IsEnabled = true;
                    BtnValider.IsEnabled = false;
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
                var T = TabChauffeur.SelectedItem as Chauffeur;
                Id = T.Id;
                txtMatricule.Text = T.Matricule;
                txtNomChauffeur.Text = T.NomChauffeur;
                txtPrenomsChauffeur.Text = T.PrenomChauffeur;
                txtAdresse.Text = T.AdresseChauffeur;
                txtTelephone1.Text = T.Telephone1Chauffeur;
                txtTelephone2.Text = T.Telephone2Chauffeur;
                txtDateEmbauche.SelectedDate = T.DateEmbauche;
                txtPersonneAPrevenir.Text = T.PersonneAPrevenir;

                if (T != null && T.PhotoChauffeur != null)
                    txtImage.Source = ImageConverter.ToImage(ImageConverter.FromArray(Hex.FromHexString(T.PhotoChauffeur)));

                BtnNouveau.IsEnabled = true;
                DegriserChamps();
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
                Chauffeur T = TabChauffeur.SelectedItem as Chauffeur;
                T.Supprime = true;
                T.Update();
                ListChauffeur.Remove(T);
                TabChauffeur.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnChargerImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();

                fileDialog.Filter = "Image|*.jpg";

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = fileDialog.FileName;
                    txtImage.Source = new BitmapImage(new Uri(fileName));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSupprimerImage_Click(object sender, RoutedEventArgs e)
        {
            txtImage.Source = null;
        }

        private void BtnImprimer_Click(object sender, RoutedEventArgs e)
        {
            using (Report report = new Report())
            {
                string AppFolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "EnteteEtat.frx"));
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "rptChauffeurs.frx"));
                report.RegisterData(TabChauffeur.ItemsSource, "LstData");
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
                report.SetParameterValue("ParamTitre", "LISTE DES CHAUFFEURS");
                report.Show();
            }
        }
    }
}
