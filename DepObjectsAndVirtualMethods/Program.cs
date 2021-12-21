using System;
using System.Diagnostics;

namespace VirtualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dc = new DerivedClass();
            var bc = new BaseClass();

            var testVal = bc.GetInt();
            Debug.WriteLine($"Base constructor: {testVal}");
        }

        public class BaseClass
        {
            public BaseClass()
            {
                var testVal = GetInt();
                Debug.WriteLine($"Base constructor: {testVal}");
            }

            public virtual int GetInt()
            {
                return -1;
            }
        }

        public class DerivedClass : BaseClass
        {
            int test = 0;

            public DerivedClass()
            {
                Debug.WriteLine($"Derived constructor ran");
                test = 5;
            }

            public override int GetInt()
            {
                return test;
            }
        }
    }
}
