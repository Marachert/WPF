#pragma checksum "..\..\DnD.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1A5AE261B917C917B2257082E1C5AA7A8B5D9B8A11FA99F381B8D4B0733B53A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// DnD
    /// </summary>
    public partial class DnD : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\DnD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Field;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\DnD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse Subj;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\DnD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse Subj2;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\DnD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse Subj3;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\DnD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Subj4;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\DnD.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle TargetFrame;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/dnd.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DnD.xaml"
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
            
            #line 11 "..\..\DnD.xaml"
            ((WpfApp1.DnD)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Mouse_Up);
            
            #line default
            #line hidden
            
            #line 12 "..\..\DnD.xaml"
            ((WpfApp1.DnD)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Mouse_Move);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Field = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.Subj = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 16 "..\..\DnD.xaml"
            this.Subj.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Subj_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Subj2 = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 25 "..\..\DnD.xaml"
            this.Subj2.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Subj_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Subj3 = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 34 "..\..\DnD.xaml"
            this.Subj3.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Subj_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Subj4 = ((System.Windows.Controls.Image)(target));
            
            #line 43 "..\..\DnD.xaml"
            this.Subj4.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Subj_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TargetFrame = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 8:
            
            #line 61 "..\..\DnD.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Subj_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

