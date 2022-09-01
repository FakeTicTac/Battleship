using System;
using System.Linq;

using BM  = BLL.Menu;


namespace ConsoleUI.Menu.Shared
{
    
    /// <summary>
    /// Describes Shared Functionality for Menu UI Solution.
    /// </summary>
    public static class SharedUI
    {
        
        /// <summary>
        /// Draw Header of the Menu.
        /// </summary>
        /// <param name="menuTitle">Title of the Menu for Drawing.</param>
        /// <param name="topPos">Position on X Coordinate for text.</param>
        /// <param name="Lenght">Lenght of the Footer of the Menu.</param>
        public static void MenuHeader(string menuTitle, int topPos, int Lenght)
        {
            MenuFooter(Lenght, topPos);
            DrawCenteredText($">{menuTitle}<", topPos);
            Console.CursorTop += 2;
        }


        /// <summary>
        /// Draw the footer of the Menu.
        /// </summary>
        /// <param name="Lenght">Lenght of the Footer of the Menu.</param>
        /// <param name="xPos">Position on X Coordinate for text.</param>
        public static void MenuFooter(int Lenght, int xPos) =>
            DrawCenteredText(string.Concat(Enumerable.Repeat("=", Lenght)), xPos);
        
        
        /// <summary>
        /// Draw text in the Middle of the Console.
        /// </summary>
        /// <param name="textToDraw">Text to be Drawn in Console.</param>
        /// <param name="xPos">Position on X Coordinate for text.</param>
        public static void DrawCenteredText(string textToDraw, int xPos)
        {
            Console.SetCursorPosition((Console.WindowWidth - textToDraw.Length) / 2 , xPos);
            Console.WriteLine(textToDraw);
        }


        /// <summary>
        /// Draw text in the Concrete Position of the Console.
        /// </summary>
        /// <param name="textToDraw">Text to be Drawn in Console.</param>
        /// <param name="yPos">Position on Y Coordinate for text.</param>
        /// <param name="xPos">Position on X Coordinate for text.</param>
        public static void SetCursorAndDrawText(string textToDraw, int? yPos = null, int? xPos = null)
        {
            Console.SetCursorPosition(yPos ?? Console.CursorLeft, xPos ?? Console.CursorTop);
            Console.Write(textToDraw);
        }

        
        /// <summary>
        /// Choose the color for Menu Item.
        /// </summary>
        /// <param name="isSelected">Indicator of Item Selection.</param>
        /// 
        /// <param name="colorTrue">Color to change if is Selected.</param>
        /// <param name="colorFalse">Color to change if not Selected.</param>
        public static void ColorDecider(bool isSelected, ConsoleColor colorTrue, ConsoleColor colorFalse) 
                                => Console.ForegroundColor = isSelected ? colorTrue : colorFalse;
    }
}