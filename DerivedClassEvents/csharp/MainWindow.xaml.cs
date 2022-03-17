using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace CodeSampleCsharp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Debug output:
        // OnKeyDown class handler triggered from ComponentWrapper.
    }

    public class ComponentWrapper : ComponentWrapperBase
    {
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            Debug.WriteLine($"OnKeyDown class handler triggered from ComponentWrapper.");

            //e.Handled = true;

            //base.OnKeyDown(e);
        }
    }

    public class ComponentWrapperBase : StackPanel
    {
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            Debug.WriteLine($"OnKeyDown class handler triggered from ComponentWrapperBase.");

            //e.Handled = true;

            //base.OnKeyDown(e);
        }
    }
}
