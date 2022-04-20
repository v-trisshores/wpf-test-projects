using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfControl
{
    //<CustomButton>
    public class CustomButton : Button
    {
        // Register a custom routed event using the Bubble routing strategy.
        public static readonly RoutedEvent ConditionalClickEvent = EventManager.RegisterRoutedEvent(
            name: "ConditionalClick",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(CustomButton));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler ConditionalClick
        {
            add { AddHandler(ConditionalClickEvent, value); }
            remove { RemoveHandler(ConditionalClickEvent, value); }
        }

        // Use the Click event as a trigger for other events.
        protected override void OnClick()
        {
            // Raise the events, which will bubble up through the element tree.
            RaiseEvent(new(ConditionalClickEvent));
            RaiseEvent(new(AquariumFilterNonElement.CleanNonElementEvent));
            RaiseEvent(new(AquariumFilterElement.CleanElementEvent));

            // Call the base class OnClick() method so Click event subscribers are notified.
            base.OnClick();
        }
    }
    //</CustomButton>
}
