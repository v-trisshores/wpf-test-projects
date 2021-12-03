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
                Debug.WriteLine("Attemting to set a read-write property value...");
                DependencyProperty dp = locallySetProperties1.Current.Property;
                _aquarium.SetValue(dp, 2);
            }

            AquariumReadOnly _aquariumReadOnly = new();
            LocalValueEnumerator locallySetProperties2 = _aquariumReadOnly.GetLocalValueEnumerator();
            while (locallySetProperties2.MoveNext())
            {
                DependencyProperty dp = locallySetProperties2.Current.Property;
                try
                {
                    Debug.WriteLine("Attemting to set a read-only property value...");
                    _aquariumReadOnly.SetValue(dp, 2);
                }
                catch (InvalidOperationException e)
                {
                    Debug.WriteLine(e.Message);
                    // 'FishCount' property was registered as read-only and cannot be modified without an authorization key.
                }
            }

            Debug.WriteLine($"_aquarium.FishCount: {_aquarium.FishCount}");
            // _aquarium.FishCount: 2

            Debug.WriteLine($"_aquariumReadOnly.FishCount: {_aquariumReadOnly.FishCount}");
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