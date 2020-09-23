using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstApp
{
    public class Person
    {
        private string firstName;   //  Backing field of the property

        public string FirstName
        {
            get => firstName;       //  Expression bodied property
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("FirstName");

                this.firstName = value;
            }
        }
        
        public string SecondName { get; set; } = "N/A";     //  Auto implemented property with default value

        public string FullName  //  Property for calculated value without backing field
        {
            get => $"{FirstName} {SecondName}";
        }
    }
}
