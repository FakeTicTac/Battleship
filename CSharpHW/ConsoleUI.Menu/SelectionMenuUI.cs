using System;

using BM  = BLL.Menu;
using SF = ConsoleUI.Menu.Shared.SharedUI;


namespace ConsoleUI.Menu
{
    
    /// <summary>
    /// Describes Selection Menu Console UI Solution.
    /// </summary>
    public class SelectionMenuUI
    {

        /// <summary>
        /// Display Selection Menu in Console Application.
        /// </summary>
        /// <param name="menu">Reference to the Menu for Displaying.</param>
        /// <param name="topPos">Position on X Coordinate to Display Menu.</param>
        public static void DisplayMenu(BM.Menu menu, int topPos = 4)
        {
            Console.Clear();
            
            SF.MenuHeader($"{menu}", topPos, $"{menu}".Length + 10);
            
            var menuItems = menu.CopyOfMenuItems();

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