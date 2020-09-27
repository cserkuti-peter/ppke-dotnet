using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace MyFirstApp
{
    public static class PrettyPrinter
    {
        public static void Print(object obj)
        {
            var classAttr  = (PrettyPrintableAttribute)Attribute.GetCustomAttribute(
                obj.GetType(), typeof(PrettyPrintableAttribute));

            if (classAttr == null)
                throw new ArgumentException("Type is not prettyprintable.", nameof(obj));

            var propInfos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propInfos)
            {
                var propAttr = (PrettyPrintAttribute)Attribute.GetCustomAttribute(prop, typeof(PrettyPrintAttribute));

                if (propAttr != null)
                {
                    var propValue = prop.GetValue(obj).ToString();

                    propValue = propAttr.Capitalize 
                        ? propValue = propValue.ToUpper()
                        : propValue = propValue.ToLower();

                    Console.WriteLine($"{prop.Name}: {propValue}");
                }
            }
        }
    }
}
