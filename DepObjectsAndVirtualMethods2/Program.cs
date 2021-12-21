using System;
using System.Diagnostics;

namespace VirtualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new DerivedClass();

            // Base constructor: GetInt()=0
            // Derived constructor: GetInt()=5
        }

        public class BaseClass
        {
            public int test;

            public BaseClass()
            {
                Debug.WriteLine($"Base constructor: GetInt()={GetInt()}");
            }

            public virtual int GetInt()
            {
                return -1;
            }
        }

        public class DerivedClass : BaseClass
        {

            public DerivedClass()
            {
                test = 5;
                Debug.WriteLine($"Derived constructor: GetInt()={GetInt()}");
            }

            public override int GetInt()
            {
                return test;
            }
        }
    }
}
