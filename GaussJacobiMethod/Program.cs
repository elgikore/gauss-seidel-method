using System;
using System.Text;

namespace GaussSeidelMethod
{
    class Program
    {

        private static void TitleScreen(string title)
        {
            const int charLength = 80;
            string repeatedChar = new string('=', charLength);

            Console.WriteLine(repeatedChar);
            Console.WriteLine($"{title,charLength}");
            Console.WriteLine($"{repeatedChar}\n");
        }

        private static void DisplayAugArray()
        {
            string[,] indexNumRef = { {"a11", "a12", "a13", "b1"},
                                      {"a21", "a22", "a23", "b2"},
                                      {"a31", "a32", "a33", "b3"}
            };
            
            int colLength = indexNumRef.GetLength(1);
            int rowLength = indexNumRef.GetLength(0);

            StringBuilder sb = new StringBuilder();
            

            for (int row = 0; row < rowLength; row++)
            {
                for (int col = 0; col < colLength; col++)
                {
                    string index = indexNumRef[row, col];

                    if (col == 0) sb.Append($"[{index}, ");
                    else if (col == (colLength - 1)) sb.Append($"| {index}]\n");
                    else if (col == (colLength - 2)) sb.Append($"{index} ");
                    else sb.Append($"{index}, ");
                }
            }

            Console.WriteLine(sb.ToString());
        }

        private static void clearContinue()
        {
            Console.Write("Press ENTER to continue");
            Console.Clear();
        }

        //private static double roundNPlaces(double number, int numPlaces)
        //{
        //    return Math.Round(number, numPlaces, MidpointRounding.AwayFromZero);
        //}

        static void Main()
        {
            double[,] augMatrix = new double[3, 4];


            //Assignment
            TitleScreen("Gauss-Seidel Iteration Method");
            DisplayAugArray();
            Console.WriteLine("Calculates x1, x2, and x3 using the iterative method, rather than the\nelimination method.\n");
            Console.WriteLine("IMPORTANT: Must be a diagonally dominant matrix in order to converge to the\ntrue value.\n");
            Console.WriteLine("NOTE: Accurate to 3 decimal places.\n");

            for (int row = 0; row < augMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < augMatrix.GetLength(1); col++)
                {
                    if (col == (augMatrix.GetLength(1) - 1))
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

                Console.WriteLine("******************");
            }

            //first iteration value
            double[] oldValues = new double[3];
            double[] newValues = new double[3];

            int iteration = 1;

            //check if valid (diagonally dominant) 
            if ((Math.Abs(augMatrix[0, 0]) > (Math.Abs(augMatrix[0, 1]) + Math.Abs(augMatrix[0, 2])))
                && (Math.Abs(augMatrix[1, 1]) > (Math.Abs(augMatrix[1, 0]) + Math.Abs(augMatrix[1, 2])))
                && (Math.Abs(augMatrix[2, 2]) > (Math.Abs(augMatrix[2, 0]) + Math.Abs(augMatrix[2, 1]))))
            {
                Console.WriteLine("\nValid for Gauss-Seidel Method!\n\n");
                clearContinue();
            }
            else throw new Exception();

            


            //Iteration Open! Start!
            TitleScreen("Iteration Open! Start!");
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
