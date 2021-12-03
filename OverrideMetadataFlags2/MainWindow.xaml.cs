using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var defaultMetadata = new FrameworkPropertyMetadata();
            Debug.WriteLine($"\r\nAffectsArrange = {defaultMetadata.AffectsArrange}");  // false
            Debug.WriteLine($"AffectsMeasure = {defaultMetadata.AffectsMeasure}");  // false
            Debug.WriteLine($"AffectsParentArrange = {defaultMetadata.AffectsParentArrange}");  // false
            Debug.WriteLine($"AffectsParentMeasure = {defaultMetadata.AffectsParentMeasure}");  // false
            Debug.WriteLine($"AffectsRender = {defaultMetadata.AffectsRender}");  // false
            Debug.WriteLine($"BindsTwoWayByDefault = {defaultMetadata.BindsTwoWayByDefault}");  // false
            Debug.WriteLine($"IsNotDataBindable = {defaultMetadata.IsNotDataBindable}");  // false
            Debug.WriteLine($"Inherits = {defaultMetadata.Inherits}");  // false
            Debug.WriteLine($"Journal = {defaultMetadata.Journal}");  // false
            Debug.WriteLine($"OverridesInheritanceBehavior = {defaultMetadata.OverridesInheritanceBehavior}");  // false
            Debug.WriteLine($"SubPropertiesDoNotAffectRender = {defaultMetadata.SubPropertiesDoNotAffectRender}\r\n");  // false

            FrameworkPropertyMetadata metadata1 = TestControl1.TestProperty.GetMetadata(tc1) as FrameworkPropertyMetadata;
            // All can be overriden from false to true.
            // These can't be overriden from true to false:
            Debug.WriteLine($"\r\nAffectsArrange = {metadata1.AffectsArrange}");
            Debug.WriteLine($"AffectsMeasure = {metadata1.AffectsMeasure}");
            Debug.WriteLine($"AffectsParentArrange = {metadata1.AffectsParentArrange}");
            Debug.WriteLine($"AffectsParentMeasure = {metadata1.AffectsParentMeasure}");
            Debug.WriteLine($"AffectsRender = {metadata1.AffectsRender}");
            Debug.WriteLine($"BindsTwoWayByDefault = {metadata1.BindsTwoWayByDefault}");
            Debug.WriteLine($"IsNotDataBindable = {metadata1.IsNotDataBindable}");
            // These can be overriden from true to false:
            Debug.WriteLine($"Inherits = {metadata1.Inherits}");
            Debug.WriteLine($"Journal = {metadata1.Journal}");
            Debug.WriteLine($"OverridesInheritanceBehavior = {metadata1.OverridesInheritanceBehavior}");
            Debug.WriteLine($"SubPropertiesDoNotAffectRender = {metadata1.SubPropertiesDoNotAffectRender}\r\n");

            FrameworkPropertyMetadata metadata2 = TestControl2.TestProperty.GetMetadata(tc2) as FrameworkPropertyMetadata;
            // All can be overriden from false to true.
            // These can't be overriden from true to false:
            Debug.WriteLine($"AffectsArrange = {metadata2.AffectsArrange}");
            Debug.WriteLine($"AffectsMeasure = {metadata2.AffectsMeasure}");
            Debug.WriteLine($"AffectsParentArrange = {metadata2.AffectsParentArrange}");
            Debug.WriteLine($"AffectsParentMeasure = {metadata2.AffectsParentMeasure}");
            Debug.WriteLine($"AffectsRender = {metadata2.AffectsRender}");
            Debug.WriteLine($"BindsTwoWayByDefault = {metadata2.BindsTwoWayByDefault}");
            Debug.WriteLine($"IsNotDataBindable = {metadata2.IsNotDataBindable}");
            // These can be overriden from true to false:
            Debug.WriteLine($"Inherits = {metadata2.Inherits}");
            Debug.WriteLine($"Journal = {metadata2.Journal}");
            Debug.WriteLine($"OverridesInheritanceBehavior = {metadata2.OverridesInheritanceBehavior}");
            Debug.WriteLine($"SubPropertiesDoNotAffectRender = {metadata2.SubPropertiesDoNotAffectRender}");
        }

        public class TestControl1 : ButtonBase
        {
            public static FrameworkPropertyMetadata fpm;

            public TestControl1() : base() 
            {
            }

            static TestControl1()
            {
                // Change all the default values.
                bool boolVal = true;
                var fpm = new FrameworkPropertyMetadata(defaultValue: 1)
                {
                    AffectsArrange = boolVal,
                    AffectsMeasure = boolVal,
                    AffectsParentArrange = boolVal,
                    AffectsParentMeasure = boolVal,
                    AffectsRender = boolVal,
                    BindsTwoWayByDefault = boolVal,
                    Inherits = boolVal,
                    IsAnimationProhibited = boolVal,
                    IsNotDataBindable = boolVal,
                    Journal = boolVal,
                    OverridesInheritanceBehavior = boolVal,
                    SubPropertiesDoNotAffectRender = boolVal
                };

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

        public class TestControl2 : TestControl1
        {
            public TestControl2() : base()
            {
            }

            static TestControl2()
            {
                bool boolVal = false;
                var fpm = new FrameworkPropertyMetadata(defaultValue: 2)
                {
                    AffectsArrange = boolVal,
                    AffectsMeasure = boolVal,
                    AffectsParentArrange = boolVal,
                    AffectsParentMeasure = boolVal,
                    AffectsRender = boolVal,
                    BindsTwoWayByDefault = boolVal,
                    Inherits = boolVal,
                    IsAnimationProhibited = boolVal,
                    IsNotDataBindable = boolVal,
                    Journal = boolVal,
                    OverridesInheritanceBehavior = boolVal,
                    SubPropertiesDoNotAffectRender = boolVal
                };

                TestProperty.OverrideMetadata(typeof(TestControl2), fpm);
            }
        }
    }
}