using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsusingC_
{
    public class Vehicle
    {
        //public int Length { get; set; }
        //public int Width { get; set; }

        public virtual int Length { get; set; }
        public virtual int Width { get; set; }

    }

    public class Car : Vehicle
    {
       //public new int Width
       // {
       //     set { base.Width = base.Length = value; }
       // }
       // public new int Length
       // {
       //     set { base.Width = base.Length = value; }
       // }

        public override int Width
        {
            set { base.Width = base.Length = value; }
        }
        public override int Length
        {
            set { base.Width = base.Length = value; }
        }
    }

    internal class LiskovSubstitutionPrinciple
    {
    
       
        static public int Area(Vehicle v) => v.Width * v.Length;
        
        public void Test()
        {
            Vehicle v = new Vehicle();
            v.Length = 5;
            v.Width = 6;

            Car c = new Car();
            c.Length = 4;
            // Above code will work fine

            // Below code will cause issue
            Vehicle v1 = new Car();
            v1.Width = 5;

            Console.WriteLine("Area of Vehicle is "+ Area(v));
            Console.WriteLine("Area of Car is " + Area(c));

            // Area of vehicle will be zero
            Console.WriteLine("Area of Vehicle 1 is " + Area(v1));

            // In order to solve this issue, use virtual keyword in base class and override in child class
            // By making above changes code will work fine


        }
    }
}
