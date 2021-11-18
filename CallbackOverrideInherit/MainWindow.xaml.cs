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
            TestControl1.SetTest(tc1, 6);
            var val1 = tc1.Test;

            TestControl2 tc2 = new();
            var val2 = tc2.Test;

            TestControl3 tc3 = new();
            var val3 = tc3.Test;    // val3 is 5 (default value), not 6 (local value). If inherits is `false', then val3 is 0.

            // Conclusion: the inherits flag makes the "default value" inheritable, not the "local value".
        }

        public class TestControl1 : ButtonBase
        {
            static FrameworkPropertyMetadata pm;

            static TestControl1()
            {
                pm = new FrameworkPropertyMetadata(defaultValue: 5)
                {
                    Inherits = true
                };

                TestProperty =
                 DependencyProperty.RegisterAttached(
                     name: "Test",
                     propertyType: typeof(int),
                     ownerType: typeof(TestControl1),
                     defaultMetadata: pm);
            }

            public TestControl1() : base() { }

            public int Test
            {
                get => GetTest(this);
                set => SetTest(this, value);
            }

            // Declare a get accessor method.
            public static int GetTest(DependencyObject target) =>
                (int)target.GetValue(TestProperty);

            // Declare a set accessor method.
            public static void SetTest(DependencyObject target, int value) =>
                target.SetValue(TestProperty, value);

            public static readonly DependencyProperty TestProperty;
        }

        public class TestControl2 : TestControl1
        {
            public TestControl2() : base() 
            { }

            static TestControl2()
            {
                FrameworkPropertyMetadata newPropMetadata = new();
                //FrameworkPropertyMetadata newPropMetadata = new(defaultValue: 8);

                TestProperty.OverrideMetadata(typeof(TestControl2), newPropMetadata);
            }
        }

        public class TestControl3 : TestControl2
        {
            public TestControl3() : base() 
            { }
        }
    }
}
