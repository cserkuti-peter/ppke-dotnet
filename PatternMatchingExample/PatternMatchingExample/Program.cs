using System;
using System.Drawing;

namespace PatternMatchingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Fruit fruit = new Apple { Color = Color.Red };

            //  1. This is what we want to check
            if (fruit.GetType() == typeof(Apple) && fruit.Color == Color.Green)
            {
                var apple = fruit as Apple;
            }

            if (fruit is Apple)
            {
                (fruit as Apple).MakeApplePieFrom();
            }

            //  C# 7
            //  Pattern matching based on type
            switch (fruit)
            {
                case Apple apple:
                    apple.MakeApplePieFrom();
                    break;
                default:
                    break;
            }

            //  Pattern matching with condition
            switch (fruit)
            {
                case Apple apple when apple.Color == Color.Green:
                    apple.MakeApplePieFrom();
                    break;
                case Apple apple when apple.Color == Color.Brown:
                    apple.ThrowAway();
                    break;
                case Orange orange:
                    orange.Peel();
                    break;
            }

            //  C# 8
            //  switch expression vs switch statement
            var whatFruit = fruit switch
            {
                Apple _ => "This is an apple",
                _ => "This is not an apple"
            };

            //  Property pattern
            var text = fruit switch
            {
                Apple { IsRipe: true} => "This is a ripe apple.",
                { IsRipe: true} => "This is a ripe fruit.",
                _ => "This is not a ripe fruit",
            };

            //   Tuple pattern
            State state = State.Open;
            Action action = Action.Close;
            var newState = (state, action) switch
            {
                (State.Open, Action.Close) => State.Closed,
                (State.Open, Action.Open) => throw new Exception("Cannot open."),
                (State.Closed, Action.Open) => State.Open,
                (State.Closed, Action.Close) => throw new Exception("Cannot close."),
                _ => state
            };

            //  And also: Positional pattern (deconstruction)
        }
        public enum State { Open, Closed }
        public enum Action { Open, Close }
    }
}
