using MyFirstLib;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;

namespace MyFirstApp
{ 
    class Program
    {
        static void Main(string[] args)
        {
            //  1. Calling a method from a project reference
            var s = "Hello world! My name is Peter";    //  var: implicit typed local varialbe, need to be initialized
            var words = Helpers.GetWords(s);
            foreach (var w in words)    //  foreach for IEnumerables
            {
                Console.WriteLine(w);
            }

            //  2. Using nuget packages
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Hello world!"));

            //  3. Using enums
            Colors color = Colors.Red;
            color = (Colors)2;
            Console.WriteLine(color);

            //  Flagged enums
            Days meetingDays = Days.Monday | Days.Wednesday;
            bool isMeetingOnTuesday = (meetingDays & Days.Tuesday) == Days.Tuesday;
            Console.WriteLine($"There is a meeting on Tuesday: {isMeetingOnTuesday}");

            //  4. Nullable types (value types + null value)
            Nullable<int> i = null;
            //  int? i = null;  //  on other syntax to define a nullable type
            i = 1;
            if (i.HasValue)
            {
                int k = i.Value;
            }
            Console.WriteLine(i);

            //  5. Tuple types
            (double, int) t1 = (1.2, 2);
            Console.WriteLine(t1);

            var t2 = (1.2, 2);
            Console.WriteLine(t2);

            var t3 = (Sum: 2, Max: 3);      //  Give explicit field names
            Console.WriteLine(t3);

            (int a, int b) = t3;    //  Deconstruction
            Console.WriteLine($"{a}, {b}");

            //  Most commonly used as return types of a function to return multiple values
            var limits = FindMinMax(new int[] { 1, 2, 3, 5, -10 });

            //  5. Boxing and unboxing  -  every type implicitly derives from the object base class
            object o1 = new { };
            object o2 = 1;          // boxing (value type -> reference type, implicit)
            int n = (int)o2;        // unboxing (reference type -> value type, explicit)s
            Console.WriteLine($"{o2}, {n}");

            //  6. Using properties
            Person p = new Person();
            p.FirstName = "David";
            p.SecondName = "Smith";
            Console.WriteLine($"{p.FirstName} {p.SecondName}, {p.FullName}");
        }

        private static (int min, int max) FindMinMax(int[] input)
        {
            var min = int.MaxValue;
            var max = int.MinValue;

            foreach (var i in input)
            {
                if (i < min)
                    min = i;
                if (i > max)
                    max = i;
            }

            return (min, max);
        }
    }
}
