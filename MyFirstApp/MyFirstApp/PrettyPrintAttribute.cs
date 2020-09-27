using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrettyPrintAttribute : Attribute
    {
        public bool Capitalize { get; set; } = false;
    }
}
