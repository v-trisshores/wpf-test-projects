using System;
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

        static MainWindow()
        {
            EventManager.RegisterClassHandler(typeof(ComponentWrapper), KeyDownEvent, new RoutedEventHandler(KeyDownHandler));
        }

        private static void KeyDownHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"KeyDown class handler triggered on ComponentWrapper within MainWindow.");
        }

        // Debug output:
        //
        // KeyDown class handler triggered on ComponentWrapper.
        // KeyDown class handler triggered on ComponentWrapperBase.
        // OnKeyDown class handler triggered on ComponentWrapper.
    }

    public class ComponentWrapper : ComponentWrapperBase
    {
        static ComponentWrapper()
        {
            EventManager.RegisterClassHandler(typeof(ComponentWrapper), KeyDownEvent, new RoutedEventHandler(KeyDownHandler));
        }

        private static void KeyDownHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"KeyDown class handler triggered on ComponentWrapper.");
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            Debug.WriteLine($"OnKeyDown class handler triggered on ComponentWrapper.");

            //e.Handled = true;

            //base.OnKeyDown(e);
        }
    }

    public class ComponentWrapperBase : StackPanel
    {
        static ComponentWrapperBase()
        {
            EventManager.RegisterClassHandler(typeof(ComponentWrapperBase), KeyDownEvent, new RoutedEventHandler(KeyDownHandler));
        }

        private static void KeyDownHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"KeyDown class handler triggered on ComponentWrapperBase.");
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            Debug.WriteLine($"OnKeyDown class handler triggered on ComponentWrapperBase.");

            //e.Handled = true;

            //base.OnKeyDown(e);
        }
    }
}
