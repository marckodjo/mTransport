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
    /// Interaction logic for FenTypeBenneEngin.xaml
    /// </summary>
    public partial class FenTypeBenneEngin : Window
    {
        public static List<TypeBenneEngin> ListeTypeDepense { get; set; }
        public FenTypeBenneEngin()
        {
            InitializeComponent();
            Loaded += FenTypeDepense_Loaded;
        }

        private void FenTypeDepense_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListeTypeDepense = new List<TypeBenneEngin>();
                LoadTabTypeDepense();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void LoadTabTypeDepense()
        {
            ListeTypeDepense = TypeBenneEngin.getAll();
            TabTypeDepense.ItemsSource = ListeTypeDepense;
        }

        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //DegriserChamps();
                //InitChamps();
                //btnModifier.IsEnabled = false;
                //btnSupprimer.IsEnabled = false;
                Insert();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Insert()
        {
            var input = string.Empty;
            var dialog = new InputDialog("Type benne engin", "Saisissez le type de benne engin", input);

            if (dialog.ShowDialog() == true)
            {
                if (!string.IsNullOrEmpty(dialog.Input))
                {
                    var typ = new TypeBenneEngin
                    {
                        Libelle = dialog.Input
                    };

                    typ.Insert();

                    ListeTypeDepense.Add(typ);
                    TabTypeDepense.Items.Refresh();

                }
            }
        }

        private void GriserChamps()
        {
            InitChamps();
        }

        private void DegriserChamps()
        {

        }

        private void InitChamps()
        {

        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //DegriserChamps();
                //InitChamps();
                //btnModifier.IsEnabled = false;
                //btnSupprimer.IsEnabled = false;
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Update()
        {
            var typ = TabTypeDepense.SelectedItem as TypeBenneEngin;
            var input = typ.Libelle;
            var dialog = new InputDialog("Type benne engin", "Saisissez le type de benne engin", input);

            if (dialog.ShowDialog() == true)
            {
                if (!string.IsNullOrEmpty(dialog.Input))
                {
                    typ.Libelle = dialog.Input;

                    typ.Update();

                    TabTypeDepense.Items.Refresh();

                }
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete()
        {
            var typ = TabTypeDepense.SelectedItem as TypeBenneEngin;
            if (typ == null)
            {
                MessageBox.Show("Veuiller selectionner un élément", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("Voulez-vous vraiment supprimer le type de benne engin " + typ.Libelle + " ?", "mTransport", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    typ.Delete();
                    TabTypeDepense.Items.Refresh();
                }
            }
        }
    }
}
