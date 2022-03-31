using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeSampleCsharp
{
    //<LifetimeEventsCodeBehind>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        // Handler for the Initialized lifetime event (attached in XAML).
        private void InitializedEventHandler(object sender, EventArgs e) => 
            Handler.EventInfo(((FrameworkElement) sender).Name, "Initialized");

        // Handler for the Loaded lifetime event (attached in XAML).
        private void LoadedEventHandler(object sender, RoutedEventArgs e) => 
            Handler.EventInfo(((FrameworkElement)sender).Name, "Loaded");

        // Handler for the Unloaded lifetime event (attached in XAML).
        private void UnloadedEventHandler(object sender, RoutedEventArgs e) =>
            Handler.EventInfo(((FrameworkElement) sender).Name, "Unloaded");

        // Remove nested controls.
        private void Button_Click(object sender, RoutedEventArgs e) => 
            canvas.Children.Clear();
    }

    // Custom control.
    public class ComponentWrapper : ComponentWrapperBase 
    {
        public ComponentWrapper()
        {
            TextBox textBox1 = new();
            textBox1.Name = "componentTextBox1";
            textBox1.Initialized += (object sender, EventArgs e) =>
                Handler.EventInfo(((FrameworkElement)sender).Name, "Initialized");
            textBox1.Loaded += (object sender, RoutedEventArgs e) =>
                Handler.EventInfo(((FrameworkElement)sender).Name, "Loaded");
            textBox1.Unloaded += (object sender, RoutedEventArgs e) =>
                Handler.EventInfo(((FrameworkElement)sender).Name, "Unloaded");

            TextBox textBox2 = new();
            textBox2.Name = "componentTextBox2";
            textBox2.Initialized += (object sender, EventArgs e) =>
                Handler.EventInfo(((FrameworkElement)sender).Name, "Initialized");
            textBox2.Loaded += (object sender, RoutedEventArgs e) =>
                Handler.EventInfo(((FrameworkElement)sender).Name, "Loaded");
            textBox2.Unloaded += (object sender, RoutedEventArgs e) =>
                Handler.EventInfo(((FrameworkElement)sender).Name, "Unloaded");

            // Attach Initialize handler in code-behind before adding children.
            // A XAML-registered Initialize event handler won't fire
            // if child controls are added in code-behind because the
            // XAML-attached handler gets registered after the control has initialized.
            Initialized += (object sender, EventArgs e) =>
                   Handler.EventInfo("componentWrapper", "Initialized");

            Children.Add(textBox1);
            Children.Add(textBox2);
        }
    }

    // Custom base control.
    public class ComponentWrapperBase : StackPanel
    {
        public ComponentWrapperBase()
        {
            // Assign a base class handler for the Initialized lifetime event (attached in code-behind).
            Initialized += (object sender, EventArgs e) => 
                Handler.EventInfo("componentWrapperBase", "Initialized");

            // Assign a base class handler for the Loaded lifetime event (attached in code-behind).
            Loaded += (object sender, RoutedEventArgs e) => 
                Handler.EventInfo("componentWrapperBase", "Loaded");

            // Assign a base class handler for the Unloaded lifetime event (attached in code-behind).
            Unloaded += (object sender, RoutedEventArgs e) => 
                Handler.EventInfo("componentWrapperBase", "Unloaded");
        }
    }

    /* Debug output:
    componentTextBox1 Initialized event.
    componentTextBox2 Initialized event.
    componentWrapperBase Initialized event.
    componentWrapper Initialized event.
    outerStackPanel Initialized event.

    outerStackPanel Loaded event.
    componentWrapperBase Loaded event.
    componentWrapper Loaded event.
    componentTextBox1 Loaded event.
    componentTextBox2 Loaded event.

    outerStackPanel Unloaded event.
    componentWrapperBase Unloaded event.
    componentWrapper Unloaded event.
    componentTextBox1 Unloaded event.
    componentTextBox2 Unloaded event.
    */
    //</LifetimeEventsCodeBehind>
}
