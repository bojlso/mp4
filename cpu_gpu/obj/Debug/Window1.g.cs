﻿#pragma checksum "..\..\Window1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AE85123403FF70D804FB829498CEF404"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace cpu_gpu {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bu_1;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bu_2;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bu_3;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bu_4;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bu_5;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PostB;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextB;
        
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
            System.Uri resourceLocater = new System.Uri("/cpu_gpu;component/window1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window1.xaml"
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
            this.bu_1 = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\Window1.xaml"
            this.bu_1.Click += new System.Windows.RoutedEventHandler(this.CButton_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bu_2 = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\Window1.xaml"
            this.bu_2.Click += new System.Windows.RoutedEventHandler(this.CButton_2);
            
            #line default
            #line hidden
            return;
            case 3:
            this.bu_3 = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\Window1.xaml"
            this.bu_3.Click += new System.Windows.RoutedEventHandler(this.CButton_3);
            
            #line default
            #line hidden
            return;
            case 4:
            this.bu_4 = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Window1.xaml"
            this.bu_4.Click += new System.Windows.RoutedEventHandler(this.CButton_4);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bu_5 = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\Window1.xaml"
            this.bu_5.Click += new System.Windows.RoutedEventHandler(this.CButton_5);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PostB = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Window1.xaml"
            this.PostB.Click += new System.Windows.RoutedEventHandler(this.left_click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NextB = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\Window1.xaml"
            this.NextB.Click += new System.Windows.RoutedEventHandler(this.right_click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 28 "..\..\Window1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close_Act);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

