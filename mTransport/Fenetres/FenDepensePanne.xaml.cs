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
    /// Logique d'interaction pour FenDepense.xaml
    /// </summary>
    public partial class FenDepensePanne : Window
    {
        public int voyageId { get; set; }
        public int historiquePanneId { get; set; }
        public List<LoadCombo> ListVoyage { get; set; }
        public List<LoadCombo> ListHistoriquePanne { get; set; }
        public List<LoadCombo> ListTypeDepense { get; set; }

        public List<Depense> ListDepense;

        public int Id;
        public FenDepensePanne()
        {
            InitializeComponent();
            Loaded += FenDepensePanne_Loaded; ;
            //DataContext = this;
        }

        private void FenDepensePanne_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ListVoyage = new List<LoadCombo>();
                ListHistoriquePanne = new List<LoadCombo>();
                ListTypeDepense = new List<LoadCombo>();
                ListDepense = new List<Depense>();
                if (voyageId > 0)
                {
                    ListCmbVoyage();
                }
                if (historiquePanneId > 0)
                {
                    ListCmbHistoriquePanne();
                }
                ListCmbTypeDepense();
                LoadTabDepense();
                GriserChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DepenseFen(int idVehi , int idHistoPanne ) 
        {
            if (idVehi > 0)
            {
                voyageId = idVehi;
            }
            if (idHistoPanne > 0)
            {
                historiquePanneId = idHistoPanne;
            }
        }
        public void ListCmbVoyage()
        {
            var l = Voyage.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.Designation;
                ListVoyage.Add(lCombo);
            }
            //Verifier si c'est l'id du véhicule qui est renseigner, sélectionner et afficher le libellé correspondant, griser le champ
            //if (voyageId > 0)
            //{
            //    var c = ListVoyage.SingleOrDefault(a => a.Id == voyageId);
            //    int val = ListVoyage.IndexOf(c);
            //    cmbVoyage.SelectedIndex = val;
            //    cmbVoyage.IsEnabled = false;
            //}
            
        }
        public void ListCmbTypeDepense()
        {
            var l = TypeDepense.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.Designation;
                ListTypeDepense.Add(lCombo);
            }
            cmbTypeDepense.ItemsSource = ListTypeDepense;
        }
        public void ListCmbHistoriquePanne()
        {
            var l = HistoriquePanne.getAll();
            foreach (var item in l)
            {
                LoadCombo lCombo = new LoadCombo();
                lCombo.Id = item.Id;
                lCombo.Libelle = item.Description;
                ListHistoriquePanne.Add(lCombo);
            }
            cmbHistoriquePanne.ItemsSource = ListHistoriquePanne;
            if (historiquePanneId > 0)
            {
                var c = ListHistoriquePanne.SingleOrDefault(a => a.Id == historiquePanneId);
                int val = ListHistoriquePanne.IndexOf(c);
                cmbHistoriquePanne.SelectedIndex = val;
            }
        }
        private void GriserChamps()
        {
            InitChamps();
            if (voyageId > 0)
            {
                cmbHistoriquePanne.IsEnabled = false;
            }
            if (historiquePanneId > 0)
            {
                //cmbVoyage.IsEnabled = false;
            }
            cmbTypeDepense.IsEnabled = false;
            txtMontant.IsEnabled = false;
            BtnValider.IsEnabled = false;
            BtnModifier.IsEnabled = false;
            BtnNouveau.IsEnabled = true;
            BtnSupprimer.IsEnabled = false;
        }
        private void DegriserChamps()
        {
            cmbTypeDepense.IsEnabled = true;
            txtMontant.IsEnabled = true;
            if (voyageId > 0)
            {
                cmbHistoriquePanne.IsEnabled = false;
            }
            if (historiquePanneId > 0)
            {
                //cmbVoyage.IsEnabled = false;
            }
            BtnValider.IsEnabled = true;
            //BtnAnnuler.IsEnabled = true;
            //BtnSupprimer.IsEnabled = true;
        }
        private void InitChamps()
        {
            txtMontant.Text = "";
            cmbTypeDepense.SelectedIndex = -1;
            if (voyageId > 0)
            {
                cmbHistoriquePanne.SelectedIndex = -1;
            }
            if (historiquePanneId > 0)
            {
                //cmbVoyage.SelectedIndex = -1;
            }
            Id = 0;
        }
        private void LoadTabDepense()
        {
            if (voyageId > 0)
            {
                ListDepense = Depense.getAll(voyageId, "v");
            }
            if (historiquePanneId > 0)
            {
                ListDepense = Depense.getAll(historiquePanneId, "h");
            }

            TabDepense.ItemsSource = ListDepense;
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
                if (string.IsNullOrWhiteSpace(txtMontant.Text) ||
                    //cmbHistoriquePanne.SelectedIndex == -1 ||
                    cmbTypeDepense.SelectedIndex == -1 
                    //cmbVoyage.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("Veuillez remplir tous les champs !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Depense T = new Depense();
                    T.Montant = Decimal.Parse(txtMontant.Text);
                    
                    var td = cmbTypeDepense.SelectedItem as LoadCombo;
                    T.IdTypeDepense = td.Id;
                    //var vg = cmbVoyage.SelectedItem as LoadCombo;
                    var hi = cmbHistoriquePanne.SelectedItem as LoadCombo;
                    if (voyageId > 0)
                    {
                        //T.IdVoyage = vg.Id;
                    }
                    if (historiquePanneId > 0)
                    {
                        T.IdHistoriquePanne = hi.Id;
                    }
                    if (Id > 0)
                    {
                        T.Id = Id;
                        //var k = Voyage.getVoyage(vg.Id);
                        var m = TypeDepense.getTypeDepense(td.Id);
                        var n = HistoriquePanne.getHistoriquePanne(hi.Id);
                        T.TypeDepense = m;
                        if (voyageId > 0)
                        {
                            //T.Voyage = k;
                        }
                        if (historiquePanneId > 0)
                        {
                            T.HistoriquePanne = n;
                        }

                        T.Update();
                        MessageBox.Show("Modification effectuée !");
                        LoadTabDepense();
                        GriserChamps();
                        Id = 0;
                    }
                    else
                    {
                        T.Insert();
                        var m = TypeDepense.getTypeDepense(td.Id);
                        T.TypeDepense = m;
                        if (voyageId > 0)
                        {
                            //var k = Voyage.getVoyage(vg.Id);
                            //T.Voyage = k;
                        }
                        if (historiquePanneId > 0)
                        {
                            var n = HistoriquePanne.getHistoriquePanne(hi.Id);
                            T.HistoriquePanne = n;
                        }
                        ListDepense.Add(T);
                        TabDepense.Items.Refresh();
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
                Depense Tab = TabDepense.SelectedItem as Depense;
                var T = Depense.getDepense(Tab.Id);
                Id = T.Id;
                txtMontant.Text = T.Montant.ToString();
                var c = ListHistoriquePanne.SingleOrDefault(a => a.Id == T.IdHistoriquePanne);
                int val = ListHistoriquePanne.IndexOf(c);
                cmbHistoriquePanne.SelectedIndex = val;
                var k = ListVoyage.SingleOrDefault(a => a.Id == T.IdVoyage);
                int l = ListVoyage.IndexOf(k);
                //cmbVoyage.SelectedIndex = l;
                var d = ListTypeDepense.SingleOrDefault(a => a.Id == T.IdTypeDepense);
                int ii = ListTypeDepense.IndexOf(d);
                cmbTypeDepense.SelectedIndex = ii;
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
                if (TabDepense.SelectedIndex != -1)
                {
                    Depense Tab = (Depense)TabDepense.SelectedItem;
                    var T = Depense.getDepense(Tab.Id);
                    T.Supprime = true;
                    T.Delete();
                    LoadTabDepense();
                    InitChamps();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TabDepense_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TabDepense.SelectedIndex != -1)
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
        private void TxtMontant_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Outils.CheckNumber(txtMontant.Text))
            {
                MessageBox.Show("Ce champs ne prends que des valeurs numériques !!!");
            }
        }
        private void BtnImprimer_Click(object sender, RoutedEventArgs e)
        {
            using (Report report = new Report())
            {
                string AppFolder = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "EnteteEtat.frx"));
                report.Load(System.IO.Path.Combine(AppFolder, "Reports", "rptBenneEngins.frx"));
                report.RegisterData(TabDepense.ItemsSource, "LstData");
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
                report.SetParameterValue("ParamTitre", "LISTE DES DEPENSES");
                report.Show();
            }
        }
    }
}
