using System.Diagnostics;
using System.Windows;

namespace CodeSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Assign an event handler to the CustomButton using the '+=' operator.
            // customButton.ConditionalClick += Handler_EventInfo;

            // Assign an event handler to the CustomButton using the AddHandler method.
            // customButton.AddHandler(WpfControl.CustomButton.ConditionalClickEvent,
            //   new RoutedEventHandler(Handler_EventInfo));

            // Assign an event handler to the StackPanel using the AddHandler method.
            // StackPanel1.AddHandler(WpfControl.CustomButton.ConditionalClickEvent, 
            //   new RoutedEventHandler(Handler_EventInfo));

            StackPanel1.AddHandler(WpfControl.AquariumFilterElement.CleanElementEvent, new RoutedEventHandler(Handler_EventInfo));
            StackPanel1.AddHandler(WpfControl.AquariumFilterNonElement.CleanNonElementEvent, new RoutedEventHandler(Handler_EventInfo));
        }



        //<EventHandler>
        // The ConditionalClick event handler.
        private void Handler_EventInfo(object sender, RoutedEventArgs e)
        {
            string senderName = ((FrameworkElement)sender).Name;
            string sourceName = ((FrameworkElement)e.Source).Name;
            string eventName = e.RoutedEvent.Name;

            Debug.WriteLine($"Routed event handler attached to {senderName}, " +
                $"triggered by the {eventName} routed event raised on {sourceName}.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customButton.RaiseEvent(new RoutedEventArgs(WpfControl.AquariumFilterNonElement.CleanNonElementEvent));
            customButton.RaiseEvent(new RoutedEventArgs(WpfControl.AquariumFilterElement.CleanElementEvent));
        }

        private void StackPanel1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
