﻿#pragma checksum "..\..\NewClientWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A44234A59C19E691B26D3D3C4CA402DD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DB_Store;
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


namespace DB_Store {
    
    
    /// <summary>
    /// NewClientWindow
    /// </summary>
    public partial class NewClientWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid titleBar;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonClose;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridClients;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxSurnameClients;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxNameClients;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxPatronymicClients;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxPhoneClients;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxAddresClients;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAdd;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\NewClientWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/DB_Store;component/newclientwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewClientWindow.xaml"
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
            
            #line 14 "..\..\NewClientWindow.xaml"
            ((DB_Store.NewClientWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.titleBar = ((System.Windows.Controls.Grid)(target));
            
            #line 16 "..\..\NewClientWindow.xaml"
            this.titleBar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.titleBar_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonClose = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\NewClientWindow.xaml"
            this.buttonClose.Click += new System.Windows.RoutedEventHandler(this.buttonClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.gridClients = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.textBoxSurnameClients = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\NewClientWindow.xaml"
            this.textBoxSurnameClients.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInputCheck);
            
            #line default
            #line hidden
            
            #line 32 "..\..\NewClientWindow.xaml"
            this.textBoxSurnameClients.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PreviewKeyDownCheck);
            
            #line default
            #line hidden
            
            #line 32 "..\..\NewClientWindow.xaml"
            this.textBoxSurnameClients.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChangedCheck);
            
            #line default
            #line hidden
            return;
            case 6:
            this.textBoxNameClients = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\NewClientWindow.xaml"
            this.textBoxNameClients.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInputCheck);
            
            #line default
            #line hidden
            
            #line 41 "..\..\NewClientWindow.xaml"
            this.textBoxNameClients.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PreviewKeyDownCheck);
            
            #line default
            #line hidden
            
            #line 41 "..\..\NewClientWindow.xaml"
            this.textBoxNameClients.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChangedCheck);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textBoxPatronymicClients = ((System.Windows.Controls.TextBox)(target));
            
            #line 50 "..\..\NewClientWindow.xaml"
            this.textBoxPatronymicClients.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInputCheck);
            
            #line default
            #line hidden
            
            #line 50 "..\..\NewClientWindow.xaml"
            this.textBoxPatronymicClients.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.PreviewKeyDownCheck);
            
            #line default
            #line hidden
            
            #line 50 "..\..\NewClientWindow.xaml"
            this.textBoxPatronymicClients.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChangedCheck);
            
            #line default
            #line hidden
            return;
            case 8:
            this.textBoxPhoneClients = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\NewClientWindow.xaml"
            this.textBoxPhoneClients.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInputCheck);
            
            #line default
            #line hidden
            
            #line 59 "..\..\NewClientWindow.xaml"
            this.textBoxPhoneClients.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChangedCheck);
            
            #line default
            #line hidden
            return;
            case 9:
            this.textBoxAddresClients = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\NewClientWindow.xaml"
            this.textBoxAddresClients.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.PreviewTextInputCheck);
            
            #line default
            #line hidden
            
            #line 68 "..\..\NewClientWindow.xaml"
            this.textBoxAddresClients.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChangedCheck);
            
            #line default
            #line hidden
            return;
            case 10:
            this.buttonAdd = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\NewClientWindow.xaml"
            this.buttonAdd.Click += new System.Windows.RoutedEventHandler(this.buttonAdd_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.buttonCancel = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\NewClientWindow.xaml"
            this.buttonCancel.Click += new System.Windows.RoutedEventHandler(this.buttonCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

