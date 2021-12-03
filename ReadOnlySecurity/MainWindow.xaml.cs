using System;
using System.Diagnostics;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Aquarium _aquarium = new();
            LocalValueEnumerator locallySetProperties1 = _aquarium.GetLocalValueEnumerator();
            while (locallySetProperties1.MoveNext())
            {
                DependencyProperty dp = locallySetProperties1.Current.Property;

                Debug.WriteLine("Attempting to get a read-write property value...");
                int val = (int)_aquarium.GetValue(dp);
                Debug.WriteLine($"_aquarium.FishCount: {val}");

                try
                {
                    Debug.WriteLine("Attempting to set a read-write property value to 2...");
                    _aquarium.SetValue(dp, 2);
                }
                finally
                {
                    Debug.WriteLine($"_aquarium.FishCount: {_aquarium.FishCount}");
                }
            }

            AquariumReadOnly _aquariumReadOnly = new();
            LocalValueEnumerator locallySetProperties2 = _aquariumReadOnly.GetLocalValueEnumerator();
            while (locallySetProperties2.MoveNext())
            {
                DependencyProperty dp = locallySetProperties2.Current.Property;
                try
                {
                    Debug.WriteLine("Attempting to get a read-only property value...");
                    int val = (int)_aquariumReadOnly.GetValue(dp);
                    Debug.WriteLine($"_aquariumReadOnly.FishCount: {val}");

                    Debug.WriteLine("Attempting to set a read-only property value to 2...");
                    _aquariumReadOnly.SetValue(dp, 2);
                }
                catch (InvalidOperationException e)
                {
                    Debug.WriteLine(e.Message);
                }
                finally
                {
                    Debug.WriteLine($"_aquariumReadOnly.FishCount: {_aquariumReadOnly.FishCount}");
                }
            }
          
            // Attempting to get a read-write property value...
            // _aquarium.FishCount: 1
            // Attempting to set a read-write property value to 2...
            // _aquarium.FishCount: 2

            // Attempting to get a read-only property value...
            // _aquariumReadOnly.FishCount: 1
            // Attempting to set a read-only property value to 2...
            // 'FishCount' property was registered as read-only and cannot be modified without an authorization key.
            // _aquariumReadOnly.FishCount: 1
        }
    }

    public class Aquarium : DependencyObject
    {
        // Assign DependencyProperty to a private field.
        private static readonly DependencyProperty FishCountProperty =
            DependencyProperty.Register(
              name: "FishCount",
              propertyType: typeof(int),
              ownerType: typeof(Aquarium),
              typeMetadata: new FrameworkPropertyMetadata());

        public Aquarium()
        {
            // Assign a locally-set value.
            SetValue(FishCountProperty, 1);
        }

        // Declare a public get accessor.
        public int FishCount => (int)GetValue(FishCountProperty);

        //protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    base.OnPropertyChanged(e);
        //}
    }

    public class AquariumReadOnly : DependencyObject
    {
        // Assign DependencyProperty to a private field.
        private static readonly DependencyPropertyKey FishCountPropertyKey =
            DependencyProperty.RegisterReadOnly(
              name: "FishCount",
              propertyType: typeof(int),
              ownerType: typeof(AquariumReadOnly),
              typeMetadata: new FrameworkPropertyMetadata());

        public AquariumReadOnly()
        {
            // Assign a locally-set value.
            SetValue(FishCountPropertyKey, 1);
        }

        // Declare a public get accessor.
        public int FishCount => (int)GetValue(FishCountPropertyKey.DependencyProperty);

        //protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    base.OnPropertyChanged(e);
        //}
    }
}