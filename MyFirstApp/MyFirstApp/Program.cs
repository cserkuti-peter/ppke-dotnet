using MyFirstLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;

namespace MyFirstApp
{ 
    class Program
    {
        static void Main(string[] args)
        {
            #region Lab #1
            //  1. Calling a method from a project reference
            var s = "Hello world! My name is Peter";    //  var: implicit typed local varialbe, need to be initialized
            var words = Helpers.GetWords(s);
            foreach (var w in words)    //  foreach for IEnumerables
            {
                Console.WriteLine(w);
            }

            //  2. Using nuget packages
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Hello world!"));
            #endregion

            #region Lab #2
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

            //  Object initializer
            var p_ = new Person
            {
                FirstName = "Peter",
                SecondName = "Smith"
            };
            #endregion

            #region Lab #3
            //  7. Delegates
            //  Pointing to a method with a delegate and calling the method through the delegate.
            WriteSomethingDlgt func = WriteSomething;
            func("Hello1");

            //  One delegate can point to multiple methods
            //  Anonymous method
            func += delegate (string message)
            {
                Console.WriteLine($"Message from anonymous function: {message}");
            };
            func("Hello2");

            //  Lambda expression can be converted to delegate type.
            func += message => Console.WriteLine($"Message from lambda expression: {message}");
            func("Hello3");

            //  8. Some more examples for lambda expression
            //  a) Lambda as delegate
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5));

            //  b) Lambda as expression tree
            System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
            Console.WriteLine(e);

            //  Compile an expression tree and execute
            var comp = e.Compile();
            Console.WriteLine(comp(3));

            //  9. Logger: delegate for different log sources
            //  Configure at startup
            Logger.WriteMessage += LoggingMethods.LogToConsole;                         //  point to a method
            Logger.WriteMessage += delegate (string msg) { Debug.WriteLine(msg); };     //  using anonymous method
            Logger.WriteMessage += msg => {                                             //  using statement lambda
                //  Files wrapping unmanaged resources need to closed safely (in finally) and asap (dont wait for the GC).
                //  This is what using is good for. Calls Dispose
                using (var sw = File.AppendText("output.txt"))
                {
                    sw.WriteLine(msg);
                    sw.Flush();
                }
            };

            //  Fire somewhere else
            Logger.WriteMessage("Something has happened.");

            //  10. Event handling
            var px = new Person();
            px.FirstNameChanged += 
                (source, arg) =>
                    Console.WriteLine($"Firstname changed, {((Person)source).FirstName}");

            px.FirstName = "David";

            //  11. Null-conditional/propagating operator, Null-coalescing and null-coalescing assignment
            //  a) Null-coalescing
            int? x1 = null;
            int x2 = x1 ?? -1;
            Console.WriteLine(x2);  // output: -1

            //  b) Null-coalescing assignment
            List<int> numbers = null;
            (numbers ??= new List<int>()).Add(5);
            Console.WriteLine(string.Join(" ", numbers));  // output: 5

            int? x3 = null;
            x3 ??= 0;
            Console.WriteLine(a);  // output: 0

            //  c) Null-propagating
            Func<string> f1 = null;
            Console.WriteLine(f1?.Invoke() ?? "Nothing");

            Func<string> f2 = () => "Something";
            Console.WriteLine(f2?.Invoke() ?? "Nothing");

            //  12. Collections
            //      - System.Collections (items are objects)
            //      - System.Collections.Generic (generic collections)
            //      - System.Collections.Concurrent (threadsafe for multithreaded applications)

            //  List
            List<Person> list_ = new List<Person>();
            list_.Add(new Person { FirstName = "Peter" });
            list_.Add(new Person { FirstName = "David" });
            list_.Add(new Person { FirstName = "Dora" });
            
            //  Collection initializer
            var list = new List<Person>
            {
                new Person { FirstName = "Peter" },
                new Person { FirstName = "David" },
                new Person { FirstName = "Dora" }
            };

            foreach (var x in list)
            {
                Console.WriteLine(x);
            }

            list.ForEach(x => Console.WriteLine(x));

            list.Sort((p1, p2) => String.Compare(p1.FirstName, p2.FirstName));

            list.ForEach(x => Console.WriteLine(x));

            //  Dictionary (key-value pairs)
            var dict = new Dictionary<int, Person>();
            var p1 = new Person { FirstName = "Cili" };
            var p2 = new Person { FirstName = "Luca" };
            dict.Add(p1.Id, p1);
            dict.Add(p2.Id, p2);

            list.ForEach(p => dict.Add(p.Id, p));

            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            var p1byId = dict[p1.Id];
            Console.WriteLine($"Person with Id {p1.Id} is {p1byId}");

            //  Reflection
            //  - using built-in attrubute (DebuggerDisplay)
            //  - using custom attributes
            //  - querying custom attributes and discovering type runtime
            PrettyPrinter.Print(new Person { FirstName = "Cili" });
            #endregion
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

        private delegate void WriteSomethingDlgt(string message);

        private static void WriteSomething(string message)
        {
            Console.WriteLine($"Message from function: {message}");
        }
    }
}
