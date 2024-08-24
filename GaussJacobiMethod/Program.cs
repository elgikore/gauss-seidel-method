using System;
using System.Text;

namespace GaussSeidelMethod
{ 

    class Program
    {
        //interface-related methods
        private static void TitleScreen(string title)
        {
            const int charLength = 80;
            string repeatedChar = new string('=', charLength);

            Console.WriteLine(repeatedChar);
            Console.WriteLine($"{title, charLength}");
            Console.WriteLine($"{repeatedChar}\n");
        }

        private static void Attention(string title)
        {
            const int charLength = 80;
            string repeatedChar = new string('*', charLength);

            int spaces = charLength - title.Length;
            int padLeft = spaces / 2 + title.Length;

            Console.WriteLine(repeatedChar);
            Console.WriteLine(title.PadLeft(padLeft).PadRight(charLength));
            Console.WriteLine($"{repeatedChar}\n");
        }

        private static void clearContinue()
        {
            Console.Write("Press ENTER to continue. ");
            Console.ReadKey();
            Console.Clear();
        }



        //iteration methods
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

        //NOTE: Uses strictly diagonal dominance when checking
        private static bool isDiagonallyDominant(double[,] array)
        {
            double nonDiagSum = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1) - 1; j++)
                {
                    if (i != j) nonDiagSum += Math.Abs(array[i, j]);
                }

                if (!(Math.Abs(array[i, i]) > nonDiagSum)) return false;
                nonDiagSum = 0; //reset
            }

            return true;
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

                if (row != (augMatrix.GetLength(0) - 1)) Console.WriteLine("******************");
            }

            //first iteration value
            double[] oldValues = new double[3];
            double[] newValues = new double[3];
            int iteration = 1;

            Console.WriteLine();


            //check if valid (diagonally dominant) 
            if (isDiagonallyDominant(augMatrix))
            {
                Attention("Valid for Gauss-Seidel Method!");
                clearContinue();
            }
            else
            {
                Console.Clear();
                TitleScreen("Gauss-Seidel Iteration Method");
                Console.WriteLine("This system of equations can't be solved!");
                Console.Write("Want to input a different matrix? Press ENTER/any key to continue, or press Q/q to quit. ");

                switch ((char) Console.ReadKey().Key)
                {
                    case 'Q':
                        Console.WriteLine("\n\nSee you later!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Main();
                        break;
                }
            }
                

            


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
