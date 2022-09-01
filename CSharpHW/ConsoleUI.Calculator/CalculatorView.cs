using System;
using System.Threading;


namespace ConsoleUI.Calculator
{
    
    /// <summary>
    /// Describes Calculator UI.
    /// </summary>
    public static class CalculatorView
    {
        
        /// <summary>
        /// Write text in the middle of Console.
        /// </summary>
        /// <param name="text">Text to be written.</param>
        /// <param name="topPosition">X coordinate position for cursor.</param>
        private static void ConsoleCursorWrite(string text, int topPosition)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, topPosition);
            Console.Write(text);
        }
        
        
        /// <summary>
        /// Displays the calculation process and parses User Input.
        /// </summary>
        /// <param name="sign">String of calculation sign.</param>
        /// <param name="value">Current Calculation Value.</param>
        /// <returns>Converted User Input.</returns>
        public static double CalculationDisplayProcess(string sign, double value)
        {
            ConsoleCursorWrite(
                sign == "new" ? "Current value => " : $"Current value => {value} {sign} ", 9);
            
            // User input parsing and validation.
            if (double.TryParse(Console.ReadLine()?.Trim(), out var number)) return number;
     
            ErrorMessage("Input should be a number.");
            return 0;
        }

        
        /// <summary>
        /// Prints Error message on screen if calculation isn't possible.
        /// </summary>
        /// <param name="errorMessage">Message to be displayed on screen.</param>
        public static void ErrorMessage(string errorMessage)
        {
            Console.Clear();
            ConsoleCursorWrite(errorMessage, (Console.WindowHeight - 1) / 2);

            Thread.Sleep(2000);
        }
    }
}