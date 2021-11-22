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

            // TestProperty registered with metadata now.

            //TestControl1.Tester();  // System.InvalidOperationException: "Cannot change property metadata after it has been associated with a property."

            TestControl1.fpm.DefaultValue = 22;  // System.InvalidOperationException: "Cannot change property metadata after it has been associated with a property."
            TestControl1 tc1 = new();
            TestControl1.fpm.DefaultValue = 33;  // System.InvalidOperationException: "Cannot change property metadata after it has been associated with a property."
            tc1.Test = 11;

        }

        public class TestControl1 : ButtonBase
        {
            public static FrameworkPropertyMetadata fpm = new();

            static TestControl1()
            {
                FrameworkPropertyMetadataOptions flags = (FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);
                fpm = new(defaultValue: 1, flags);
                fpm.AffectsRender = false;

                TestProperty =
                DependencyProperty.Register(
                    name: "Test",
                    propertyType: typeof(int),
                    ownerType: typeof(TestControl1),
                    typeMetadata: fpm);
            }

            public int Test
            {
                get => (int)GetValue(TestProperty);
                set => SetValue(TestProperty, value);
            }

            public static readonly DependencyProperty TestProperty;
        }
    }
}