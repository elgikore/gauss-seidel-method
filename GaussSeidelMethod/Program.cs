using ConsoleTables;
using System;
using System.Text;

namespace GaussSeidelMethod
{ 

    class Program
    {
        private const int CHAR_LENGTH = 80;

        //interface-related methods
        private static void TitleScreen(string title)
        {
            
            string repeatedChar = new string('=', CHAR_LENGTH);

            Console.WriteLine(repeatedChar);
            Console.WriteLine($"{title, CHAR_LENGTH}");
            Console.WriteLine($"{repeatedChar}\n");
        }

        private static void Attention(string title)
        {
            string repeatedChar = new string('*', CHAR_LENGTH);

            int spaces = CHAR_LENGTH - title.Length;
            int padLeft = spaces / 2 + title.Length;

            Console.WriteLine(repeatedChar);
            Console.WriteLine(title.PadLeft(padLeft).PadRight(CHAR_LENGTH));
            Console.WriteLine($"{repeatedChar}\n");
        }

        private static void ClearContinue()
        {
            Console.Write("Press ENTER to continue. ");
            Console.ReadKey();
            Console.Clear();
        }

        private static void RepeatAgainPrompt(string message)
        {
            Console.Write($"{message} Press ENTER/any key to continue, or press\nQ/q to quit. ");

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
        private static bool IsDiagonallyDominant(double[,] array)
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

        private static double RoundNPlaces(double number, byte numPlaces)
        {
            if (numPlaces == 255) return number; //if the user doesn't want to set any rounding

            return Math.Round(number, numPlaces, MidpointRounding.AwayFromZero);
        }



        static void Main()
        {
            double[,] augMatrix = new double[3, 4];
            byte rounding = 255;
            string title = "Gauss-Seidel Iteration Method";


            //title screen
            TitleScreen(title);
            DisplayAugArray();
            Console.WriteLine("Calculates x1, x2, and x3 using the iterative method, rather than the\nelimination method.\n");
            Console.WriteLine("IMPORTANT: Must be a diagonally dominant matrix in order to converge to the\ntrue value.\n");
            Console.WriteLine("You can also specify the number of decimal places, e.g. 0 for whole number, 1-15\nfor 1-15 decimal places, or 255 for no rounding.");

            Console.WriteLine(new string('-', CHAR_LENGTH));


            //sepecify decimal places
            try
            {
                Console.Write("\nSpecify the number of decimal places: ");
                rounding = Convert.ToByte(Console.ReadLine());

                if (rounding > 15 && rounding != 255) throw new ArgumentOutOfRangeException();
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"\n{fe.Message}");
                ClearContinue();
                Main();
            }
            catch (OverflowException oe)
            {
                Console.WriteLine($"\nOutside of the allowable range! {oe.Message}");
                ClearContinue();
                Main();
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                Console.WriteLine($"\nOutside of the allowable range! {aoore.Message}");
                ClearContinue();
                Main();
            }


            //assignment
            Console.Clear();
            TitleScreen(title);
            DisplayAugArray();
            try
            {
                for (int row = 0; row < augMatrix.GetLength(0); row++)
                {
                    Console.WriteLine($"\nRow {row + 1}");
                    for (int col = 0; col < augMatrix.GetLength(1); col++)
                    {
                        if (col == (augMatrix.GetLength(1) - 1))
                        {
                            Console.Write($"Input b{row + 1}:\t");
                            augMatrix[row, col] = Convert.ToDouble(Console.ReadLine());
                        }
                        else
                        {
                            Console.Write($"Input a{row + 1}{col + 1}:\t");
                            augMatrix[row, col] = Convert.ToDouble(Console.ReadLine());
                        }
                    }
                }
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"\n{fe.Message}");
                ClearContinue();
                Main();
            }

            //first iteration value
            double[] oldValues = new double[3];
            double[] newValues = new double[3];
            int iteration = 1;

            Console.WriteLine();


            //check if valid (diagonally dominant) 
            if (IsDiagonallyDominant(augMatrix))
            {
                Attention("Valid for Gauss-Seidel Method!");
                ClearContinue();
            }
            else
            {
                Console.Clear();
                TitleScreen("Gauss-Seidel Iteration Method");
                Console.WriteLine("This system of equations can't be solved!");

                RepeatAgainPrompt("Want to input a different matrix?");
            }
                

            
            //Iteration Open! Start!
            TitleScreen("Iteration Open! Start!");

            ConsoleTable table = new ConsoleTable("Iteration No.", "x1", "x2", "x3");

            while (true)
            {
                
                newValues[0] = RoundNPlaces((1 / augMatrix[0, 0]) * (augMatrix[0, 3] - (augMatrix[0, 1] * oldValues[1]) - (augMatrix[0, 2] * oldValues[2])), rounding);
                newValues[1] = RoundNPlaces((1 / augMatrix[1, 1]) * (augMatrix[1, 3] - (augMatrix[1, 0] * newValues[0]) - (augMatrix[1, 2] * oldValues[2])), rounding);
                newValues[2] = RoundNPlaces((1 / augMatrix[2, 2]) * (augMatrix[2, 3] - (augMatrix[2, 0] * newValues[0]) - (augMatrix[2, 1] * newValues[1])), rounding);

                table.AddRow(iteration, newValues[0], newValues[1], newValues[2]);

                //if last iteration and latest iteration is the same, stop loop
                if ((oldValues[0] == newValues[0]) && (oldValues[1] == newValues[1]) && (oldValues[2] == newValues[2])) break;
                //else assign new values to old values
                else
                {
                    for (int i = 0; i < oldValues.Length; i++) oldValues[i] = newValues[i];
                }

                iteration++;
            }

            Console.WriteLine(table.ToMinimalString());

            Attention($"The iteration stopped at {iteration}.");
            
            RepeatAgainPrompt("Want to calculate a different matrix?");
        }
    }
}
