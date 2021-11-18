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

            // Using class inheritance to inherit DP.

            TestControl1 tc1 = new();
            //tc1.Test = 6;
            TestControl1.SetTest(tc1, 6);
            int val1 = TestControl1.GetTest(tc1);

            TestControl2 tc2 = new();
            //tc1.Test = 6;
            TestControl1.SetTest(tc1, 6);
            int val2 = TestControl1.GetTest(tc2);

            TestControl3 tc3 = new();
            //tc1.Test = 6;
            TestControl1.SetTest(tc1, 6);
            int val3 = TestControl1.GetTest(tc3);    // val3 is 5 (default value), not 6 (local value of base DP).

            // Per docs, use `RegisterAttached` for DP value inheritance to work properly.
            // Test result: the inherits flag has no effect.
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
                     pm);
            }

            public TestControl1() : base() { }

            public int Test
            {
                get => (int)GetValue(TestProperty);
                set => SetValue(TestProperty, value);
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
                newPropMetadata.Inherits = true;

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
