using System;

namespace GaussSeidelMethod
{
    class Program
    {
        static void TitleScreen(string title)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine(title);
            Console.WriteLine("----------------------------------\n");
        }

        static void Main()
        {
            TitleScreen("Gauss-Seidel Iteration Method");

            double[,] augMatrix = new double[3, 4];

            //Assignment
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
            double[] oldValues = new double[3];
            double[] newValues = new double[3];

            int iteration = 1;

            //check if valid

            if ((Math.Abs(augMatrix[0, 0]) > (Math.Abs(augMatrix[0, 1]) + Math.Abs(augMatrix[0, 2])))
                && (Math.Abs(augMatrix[1, 1]) > (Math.Abs(augMatrix[1, 0]) + Math.Abs(augMatrix[1, 2])))
                && (Math.Abs(augMatrix[2, 2]) > (Math.Abs(augMatrix[2, 0]) + Math.Abs(augMatrix[2, 1]))))
            {
                Console.WriteLine("\nValid for Gauss-Seidel Method!\n\n");
                Console.WriteLine("Iteration Open! Start!\n\n");
            }
            else throw new Exception();

            //Iteration Open! Start!
            while (true)
            {
                Console.WriteLine($"Iteration no. {iteration}");
                newValues[0] = Math.Round((1 / augMatrix[0, 0]) * (augMatrix[0, 3] - (augMatrix[0, 1] * oldValues[1]) - (augMatrix[0, 2] * oldValues[2])), 3, MidpointRounding.AwayFromZero);
                newValues[1] = Math.Round((1 / augMatrix[1, 1]) * (augMatrix[1, 3] - (augMatrix[1, 0] * newValues[0]) - (augMatrix[1, 2] * oldValues[2])), 3, MidpointRounding.AwayFromZero);
                newValues[2] = Math.Round((1 / augMatrix[2, 2]) * (augMatrix[2, 3] - (augMatrix[2, 0] * newValues[0]) - (augMatrix[2, 1] * newValues[1])), 3, MidpointRounding.AwayFromZero);

                Console.WriteLine($"x1 = {newValues[0]}, x2 = {newValues[1]}, x3 = {newValues[2]}\n\n");

                //if last iteration and latest iteration is the same, stop loop
                if ((oldValues[0] == newValues[0]) && (oldValues[1] == newValues[1]) && (oldValues[2] == newValues[2])) break;
                //else assign new values to old values
                else
                {
                    oldValues[0] = newValues[0];
                    oldValues[1] = newValues[1];
                    oldValues[2] = newValues[2];
                }

                iteration++;
            }

            Console.WriteLine($"\nThe iteration stopped at {iteration}.");
            Console.ReadLine();
        }
    }
}
