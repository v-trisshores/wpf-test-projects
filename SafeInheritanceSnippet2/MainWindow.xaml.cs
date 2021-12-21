using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WpfApp1
{
    // Snippet 1 from safe-constructor-patterns-for-dependencyobjects
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            new MyClass(new object());

            /*

             */
        }

        public class MyBaseClass : DependencyObject
        {
            public MyBaseClass()
            {
                Debug.WriteLine("MyBaseClass constructor");
            }
        }

        public class MyClass : MyBaseClass
        {
            public MyClass()
            {
                Debug.WriteLine("MyClass parameterless constructor");
                _myList = new List<object>();
            }

            public MyClass(object toSetWobble) : this()
            {
                Debug.WriteLine("MyClass parameter constructor");
                Wobble = toSetWobble; //this is backed by a DependencyProperty  
                _myList = new List<object>();    // this line should be in the default ctor  
            }

            public static readonly DependencyProperty WobbleProperty =
                DependencyProperty.Register("Wobble", typeof(object), typeof(MyClass), new PropertyMetadata(new object()));

            public object Wobble
            {
                get => GetValue(WobbleProperty);
                set => SetValue(WobbleProperty, value);
            }

            protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
            {
                int count = _myList.Count;    // null-reference exception  
            }

            private List<object> _myList;
        }
    }
}
