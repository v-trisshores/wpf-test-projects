using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CodeSampleCsharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        public async void Test1(object sender, RoutedEventArgs e)
        {
            Gauge gauge = new();

            Event.Logger.Clear();  // set breakpoint here.

            gauge.CurrentReading = 5;
            //gauge.MinReading = 0;
            //gauge.MaxReading = 10;

            var initialCurrReading = gauge.CurrentReading;
            //var initialMinReading = gauge.MinReading;
            //var initialMaxReading = gauge.MaxReading;

            await Task.Delay(100);

            Event.Logger.Clear();  // set breakpoint here.

            gauge.CurrentReading = 15;

            var currReading = gauge.CurrentReading;
            //var minReading = gauge.MinReading;
            //var maxReading = gauge.MaxReading;

            await Task.Delay(100);

            Event.Logger.Clear();  // set breakpoint here.

            try
            {
                gauge.CurrentReading = 100;
            }
            catch (ArgumentException e2)
            { }

            //lblMessage.Content = $"Tropical aquarium graphic URL: " + $"{tropicalAquarium.AquariumGraphic.OriginalString}" + Environment.NewLine;
            //lblMessage.Content += $"Aquarium graphic URL: " + $"{aquarium.AquariumGraphic.OriginalString}" + Environment.NewLine;

            //lblMessage.Content += $"Queried owner-type AquariumGraphic default value: " + $"{aquariumPropertyMetadata.DefaultValue}" + Environment.NewLine;
            //lblMessage.Content += $"Queried owner-type AquariumGraphic affects render: " + $"{aquariumPropertyMetadata.AffectsRender}" + Environment.NewLine;

            //lblMessage.Content += $"Queried derived-type TropicalAquarium default value: " + $"{tropicalAquariumPropertyMetadata.DefaultValue}" + Environment.NewLine;
            //lblMessage.Content += $"Queried derived-type TropicalAquarium affects render: " + $"{tropicalAquariumPropertyMetadata.AffectsRender}" + Environment.NewLine;
        }
    }

    public static class Event
    {
        public static List<string> Logger = new();
    }

    //<ValidateValueCallback>
    public class Gauge : Control
    {
        public Gauge() : base() { }

        // Register a dependency property with the specified property name,
        // property type, owner type, property metadata, and callbacks.
        public static readonly DependencyProperty CurrentReadingProperty = 
            DependencyProperty.Register(
                name: "CurrentReading",
                propertyType: typeof(double),
                ownerType: typeof(Gauge),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: double.NaN,
                    flags: FrameworkPropertyMetadataOptions.AffectsMeasure,
                    propertyChangedCallback: new PropertyChangedCallback(OnCurrentReadingChanged),
                    //propertyChangedCallback: null,
                    coerceValueCallback: new CoerceValueCallback(CoerceCurrentReading)
                )
                ,validateValueCallback: new ValidateValueCallback(IsValidReading)
            );

        // CLR wrapper with get/set accessors.
        public double CurrentReading
        {
            get => (double)GetValue(CurrentReadingProperty);
            set => SetValue(CurrentReadingProperty, value);
        }

        // Validate value callback.
        public static bool IsValidReading(object value)
        {
            Event.Logger.Add(MethodBase.GetCurrentMethod().Name);
            return (double)value != 100;
        }

        // property changed callback.
        private static void OnCurrentReadingChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            Event.Logger.Add(MethodBase.GetCurrentMethod().Name + ": " + depObj.GetValue(e.Property));

            //depobj.coercevalue(minreadingproperty);
            //depobj.coercevalue(maxreadingproperty);
        }

        // Coerce value callback.
        private static object CoerceCurrentReading(DependencyObject depObj, object value)
        {
            Event.Logger.Add(MethodBase.GetCurrentMethod().Name);

            Gauge gauge = (Gauge)depObj;
            double currentVal = (double)value;
            //currentVal = currentVal < gauge.MinReading ? gauge.MinReading : currentVal;
            //currentVal = currentVal > gauge.MaxReading ? gauge.MaxReading : currentVal;
            return currentVal > 10 ? 10 : currentVal;
        }
        //</ValidateValueCallback>

        //#region MaxReading dependency property

        ////<CoerceValueAndPropertyChangedCallback>
        //// Register a dependency property with the specified property name,
        //// property type, owner type, property metadata, and callbacks.
        //public static readonly DependencyProperty MaxReadingProperty = DependencyProperty.Register(
        //    name: "MaxReading",
        //    propertyType: typeof(double),
        //    ownerType: typeof(Gauge),
        //    typeMetadata: new FrameworkPropertyMetadata(
        //        defaultValue: double.NaN,
        //        flags: FrameworkPropertyMetadataOptions.AffectsMeasure,
        //        propertyChangedCallback: new PropertyChangedCallback(OnMaxReadingChanged),
        //        coerceValueCallback: new CoerceValueCallback(CoerceMaxReading)
        //    ),
        //    validateValueCallback: new ValidateValueCallback(IsValidReading)
        //);

        //// CLR wrapper with get/set accessors.
        //public double MaxReading
        //{
        //    get => (double)GetValue(MaxReadingProperty);
        //    set => SetValue(MaxReadingProperty, value);
        //}

        //// Property changed callback.
        //private static void OnMaxReadingChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        //{
        //    Event.Logger.Add(MethodBase.GetCurrentMethod().Name);

        //    depObj.CoerceValue(MinReadingProperty);
        //    depObj.CoerceValue(CurrentReadingProperty);
        //}

        //// Coerce value callback.
        //private static object CoerceMaxReading(DependencyObject depObj, object value)
        //{
        //    Event.Logger.Add(MethodBase.GetCurrentMethod().Name);

        //    Gauge gauge = (Gauge)depObj;
        //    double maxVal = (double)value;
        //    return maxVal < gauge.MinReading ? gauge.MinReading : maxVal;
        //}
        ////</CoerceValueAndPropertyChangedCallback>

        //#endregion

        //#region MinReading dependency property

        //// Register a dependency property with the specified property name,
        //// property type, owner type, property metadata, and callbacks.
        //public static readonly DependencyProperty MinReadingProperty = DependencyProperty.Register(
        //name: "MinReading",
        //propertyType: typeof(double),
        //ownerType: typeof(Gauge),
        //typeMetadata: new FrameworkPropertyMetadata(
        //    defaultValue: double.NaN,
        //    flags: FrameworkPropertyMetadataOptions.AffectsMeasure,
        //    propertyChangedCallback: new PropertyChangedCallback(OnMinReadingChanged),
        //    coerceValueCallback: new CoerceValueCallback(CoerceMinReading)
        //),
        //validateValueCallback: new ValidateValueCallback(IsValidReading));

        //// CLR wrapper with get/set accessors.
        //public double MinReading
        //{
        //    get => (double)GetValue(MinReadingProperty);
        //    set => SetValue(MinReadingProperty, value);
        //}

        //// Property changed callback.
        //private static void OnMinReadingChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        //{
        //    Event.Logger.Add(MethodBase.GetCurrentMethod().Name);

        //    depObj.CoerceValue(MaxReadingProperty);
        //    depObj.CoerceValue(CurrentReadingProperty);
        //}

        //// Coerce value callback.
        //private static object CoerceMinReading(DependencyObject depObj, object value)
        //{
        //    Event.Logger.Add(MethodBase.GetCurrentMethod().Name);

        //    Gauge gauge = (Gauge)depObj;
        //    double minVal = (double)value;
        //    return minVal > gauge.MaxReading ? gauge.MaxReading : minVal;
        //}

        //#endregion
    }
}
