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

            TestControl1 tc1 = new();
            TestControl2 tc2 = new();


            FrameworkPropertyMetadata metadata1 = TestControl1.TestProperty.GetMetadata(tc1) as FrameworkPropertyMetadata;
            FrameworkPropertyMetadata metadata2 = TestControl2.TestProperty.GetMetadata(tc2) as FrameworkPropertyMetadata;
        }

        public class TestControl1 : ButtonBase
        {
            public static FrameworkPropertyMetadata fpm = new();

            public TestControl1() : base() 
            {
            }

            public static void Tester()
            {
            }

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
                    typeMetadata: fpm);

            public static readonly DependencyProperty Test2Property =
                DependencyProperty.Register(
                    name: "Test2",
                    propertyType: typeof(int),
                    ownerType: typeof(TestControl1));
        }

        static PropertyMetadata newPropMetadata2 = new(defaultValue: 2);

        public class TestControl2 : TestControl1
        {
            public TestControl2() : base()
            {
            }

            static TestControl2()
            {
                var fpm = new FrameworkPropertyMetadata(defaultValue: 1);
                fpm.AffectsArrange = !fpm.AffectsArrange;
                fpm.AffectsMeasure = !fpm.AffectsMeasure;
                fpm.AffectsParentArrange = !fpm.AffectsParentArrange;
                fpm.AffectsParentMeasure = !fpm.AffectsParentMeasure;
                fpm.AffectsRender = !fpm.AffectsRender;
                fpm.BindsTwoWayByDefault = !fpm.BindsTwoWayByDefault;
                fpm.Inherits = !fpm.Inherits;
                fpm.IsAnimationProhibited = !fpm.IsAnimationProhibited;
                fpm.IsNotDataBindable = !fpm.IsNotDataBindable;
                fpm.Journal = !fpm.Journal;
                fpm.OverridesInheritanceBehavior = !fpm.OverridesInheritanceBehavior;
                fpm.SubPropertiesDoNotAffectRender = !fpm.SubPropertiesDoNotAffectRender;

                TestProperty.OverrideMetadata(typeof(TestControl2), fpm);
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