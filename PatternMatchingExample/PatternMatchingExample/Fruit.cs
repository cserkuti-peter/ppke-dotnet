using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PatternMatchingExample
{
    public class Fruit
    {
        public Color Color { get; set; }
        public bool IsRipe { get; set; }
    }

    public enum AppleType
    {
        Golden,
        PinkLady,
        Gala
    }

    public class Apple : Fruit
    {
        public AppleType AppleType { get; set; }
        public void MakeApplePieFrom() { Console.WriteLine("Making applepie from the apple."); }
        public void ThrowAway() { Console.WriteLine("Throwing away the apple."); }
    }

    public class Orange : Fruit 
    {
        public void Peel() { Console.WriteLine("Peeling the orange."); }
    }
}
