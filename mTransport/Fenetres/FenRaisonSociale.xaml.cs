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
    /// Logique d'interaction pour raison sociale.xaml
    /// </summary>
    public partial class FenRaisonSociale : Window
    {
        public static List<RaisonSociale> ListRaisonSociale { get; set; }
        public int Id { get; set; }
        public FenRaisonSociale()
        {
            InitializeComponent();
            Loaded += FenRaisonSociale_Loaded1;
        }

        public void getRs() {
            var Rs = RaisonSociale.getRaisonSociale();
            if(Rs!=null)
                {
                    Id = Rs.Id;
                    txtNomSociete.Text = Rs.NomSociete;
                    txtAdresse.Text = Rs.Adresse;
                    txtBP.Text = Rs.BoitePostale;
                    txtQuartier.Text = Rs.Quartier;
                    txtVille.Text = Rs.Ville;
                    txtPays.Text = Rs.Pays;
                    txtTelephone1.Text = Rs.Telephone1;
                    txtTelephone2.Text = Rs.Telephone2;
                    txtEmail.Text = Rs.Email;
                    txtFax.Text = Rs.Fax;
                    txtSiteWeb.Text = Rs.Siteweb;
                    txtNomDirecteur.Text = Rs.NomDirecteur;
                    txtPrenomDirecteur.Text = Rs.PrenomDirecteur;

                    if (Rs != null && Rs.Logo != null)
                        txtLogo.Source = ImageConverter.ToImage(ImageConverter.FromArray(Hex.FromHexString(Rs.Logo)));
                    if (Rs != null && Rs.SignatureDirecteur != null)
                        txtSignature.Source = ImageConverter.ToImage(ImageConverter.FromArray(Hex.FromHexString(Rs.SignatureDirecteur)));
                }
            }

        private void FenRaisonSociale_Loaded1(object sender, RoutedEventArgs e)
        {
            try
            {
                ListRaisonSociale = new List<RaisonSociale>();
                GriserChamps();
                getRs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GriserChamps()
        {
           // InitChamps();
            txtNomSociete.IsEnabled = false;
            txtAdresse.IsEnabled = false;
            txtBP.IsEnabled = false;
            txtQuartier.IsEnabled = false;
            txtVille.IsEnabled = false;
            txtPays.IsEnabled = false;
            txtTelephone1.IsEnabled = false;
            txtTelephone2.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtFax.IsEnabled = false;
            txtSiteWeb.IsEnabled = false;
            txtNomDirecteur.IsEnabled = false;
            txtPrenomDirecteur.IsEnabled = false;
            BtnChargerLogo.IsEnabled = false;
            BtnSupprimerLogo.IsEnabled= false;
            BtnChargerSignature.IsEnabled = false;
            BtnSupprimerSignature.IsEnabled = false;
            BtnValider.IsEnabled = false;
            //BtnModifier.IsEnabled = false;
            BtnAnnuler.IsEnabled = false;

        }

        private void DegriserChamps()
        {
            txtNomSociete.IsEnabled = true;
            txtAdresse.IsEnabled = true;
            txtBP.IsEnabled = true;
            txtQuartier.IsEnabled = true;
            txtVille.IsEnabled = true;
            txtPays.IsEnabled = true;
            txtTelephone1.IsEnabled = true;
            txtTelephone2.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtFax.IsEnabled = true;
            txtSiteWeb.IsEnabled = true;
            txtNomDirecteur.IsEnabled = true;
            txtPrenomDirecteur.IsEnabled = true;
            BtnChargerLogo.IsEnabled = true;
            BtnSupprimerLogo.IsEnabled = true;
            BtnChargerSignature.IsEnabled = true;
            BtnSupprimerSignature.IsEnabled = true;
            BtnValider.IsEnabled = true;
            BtnModifier.IsEnabled = true;
            BtnAnnuler.IsEnabled = true;
            BtnValider.IsEnabled = true;
            BtnAnnuler.IsEnabled = true;
        }

        private void InitChamps()
        {
            txtNomSociete.Text = "";
            txtAdresse.Text = "";
            txtBP.Text = "";
            txtQuartier.Text = "";
            txtVille.Text = "";
            txtPays.Text = "";
            txtTelephone1.Text = "";
            txtTelephone2.Text = "";
            txtEmail.Text = "";
            txtFax.Text = "";
            txtSiteWeb.Text = "";
            txtNomDirecteur.Text = "";
            txtPrenomDirecteur.Text = "";

            Id = 0;
            txtSignature.Source = null;
            txtLogo.Source = null;
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
                if (string.IsNullOrWhiteSpace(txtNomSociete.Text) ||
                    string.IsNullOrWhiteSpace(txtAdresse.Text) ||
                    string.IsNullOrWhiteSpace(txtBP.Text) ||
                    string.IsNullOrWhiteSpace(txtQuartier.Text) ||
                    string.IsNullOrWhiteSpace(txtVille.Text) ||
                    string.IsNullOrWhiteSpace(txtPays.Text) ||
                    string.IsNullOrWhiteSpace(txtTelephone1.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtSiteWeb.Text) ||
                    string.IsNullOrWhiteSpace(txtNomDirecteur.Text) ||
                    string.IsNullOrWhiteSpace(txtPrenomDirecteur.Text))
                {
                    Outils.BoxMessage("C");
                }
                else
                {
                    RaisonSociale M = new RaisonSociale();
                    M.NomSociete = txtNomSociete.Text;
                    M.Adresse = txtAdresse.Text;
                    M.BoitePostale= txtBP.Text;
                    M.Quartier= txtQuartier.Text;
                    M.Ville= txtVille.Text;
                    M.Pays= txtPays.Text;
                    M.Telephone1 = txtTelephone1.Text;
                    M.Telephone2 = txtTelephone2.Text;
                    M.Email = txtEmail.Text;
                    M.Fax = txtFax.Text;
                    M.Siteweb = txtSiteWeb.Text;
                    M.NomDirecteur = txtNomDirecteur.Text;
                    M.PrenomDirecteur = txtPrenomDirecteur.Text;
                    if (txtLogo.Source == null)
                    {
                        M.Logo = null;
                    }
                    else
                    {
                        M.Logo = Hex.ToHexString(ImageConverter.ToArray(ImageConverter.FromImage((BitmapSource)txtLogo.Source)));
                    }
                    if (txtSignature.Source == null)
                    {
                        M.SignatureDirecteur = null;
                    }
                    else
                    {
                        M.SignatureDirecteur = Hex.ToHexString(ImageConverter.ToArray(ImageConverter.FromImage((BitmapSource)txtSignature.Source)));
                    }


                    if (Id > 0)
                    {
                        M.Id = Id;
                        M.Update();
                        Outils.BoxMessage("M");
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        M.Insert();
                        Outils.BoxMessage("A");
                        GriserChamps();
                    }
                    getRs();
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
                DegriserChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnChargerLogo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();

                fileDialog.Filter = "Image|*.jpg;*.png";

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = fileDialog.FileName;
                    txtLogo.Source = new BitmapImage(new Uri(fileName));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSupprimerLogo_Click(object sender, RoutedEventArgs e)
        {
            txtLogo.Source = null;
        }

        private void BtnChargerSignature_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();

                fileDialog.Filter = "Image|*.jpg;*.png";

                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = fileDialog.FileName;
                    txtSignature.Source = new BitmapImage(new Uri(fileName));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSupprimerSignature_Click(object sender, RoutedEventArgs e)
        {
            txtSignature.Source = null;
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            GriserChamps();
        }
    }
}
