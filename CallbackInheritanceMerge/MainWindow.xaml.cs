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

            var testControl2 = new TestControl2
            {
                Test = 33
            };

            /*
TestControl2 static constructor: val = -1
TestControl1 base constructor start
    Coerce2: val = -1
    PropertyChanged2: val = -1
    Derived DoSomething is called: val = -1
TestControl1 base constructor end
TestControl2 instance constructor start: val = -1
TestControl2 instance constructor end: val = 5
Coerce2: val = 5
             */
        }

        public class TestControl1 : ButtonBase
        {
            public TestControl1()
            {
                Debug.WriteLine($"TestControl1 base constructor start");
                Test = 11;
                DoSomething();
                Debug.WriteLine($"TestControl1 base constructor end");
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
                    typeMetadata: new PropertyMetadata(defaultValue: 5)
                    );

            public virtual void DoSomething()
            {
                Debug.WriteLine($"DoSomething: Test={Test}");
            }
        }

        public class TestControl2 : TestControl1
        {
            private static int val = -1;

            public TestControl2() : base()
            {
                Debug.WriteLine($"TestControl2 instance constructor start: val = {val}");
                val = 5;
                Debug.WriteLine($"TestControl2 instance constructor end: val = {val}");
            }

            static TestControl2()
            {
                PropertyMetadata newPropMetadata = new(
                    defaultValue: 2,
                    propertyChangedCallback: new PropertyChangedCallback(PropertyChanged2),
                    coerceValueCallback: Coerce2
                    );

                TestProperty.OverrideMetadata(typeof(TestControl2), newPropMetadata);

                Debug.WriteLine($"TestControl2 static constructor: val = {val}");
            }

            private static void PropertyChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                Debug.WriteLine($"PropertyChanged2: val = {val}");
            }

            private static object Coerce2(DependencyObject d, object baseValue)
            {
                Debug.WriteLine($"Coerce2: val = {val}");
                return 22;
            }

            public override void DoSomething()
            {
                Debug.WriteLine($"Derived DoSomething is called: val = {val}");
            }
        }
    }
}
