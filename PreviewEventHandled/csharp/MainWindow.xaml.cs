using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeSampleCsharp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Attach a handler on StackPanel that will be invoked by handled KeyDown events.
            textBox.AddHandler(KeyDownEvent, new KeyEventHandler(TextBox_KeyDown), handledEventsToo: true);
            stackPanel.AddHandler(KeyDownEvent, new KeyEventHandler(TextBox_KeyDown), handledEventsToo: true);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            Debug.WriteLine($"Instance handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}. handledEventsToo={e.Handled}");
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;
            Debug.WriteLine($"Instance handler attached to {senderName} " +
                $"triggered by {eventName} event raised on {sourceName}. handledEventsToo={e.Handled}");

            //e.Handled = true;
            Debug.WriteLine($"Setting Handled={e.Handled}.");
        }
    }
}
