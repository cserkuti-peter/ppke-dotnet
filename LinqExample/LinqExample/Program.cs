using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace LinqExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Querying on different data sources
            //  - collections
            //  - database
            //  - xml

            //  Example
            var students = new List<Student>
            {
                new Student { Name = "Dávid", Age = 11, School = "Elementary 1" },
                new Student { Name = "Dóra", Age = 9, School = "Elementary 1" },
                new Student { Name = "Luca", Age = 21, School = "University 3" }
            };

            //  Query syntax
            var q1 = from s in students
                    where s.Age > 10
                    orderby s.Age
                    select new
                    {
                        s.Name,
                        s.Age
                    };

            //  Fluent syntax
            var q2 = students
                .Where(x => x.Age > 10)
                .OrderBy(x => x.Age)
                .Select(x => new
                {
                    x.Name,
                    x.Age
                });

            foreach (var x in q1)   //  only executes as we iterate through it
            {
                Console.WriteLine($"{x.Name}, {x.Age}");
            }

            //  How would we solve it
            //  1. Starting point
            //  not flexible, hard to reuse with different queries
            var q3 = new List<StudentTmp>();
            foreach (var x in students)
            {
                if (x.Age > 10)
                {
                    q3.Add(new StudentTmp { Name = x.Name, Age = x.Age });
                }
            }

            //  2. Make querying and selecting reusable
            var q4 = 
                QueryExtensions.SelectStudentTmp(
                    QueryExtensions.WhereStudent(students, x => x.Age > 10),
                    x => new StudentTmp { Name = x.Name, Age = x.Age });

            //  3. Use extension methods
            var q5 = students
                .WhereStudent(x => x.Age > 10)
                .SelectStudentTmp(x => new StudentTmp { Name = x.Name, Age = x.Age });

            //  4. Make it generic
            var q6 = students
                .WhereGeneric(x => x.Age > 10)
                .SelectGeneric(x => new { Name = x.Name, Age = x.Age });

            //  5. Use yield return
            var q7 = students
                .WhereGenericWithYield(x => x.Age > 10)
                .SelectGenericWithYield(x => new { Name = x.Name, Age = x.Age });

            //  Differed execution
            foreach (var x in q7)  //pulling items from the source like joined conveyors
            {
                Console.WriteLine(x);
            }

            //  Some examples
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Clothes"},
                new Category { Id = 2, Name = "Drinks"},
                new Category { Id = 3, Name = "Cars"},
            };

            var products = new List<Product>
            {
                new Product { Id = 1, CategoryId = 1, Name = "Jeans", Price = 200},
                new Product { Id = 2, CategoryId = 2, Name = "Beer", Price = 2 },
            };

            //  Count of products more expensive then $100?
            var count1 = products.Where(x => x.Price > 100).Count();
            var count2 = (from x in products
                            where x.Price > 100
                            select x).Count();

            //  Join products and categories with query syntax
            var productsWithCategories1 =
                from p in products
                from c in categories
                where p.CategoryId == c.Id
                select new
                {
                    Product = p.Name,
                    Category = c.Name
                };

            //  Join products and categories with fluent syntax
            var productsWithCategories2 =
                products.Join(categories, p => p.CategoryId, c => c.Id, (p, c) => new { Product = p.Name, Category = c.Name });

            //  Join products and categories with the join keyword
            var productsWithCategories3 =
                from p in products
                join c in categories on p.CategoryId equals c.Id
                select new
                {
                    Product = p.Name,
                    Category = c.Name
                };

            //  Left outer join products and categories with group join
            var productsWithCategories4 =
                from c in categories
                join p in products on c.Id equals p.CategoryId into prodsOfCat
                from pc in prodsOfCat.DefaultIfEmpty()
                select new
                {
                    Category = c.Name,
                    Product = pc?.Name ?? String.Empty,
                    Count = prodsOfCat.Count()
                };

            var result = productsWithCategories4.ToList();

            //  Strings with uppercase letters
            var words = new string[] { "Hello", "HI", "How are you?" }.Where(word => word.All(ch => Char.IsUpper(ch))).ToList();

            //  Get each different chars of the words
            var chars = new string[] { "Hello", "HI", "How are you?" }
                .SelectMany(word => word.Select(ch => char.ToLower(ch))).Distinct().ToList();

            //  Get the first 5 square number
            var nums = Enumerable.Range(1, 5).Select(x => x * x).ToList();

            //  How would you implement the Range method - yield return

            //  OrderBy, All, Any, Contains, Max, Sum, ...

            //  LINQ to objects: IEnumerable<T> => IEnumerable<T>

            //  LINQ to Entities: IQueryable<T> => IQueryable<T>
            //      compiled to expression tree
            //      queries against EF models

            //  IQueryable relies on building expression trees, see EF

            // IQueryable<TSource> Where vs IEnumerable<TSource> Where - Func vs Expression
            IQueryable<Student> q = null;
            q = q.Where(_ => true);
        }
    }

    public class Product 
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class QueryExtensions
    {
        public static List<Student> WhereStudent(this List<Student> students, Predicate<Student> pred)
        {
            var list = new List<Student>();
            foreach (var x in students)
            {
                if (pred(x))
                {
                    list.Add(x);
                }
            }
            return list;
        }

        public static List<StudentTmp> SelectStudentTmp(this List<Student> students, Func<Student, StudentTmp> studentTmpSelector)
        {
            var list = new List<StudentTmp>();
            foreach (var x in students)
            {
                list.Add(studentTmpSelector(x));
            }
            return list;
        }

        public static IEnumerable<T> WhereGeneric<T>(this IEnumerable<T> source, Predicate<T> pred)
        {
            var list = new List<T>();
            foreach (var x in source)
            {
                if (pred(x))
                {
                    list.Add(x);
                }
            }
            return list;
        }

        public static IEnumerable<TTarget> SelectGeneric<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
        {
            var list = new List<TTarget>();
            foreach (var x in source)
            {
                list.Add(selector(x));
            }
            return list;
        }

        public static IEnumerable<T> WhereGenericWithYield<T>(this IEnumerable<T> source, Predicate<T> pred)
        {
            foreach (var x in source)
            {
                if (pred(x))
                {
                    yield return x;
                }
                else
                {
                    continue;
                }
            }
        }

        public static IEnumerable<TTarget> SelectGenericWithYield<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
        {
            foreach (var x in source)
            {
                yield return selector(x);
            }
        }
    }

    public class StudentTmp
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string School { get; set; }
    }
}
