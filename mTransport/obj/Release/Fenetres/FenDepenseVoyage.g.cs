﻿#pragma checksum "..\..\..\Fenetres\FenDepenseVoyage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0F4B6EF99834D92C7E73DE3084D583C2E194D911"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using mTransport.Fenetres;


namespace mTransport.Fenetres {
    
    
    /// <summary>
    /// FenDepenseVoyage
    /// </summary>
    public partial class FenDepenseVoyage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbTypeDepense;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbVoyage;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMontant;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnNouveau;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnValider;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnModifier;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAnnuler;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSupprimer;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnImprimer;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TabDepense;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/mTransport;component/fenetres/fendepensevoyage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cmbTypeDepense = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.cmbVoyage = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.txtMontant = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.txtMontant.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtMontant_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnNouveau = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.BtnNouveau.Click += new System.Windows.RoutedEventHandler(this.BtnNouveau_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnValider = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.BtnValider.Click += new System.Windows.RoutedEventHandler(this.BtnValider_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnModifier = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.BtnModifier.Click += new System.Windows.RoutedEventHandler(this.BtnModifier_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnAnnuler = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.BtnAnnuler.Click += new System.Windows.RoutedEventHandler(this.BtnAnnuler_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BtnSupprimer = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.BtnSupprimer.Click += new System.Windows.RoutedEventHandler(this.BtnSupprimer_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BtnImprimer = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.BtnImprimer.Click += new System.Windows.RoutedEventHandler(this.BtnImprimer_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.TabDepense = ((System.Windows.Controls.DataGrid)(target));
            
            #line 23 "..\..\..\Fenetres\FenDepenseVoyage.xaml"
            this.TabDepense.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabDepense_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

