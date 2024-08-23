using System;

namespace GaussSeidelMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gauss-Seidel Iteration Method");
            Console.WriteLine("----------------------------------\n");

            //for x1
            Console.Write("Input a11: ");
            double a11 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input a12: ");
            double a12 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input a13: ");
            double a13 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input b1: ");
            double b1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" ");


            //for x2
            Console.Write("Input a21: ");
            double a21 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input a22: ");
            double a22 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input a23: ");
            double a23 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input b2: ");
            double b2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" ");

            //for x3
            Console.Write("Input a31: ");
            double a31 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input a32: ");
            double a32 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input a33: ");
            double a33 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input b3: ");
            double b3 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" ");

            //first iteration value
            double x1old = 0;
            double x2old = 0; 
            double x3old = 0;
            int iteration = 1;

            //check if valid
            if ((Math.Abs(a11) > (Math.Abs(a12) + Math.Abs(a13))) && (Math.Abs(a22) > (Math.Abs(a21) + Math.Abs(a23))) && (Math.Abs(a33) > (Math.Abs(a31) + Math.Abs(a32))))
            {
                Console.WriteLine("\nValid for Gauss-Seidel Method!\n\n");
                Console.WriteLine("Iteration Open! Start!\n\n");
            }
            else
                throw new Exception("Invalid for Gauss-Seidel Method!");

            //Iteration Open! Start!
            while (true)
            {
                Console.WriteLine($"Iteration no. {iteration}");
                double x1new = Math.Round((1 / a11) * (b1 - (a12 * x2old) - (a13 * x3old)), 3, MidpointRounding.AwayFromZero);
                double x2new = Math.Round((1 / a22) * (b2 - (a21 * x1new) - (a23 * x3old)), 3, MidpointRounding.AwayFromZero);
                double x3new = Math.Round((1 / a33) * (b3 - (a31 * x1new) - (a32 * x2new)), 3, MidpointRounding.AwayFromZero);

                Console.WriteLine($"x1 = {x1new}, x2 = {x2new}, x3 = {x3new}\n\n");

                //if last iteration and latest iteration is the same, stop loop
                if ((x1old == x1new) && (x2old == x2new) && (x3old == x3new))
                    break;
                //else assign new values to old values
                else
                {
                    x1old = x1new;
                    x2old = x2new;
                    x3old = x3new;
                }

                iteration++;
            }

            Console.WriteLine($"\nThe iteration stopped at {iteration}.");
            Console.ReadLine();
        }
    }
}
