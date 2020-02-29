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
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public string Input { get; set; }
        public string Label { get; set; }

        public InputDialog()
        {
            InitializeComponent();
            Loaded += InputDialog_Loaded;
            DataContext = this;
        }

        void InputDialog_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public InputDialog(string title, string label, string input)
            : this()
        {
            this.Title = title;
            Input = input;
            Label = label;
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt_save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_input.Text))
            {
                MessageBox.Show("Veuillez saisir un libellé !", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            this.Close();
        }
    }
}