using System;
using System.Linq;
using ConsoleUI.Menu;

using BM  = BLL.Menu;
using SF = ConsoleUI.Menu.Shared.SharedUI;


namespace ConsoleUI.Calculator.Extensions
{
    
    /// <summary>
    /// Describes Calculator Menu Console View Solution.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CalculatorView : SelectionMenuUI
    {
        /// <summary>
        /// Display Selection Menu in Console Application wit Calculation Data..
        /// </summary>
        /// <param name="menu">Reference to the Menu for Displaying.</param>
        /// <param name="value">Calculation Value to be Displayed.</param>
        /// <param name="topPos">Position on X Coordinate to Display Menu.</param>
        public static void DisplayMenu(BM.Menu menu, double value, int topPos = 4)
        {
            Console.Clear();

            var titleLenght = $"{menu}".Length + 10;
            SF.MenuHeader($"{menu}", topPos, titleLenght);
            
            var menuItems = menu.CopyOfMenuItems();

            SF.DrawCenteredText(string.Concat(Enumerable.Repeat("-", titleLenght)), Console.CursorTop);
            SF.DrawCenteredText("Current value => " + value, Console.CursorTop);
            SF.DrawCenteredText(string.Concat(Enumerable.Repeat("-", titleLenght)), Console.CursorTop);
            Console.CursorTop += 2;

            
            foreach (var item in menuItems)
            {
                var isSelected = menu.XCursorPosition == menuItems.IndexOf(item);
                var textToDraw = isSelected ? $"~~ {item} ~~" : $"{item}";

                SF.ColorDecider(isSelected, ConsoleColor.Cyan, Console.ForegroundColor);
                SF.DrawCenteredText(textToDraw, Console.CursorTop);
                Console.ResetColor();
                
            }
            
            SF.MenuFooter($"{menu}".Length, Console.CursorTop + 2);
        }
    }
}