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

            // Attach a handler on outerStackPanel that will be invoked by handled KeyDown events.
            componentTextBox.AddHandler(KeyDownEvent, new RoutedEventHandler(componentTextBox_KeyDown_handled),
                handledEventsToo: true);
        }

        private void componentTextBox_KeyDown_handled(object sender, RoutedEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            string handledEventsToo = e.Handled ? " Parameter handledEventsToo set to true." : "";
            Debug.WriteLine($"Instance handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}.{handledEventsToo}");
        }

        private void outerStackPanel_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            string handledEventsToo = e.Handled ? " Parameter handledEventsToo set to true." : "";
            Debug.WriteLine($"Instance handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}.{handledEventsToo}");

            e.Handled = true;
            Debug.WriteLine($"Handled={e.Handled}.");
        }

        private void componentTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            string handledEventsToo = e.Handled ? " Parameter handledEventsToo set to true." : "";
            Debug.WriteLine($"Instance handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}.{handledEventsToo}");
        }

        private void componentWrapper_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            string handledEventsToo = e.Handled ? " Parameter handledEventsToo set to true." : "";
            Debug.WriteLine($"Instance handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}.{handledEventsToo}");
        }
    }

    public class ComponentWrapper : StackPanel
    {
        static ComponentWrapper()
        {
            EventManager.RegisterClassHandler(typeof(ComponentWrapper), KeyDownEvent, new RoutedEventHandler(KeyDownHandler), handledEventsToo: true);
        }

        private static void KeyDownHandler(object sender, RoutedEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            string handledEventsToo = e.Handled ? " Parameter handledEventsToo set to true." : "";
            Debug.WriteLine($"Class static handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}.{handledEventsToo}");
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            string senderName = "ComponentWrapper";
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            string handledEventsToo = e.Handled ? " Parameter handledEventsToo set to true." : "";
            Debug.WriteLine($"Class virtual handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}.{handledEventsToo}");
        }
    }
}
