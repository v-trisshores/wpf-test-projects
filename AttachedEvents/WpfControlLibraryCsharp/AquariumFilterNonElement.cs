using System.Windows;

namespace WpfControl
{
    public class AquariumFilterNonElement
    {
        // Register a custom routed event using the bubble routing strategy.
        public static readonly RoutedEvent CleanNonElementEvent = EventManager.RegisterRoutedEvent(
            "CleanNonElement", 
            RoutingStrategy.Bubble, 
            typeof(RoutedEventHandler), 
            typeof(AquariumFilterNonElement));

        // Provide an add handler accessor method for the Clean event.
        public static void AddCleanNonElementHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            if (dependencyObject is not UIElement uiElement)
                return;

            uiElement.AddHandler(CleanNonElementEvent, handler);
        }

        // Provide a remove handler accessor method for the Clean event.
        public static void RemoveCleanNonElementHandler(DependencyObject dependencyObject, RoutedEventHandler handler)
        {
            if (dependencyObject is not UIElement uiElement)
                return;

            uiElement.RemoveHandler(CleanNonElementEvent, handler);
        }
    }
}
