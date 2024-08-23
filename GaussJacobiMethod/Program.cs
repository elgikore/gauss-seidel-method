using System;

namespace GaussSeidelMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Gauss-Seidel Iteration Method");
            Console.WriteLine("----------------------------------\n");

            double[,] augMatrix = new double[3, 4];



            //for x1
            for (int row = 0; row < augMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < augMatrix.GetLength(1); col++)
                {
                    if (col == 3)
                    {
                        Console.Write($"Input b{row + 1}: ");
                        augMatrix[row, col] = Convert.ToDouble(Console.ReadLine());
                    }
                    else
                    {
                        Console.Write($"Input a{row + 1}{col + 1}: ");
                        augMatrix[row, col] = Convert.ToDouble(Console.ReadLine());
                    }
                    
                }

                Console.WriteLine();
                Console.Clear();
            }

            //first iteration value
            double x1old = 0;
            double x2old = 0; 
            double x3old = 0;
            int iteration = 1;

            //check if valid
            
            if ((Math.Abs(augMatrix[0, 0]) > (Math.Abs(augMatrix[0, 1]) + Math.Abs(augMatrix[0, 2]))) 
                && (Math.Abs(augMatrix[1, 1]) > (Math.Abs(augMatrix[1, 0]) + Math.Abs(augMatrix[1, 2]))) 
                && (Math.Abs(augMatrix[2, 2]) > (Math.Abs(augMatrix[2, 0]) + Math.Abs(augMatrix[2, 1]))))
            {
                Console.WriteLine("\nValid for Gauss-Seidel Method!\n\n");
                Console.WriteLine("Iteration Open! Start!\n\n");
            }
            else throw new Exception("Invalid for Gauss-Seidel Method!");

            //Iteration Open! Start!
            while (true)
            {
                Console.WriteLine($"Iteration no. {iteration}");
                double x1new = Math.Round((1 / augMatrix[0, 0]) * (augMatrix[0, 3] - (augMatrix[0, 1] * x2old) - (augMatrix[0, 2] * x3old)), 3, MidpointRounding.AwayFromZero);
                double x2new = Math.Round((1 / augMatrix[1, 1]) * (augMatrix[1, 3] - (augMatrix[1, 0] * x1new) - (augMatrix[1, 2] * x3old)), 3, MidpointRounding.AwayFromZero);
                double x3new = Math.Round((1 / augMatrix[2, 2]) * (augMatrix[2, 3] - (augMatrix[2, 0] * x1new) - (augMatrix[2, 1] * x2new)), 3, MidpointRounding.AwayFromZero);

                Console.WriteLine($"x1 = {x1new}, x2 = {x2new}, x3 = {x3new}\n\n");

                //if last iteration and latest iteration is the same, stop loop
                if ((x1old == x1new) && (x2old == x2new) && (x3old == x3new)) break;
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
