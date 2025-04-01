using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsusingC_
{
    // This principle states that we should keep single interface for a single type of responsibility it means we should not add multiple methods in a single interface that would implement different types of logic
    // Example we shall keep different interfaces for Print, Email or Fax instead of putting these methods in a single interface because some customer does not have a printer that supports all type of functionality
    // Another example would be Caluclator, use separate interfaces to describe logic for methods like addition, subtraction, area because it is not necessary that every person need all these methods.

    public interface IAddition<T>
    {
        int Add(T t);
    }
    public interface ISubtraction<T>
    {
        int Subtract(T t);
    }

    partial interface BasicCalculation<T>
    {
        int Add(T t);
        int Subtract(T t);
    }

    public class Numbers 
    {
        public int number1 { get; set; }

        public int number2 { get; set; }

       
    }
    public class Calculation : IAddition<Numbers>, ISubtraction<Numbers>
    {
        public int Add(Numbers t)
        {
            return t.number1 + t.number2;
        }

        public int Subtract(Numbers t)
        {
            return t.number1 - t.number2;
        }
    }

    internal class InterfaceSegregationPrinciple
    {
        // Problem with BasicCalculation is that if person does not Addition or Subtraction method he still needs to provide its implementation
        public void Test()
        {
            Calculation c = new Calculation();
            Numbers num = new Numbers();
            num.number1 = 5;
            num.number2 = 10;

            Console.WriteLine($"Sum of {num.number1} + {num.number2} is "+c.Add(num));


        }
    }
}
