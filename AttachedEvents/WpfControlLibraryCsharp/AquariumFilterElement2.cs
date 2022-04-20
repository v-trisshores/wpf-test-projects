using System.Windows;

namespace WpfControl
{
    public class AquariumFilterElement2 : UIElement
    {
        // Register a custom routed event using the bubble routing strategy.
        public static readonly RoutedEvent CleanElement2Event = EventManager.RegisterRoutedEvent(
            "CleanElement2", 
            RoutingStrategy.Bubble, 
            typeof(RoutedEventHandler), 
            typeof(AquariumFilterElement));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler CleanElement
        {
            add { AddHandler(CleanElement2Event, value); }
            remove { RemoveHandler(CleanElement2Event, value); }
        }
    }
}
