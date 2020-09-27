using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MyFirstApp
{
    [DebuggerDisplay("Id={Id}, FullName={FullName}")]
    [PrettyPrintable]
    public class Person
    {
        private static int nextId = 1;
        public Person()
        {
            Id = nextId++;  //  This is initialization. Doesnt matter if we dont have even a private setter.
        }

        public event EventHandler FirstNameChanged;

        [PrettyPrint]
        public int Id { get; }

        public override string ToString()
        {
            //  The default implementation would return the full type name.
            return this.FullName;
        }

        private string firstName;   //  Backing field of the property

        [PrettyPrint(Capitalize = true)]
        public string FirstName
        {
            get => firstName;       //  Expression bodied property
            //set => this.firstName = !String.IsNullOrEmpty(value)
            //            ? value 
            //            : throw new ArgumentNullException("FirstName");
            set
            {
                this.firstName = !String.IsNullOrEmpty(value)
                        ? value
                        : throw new ArgumentNullException("FirstName");

                //  Fire the event. This will call all registered event handlers.
                FirstNameChanged?.Invoke(this, new EventArgs());
            }
        }

        [PrettyPrint(Capitalize = false)]
        public string SecondName { get; set; } = "N/A";     //  Auto implemented property with default value

        public string FullName  //  Property for calculated value without backing field
        {
            get => $"{FirstName} {SecondName}";
        }
    }
}
