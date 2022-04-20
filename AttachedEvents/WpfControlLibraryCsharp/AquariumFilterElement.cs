using System.Windows;

namespace WpfControl
{
    public class AquariumFilterElement : UIElement
    {
        // Register a custom routed event using the bubble routing strategy.
        public static readonly RoutedEvent CleanElementEvent = EventManager.RegisterRoutedEvent(
            "CleanElement", 
            RoutingStrategy.Bubble, 
            typeof(RoutedEventHandler), 
            typeof(AquariumFilterElement));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler CleanElement
        {
            add { AddHandler(CleanElementEvent, value); }
            remove { RemoveHandler(CleanElementEvent, value); }
        }
    }
}
