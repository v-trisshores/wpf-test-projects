using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> CallbackHistory = new();

        public MainWindow()
        {
            InitializeComponent();

            TestControl1 tc1 = new();
            tc1.Test = 6;
            var val1 = tc1.Test;
            CallbackHistory.Clear();    // break here to view CallbackHistory.

            TestControl2 tc2 = new();
            var metadata = TestControl2.TestProperty.GetMetadata(TestControl2.TestProperty.OwnerType);// as FrameworkPropertyMetadata;
            var val2 = tc2.Test;
            CallbackHistory.Clear();    // break here to view CallbackHistory.

            TestControl3 tc3 = new();
            var val3 = tc3.Test;
            CallbackHistory.Clear();    // break here to view CallbackHistory.
        }

        public class TestControl1 : ButtonBase
        {
            static FrameworkPropertyMetadata pm;

            static TestControl1()
            {
                pm = new FrameworkPropertyMetadata(
                    defaultValue: 5
                    //, propertyChangedCallback: new PropertyChangedCallback(OnPropertyChanged1), coerceValueCallback: OnCoerce1
                    )
                {
                    Inherits = true
                };
            }

            public TestControl1() : base() { }

            public int Test
            {
                get => (int)GetValue(TestProperty);
                set => SetValue(TestProperty, value);
            }

            public static readonly DependencyProperty TestProperty =
                DependencyProperty.RegisterAttached(
                    name: "Test",
                    propertyType: typeof(int),
                    ownerType: typeof(TestControl1),
                    defaultMetadata: pm);

            private static void OnPropertyChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                CallbackHistory.Add("OnPropertyChanged1");
            }

            private static object OnCoerce1(DependencyObject d, object baseValue)
            {
                CallbackHistory.Add("OnCoerce1");
                return 11;
            }
        }

        public class TestControl2 : TestControl1
        {
            static FrameworkPropertyMetadata pm;

            public TestControl2() : base() { }

            static TestControl2()
            {
                pm = new FrameworkPropertyMetadata(
                    //defaultValue: 5,
                    propertyChangedCallback: new PropertyChangedCallback(OnPropertyChanged2))
                    //coerceValueCallback: OnCoerce2)
                {
                    Inherits = true
                };
                //TestProperty.OverrideMetadata(typeof(TestControl2), pm);
            }

            private static void OnPropertyChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                CallbackHistory.Add("OnPropertyChanged2");
            }

            private static object OnCoerce2(DependencyObject d, object baseValue)
            {
                CallbackHistory.Add("OnCoerce2");
                return 22;
            }
        }

        public class TestControl3 : TestControl2
        {
            public TestControl3() : base() { }

            static TestControl3()
            {
                FrameworkPropertyMetadata newPropMetadata = new(
                    //defaultValue: 3,
                    propertyChangedCallback: new PropertyChangedCallback(OnPropertyChanged3)
                    );

                //TestProperty.OverrideMetadata(typeof(TestControl3), newPropMetadata);
            }

            private static void OnPropertyChanged3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                CallbackHistory.Add("OnPropertyChanged3");
                CallbackHistory.Add("OnPropertyChanged3: Coercing...");
                d.CoerceValue(TestProperty);
            }
        }
    }
}
