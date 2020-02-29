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
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace mTransport.Fenetres
{
    /// <summary>
    /// Logique d'interaction pour FenTypeDepense.xaml
    /// </summary>
    public partial class FenConnexion : Window
    {
        public static Utilisateur User { get; set; }

        public FenConnexion()
        {
            InitializeComponent();
            Loaded += FenConnexion_Loaded;
            
        }

        private void FenConnexion_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            string pseudo, password;
            pseudo = txtPseudo.Text;
            password = txtPassword.Password;
            byte[] B = Encoding.UTF8.GetBytes(password);
            SHA1Cng sha1 = new SHA1Cng();

            byte[] result = sha1.ComputeHash(B);
            var toStringResult = Hex.ToHexString(result);

            if (!Utilisateur.Connecte(pseudo, toStringResult))
            {
                MessageBox.Show("Pseudo ou mot de passe incorrect!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                try
                {
                    User = Utilisateur.GetIdUserAfterLogin(pseudo, toStringResult);
                    MainWindow F = new MainWindow();
                    F.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
         
            }
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
