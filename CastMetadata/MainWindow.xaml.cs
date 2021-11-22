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
            var metadata1 = TestControl1.TestProperty.GetMetadata(tc1) as FrameworkPropertyMetadata;

            TestControl2 tc2 = new();
            var metadata2 = TestControl2.TestProperty.GetMetadata(tc2);
        }

        public class TestControl1 : ButtonBase
        {
            public static FrameworkPropertyMetadata fpm = new();

            static TestControl1()
            {
                FrameworkPropertyMetadataOptions flags = (FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);
                fpm = new(defaultValue: 1, flags);
                fpm.AffectsRender = true;

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

        public class TestControl2 : ButtonBase
        {
            public static PropertyMetadata pm = new();

            static TestControl2()
            {
                pm = new PropertyMetadata(
                        defaultValue: 5,
                        propertyChangedCallback: new PropertyChangedCallback(OnPropertyChanged1),
                        coerceValueCallback: OnCoerce1);

                TestProperty =
                DependencyProperty.Register(
                    name: "Test",
                    propertyType: typeof(int),
                    ownerType: typeof(TestControl2),
                    typeMetadata: pm);
            }

            private static void OnPropertyChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
            }

            private static object OnCoerce1(DependencyObject d, object baseValue)
            {
                return 11;
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