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
        public MainWindow()
        {
            InitializeComponent();

            //TestControl1 tc1 = new();
            //tc1.Test = 11;

            TestControl2 tc2 = new();
            TestControl3 tc3 = new();
            tc2.Test++;
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
                    typeMetadata: new PropertyMetadata(defaultValue: 1));

            public static readonly DependencyProperty Test2Property =
                DependencyProperty.Register(
                    name: "Test2",
                    propertyType: typeof(int),
                    ownerType: typeof(TestControl1),
                    typeMetadata: new PropertyMetadata(defaultValue: 1));
        }

        static PropertyMetadata newPropMetadata2 = new(defaultValue: 2);

        public class TestControl2 : TestControl1
        {
            public TestControl2() : base()
            {
                PropertyMetadata newPropMetadata = new(defaultValue: 2);

                //TestProperty.OverrideMetadata(typeof(TestControl2), newPropMetadata);
            }

            static TestControl2()
            {
                PropertyMetadata newPropMetadata = new(defaultValue: 2);

                TestProperty.OverrideMetadata(typeof(TestControl2), newPropMetadata);
            }
        }
        public class TestControl3 : TestControl1
        {
            public TestControl3() : base()
            {
            }

            static TestControl3()
            {
                TestProperty.OverrideMetadata(typeof(TestControl2), newPropMetadata2);
            }
        }
    }
}
