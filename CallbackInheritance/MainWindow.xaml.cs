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
            tc1.Test++;
            CallbackHistory.Clear();    // break here to view CallbackHistory.

            TestControl2 tc2 = new();
            tc2.Test++;
            CallbackHistory.Clear();    // break here to view CallbackHistory.

            TestControl3 tc3 = new();
            tc3.Test++;
            CallbackHistory.Clear();    // break here to view CallbackHistory.
        }

        public class TestControl1 : ButtonBase
        {
            public TestControl1() : base() { }

            public int Test
            {
                get => (int)GetValue(TestProperty);
                set => SetValue(TestProperty, value);
            }

            public static readonly DependencyProperty TestProperty =
                DependencyProperty.Register(
                    name: "Test",
                    propertyType: typeof(int),
                    ownerType: typeof(TestControl1),
                    typeMetadata: new PropertyMetadata(
                        defaultValue: 1,
                        propertyChangedCallback: new PropertyChangedCallback(OnCurrentReadingChanged1),
                        coerceValueCallback: OnCoerce1)
                    );

            private static void OnCurrentReadingChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                CallbackHistory.Add("TestControl1");
            }

            private static object OnCoerce1(DependencyObject d, object baseValue)
            {
                CallbackHistory.Add("OnCoerce1");
                return 11;
            }
        }

        public class TestControl2 : TestControl1
        {
            public TestControl2() : base() { }

            static TestControl2()
            {
                PropertyMetadata newPropMetadata = new(
                    defaultValue: 2,
                    propertyChangedCallback: new PropertyChangedCallback(OnCurrentReadingChanged2),
                    coerceValueCallback: OnCoerce2
                    );

                TestProperty.OverrideMetadata(typeof(TestControl2), newPropMetadata);
            }

            private static void OnCurrentReadingChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                CallbackHistory.Add("TestControl2");
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
                PropertyMetadata newPropMetadata = new(
                    defaultValue: 3,
                    propertyChangedCallback: new PropertyChangedCallback(OnCurrentReadingChanged3)
                    );

                TestProperty.OverrideMetadata(typeof(TestControl3), newPropMetadata);
            }

            private static void OnCurrentReadingChanged3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                CallbackHistory.Add("TestControl3");
            }
        }
    }
}
