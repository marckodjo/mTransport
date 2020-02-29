using FastReport;
using mTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
    public partial class FenUtilisateur : Window
    {
        public static List<Utilisateur> ListUtilisateur { get; set; }
        public int Id { get; set; }
        public FenUtilisateur()
        {
            InitializeComponent();
            Loaded += FenUtilisateur_Loaded; ;
        }

        private void checkAuthorization()
        {
            using (DB db = new DB())
            {
                var Formulaire = db.Formulaires.SingleOrDefault(e => e.TagFormulaire == this.Tag.ToString());
                var Droits = db.DroitAcces.SingleOrDefault(r => r.IdFormulaire == Formulaire.IdFormulaire && r.IdProfil == FenConnexion.User.IdProfil);
                var ListeMenuAuthorized = db.DetailsDroitAcces.Where(u => u.IdDroidAcces == Droits.Id).ToList();

                foreach (var item in ListeMenuAuthorized)
                {
                    var menus = db.Menus.SingleOrDefault(o => o.IdMenu == item.IdMenu);
                    switch (menus.TagMenu)
                    {
                        case "Nouveau":
                            BtnNouveau.IsEnabled = true;
                            break;
                        case "Supprimer":
                            BtnSupprimer.IsEnabled = true;
                            break;

                        default:
                            break;
                    }
                }
            }
                
        }

        private void FenUtilisateur_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                checkAuthorization();
                ListUtilisateur = new List<Utilisateur>();
                GriserChamps();
                LoadTabUtilisateur();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GriserChamps()
        {
            InitChamps();
            txtPseudo.IsEnabled = false;
            txtNomUtilisateur.IsEnabled = false;
            txtPrenomsUtilisateur.IsEnabled = false;
            txtBp.IsEnabled = false;
            txtTelephone.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPassword.IsEnabled = false;
            txtPasswordComfirmed.IsEnabled = false;
            txtDateExpiration.IsEnabled = false;
            chkActif.IsEnabled = false;
            chkSuperUser.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnAnnuler.IsEnabled = false;

            chkmdp.IsEnabled = false;

            BtnSupprimerImage.IsEnabled = false;
            BtnChargerImage.IsEnabled = false;
        }

        private void DegriserChamps()
        {
            txtPseudo.IsEnabled = true;
            txtNomUtilisateur.IsEnabled = true;
            txtPrenomsUtilisateur.IsEnabled = true;
            txtBp.IsEnabled = true;
            txtTelephone.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtPassword.IsEnabled = true;
            txtPasswordComfirmed.IsEnabled = true;
            chkActif.IsEnabled = true;
            chkSuperUser.IsEnabled = true;
            txtDateExpiration.IsEnabled = true;
            BtnValider.IsEnabled = true;
            BtnAnnuler.IsEnabled = true;

            BtnSupprimerImage.IsEnabled = true;
            BtnChargerImage.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtPseudo.Text = "";
            txtNomUtilisateur.Text = "";
            txtPrenomsUtilisateur.Text = "";
            txtBp.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
            txtPassword.Password = "";
            txtPasswordComfirmed.Password = "";
            txtDateExpiration.SelectedDate = DateTime.Now;
            chkActif.IsChecked = false;
            chkSuperUser.IsChecked = false;
            Id = 0;
            txtPhoto.Source = null;
        }

        private void LoadTabUtilisateur()
        {
            ListUtilisateur = Utilisateur.getAll();
            TabUtilisateur.ItemsSource = ListUtilisateur;
        }


        private void BtnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DegriserChamps();
                InitChamps();
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

                if (string.IsNullOrWhiteSpace(txtPseudo.Text) ||
                    string.IsNullOrWhiteSpace(txtNomUtilisateur.Text) ||
                    string.IsNullOrWhiteSpace(txtPrenomsUtilisateur.Text) ||
                    string.IsNullOrWhiteSpace(txtBp.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Password) ||
                    string.IsNullOrWhiteSpace(txtDateExpiration.Text) ||
                    string.IsNullOrWhiteSpace(txtTelephone.Text))
                {
                    Outils.BoxMessage("C");
                }
                else
                {
                    if (txtPassword.Password != txtPasswordComfirmed.Password)
                    {
                        MessageBox.Show("Veuillez saisir des mots de passe identique", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    

                    Utilisateur M = new Utilisateur();
                    M.Pseudo = txtPseudo.Text;
                    M.NomUtilisateur = txtNomUtilisateur.Text;
                    M.PrenomUtilisateur = txtPrenomsUtilisateur.Text;
                    M.BP = txtBp.Text;
                    M.Telephone = txtTelephone.Text;
                    M.Email = txtEmail.Text;
                    M.DateExpirationPassword = txtDateExpiration.SelectedDate;
                    M.EstSuperutilisateur = chkSuperUser.IsChecked.Value;
                    M.Estactif = chkActif.IsChecked.Value;
                    if (txtPhoto.Source == null)
                    {
                        M.photo = null;
                    }
                    else
                    {
                        M.photo = Hex.ToHexString(ImageConverter.ToArray(ImageConverter.FromImage((BitmapSource)txtPhoto.Source)));                       
                    }

                    if (Id > 0)
                    {
                        M.Id = Id;
                        if (chkmdp.IsChecked.Value)
                        {
                            byte[] B = Encoding.UTF8.GetBytes(txtPassword.Password);
                            SHA1Cng sha1 = new SHA1Cng();
                            byte[] result = sha1.ComputeHash(B);
                            var toStringResult = Hex.ToHexString(result);
                            M.MotDePasse = toStringResult;
                        }
                        M.Update();
                        Outils.BoxMessage("M");
                        LoadTabUtilisateur();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        if (Utilisateur.userExist(txtPseudo.Text))
                        {
                            MessageBox.Show("Veuillez saisir un autre pseudo, celui-ci existe déjà!!!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        byte[] B = Encoding.UTF8.GetBytes(txtPassword.Password);
                        SHA1Cng sha1 = new SHA1Cng();
                        byte[] result = sha1.ComputeHash(B);
                        var toStringResult = Hex.ToHexString(result);
                        M.MotDePasse = toStringResult;

                        M.Insert();
                        ListUtilisateur.Add(M);
                        TabUtilisateur.Items.Refresh();
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
                var T = TabUtilisateur.SelectedItem as Utilisateur;
                Id = T.Id;
                txtPseudo.Text = T.Pseudo;
                txtNomUtilisateur.Text = T.NomUtilisateur;
                txtPrenomsUtilisateur.Text = T.PrenomUtilisateur;
                txtBp.Text = T.BP;
                txtTelephone.Text = T.Telephone;
                txtEmail.Text = T.Email;
                txtPassword.Password = T.MotDePasse;
                txtPasswordComfirmed.Password = T.MotDePasse;
                txtDateExpiration.SelectedDate = (DateTime) T.DateExpirationPassword;
                chkActif.IsChecked = T.Estactif;
                chkSuperUser.IsChecked = T.EstSuperutilisateur;

                if (T != null && T.photo != null)
                    txtPhoto.Source = ImageConverter.ToImage(ImageConverter.FromArray(Hex.FromHexString(T.photo)));

                BtnNouveau.IsEnabled = false;
                DegriserChamps();
                txtPassword.IsEnabled = false;
                txtPasswordComfirmed.IsEnabled = false;
                chkmdp.IsEnabled = true;
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
                Utilisateur T = TabUtilisateur.SelectedItem as Utilisateur;
                T.Supprime = true;
                T.Update();
                ListUtilisateur.Remove(T);
                TabUtilisateur.Items.Refresh();
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
                    txtPhoto.Source = new BitmapImage(new Uri(fileName));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSupprimerImage_Click(object sender, RoutedEventArgs e)
        {
            txtPhoto.Source = null;
        }

        private void BtnImprimer_Click(object sender, RoutedEventArgs e)
        {
            using (Report report = new Report())
            {
                string AppFolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "EnteteEtat.frx"));
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "rptChauffeurs.frx"));
                report.RegisterData(TabUtilisateur.ItemsSource, "LstData");
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

        private void TabUtilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TabUtilisateur.SelectedIndex != -1)
                {
                    BtnModifier.IsEnabled = true;
                    BtnSupprimer.IsEnabled = true;
                    BtnAnnuler.IsEnabled = true;
                    BtnValider.IsEnabled = false;
                    BtnNouveau.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            InitChamps();
            GriserChamps();
            BtnModifier.IsEnabled = false;
            BtnSupprimer.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnValider.IsEnabled = false;
        }

        private void Chkmdp_Click(object sender, RoutedEventArgs e)
        {
            if (chkmdp.IsChecked.Value)
            {
                txtPassword.IsEnabled = true;
                txtPasswordComfirmed.IsEnabled = true;
            }
            else if (!chkmdp.IsChecked.Value)
            {
                txtPassword.IsEnabled = false;
                txtPasswordComfirmed.IsEnabled = false;
            }
        }
    }
}
