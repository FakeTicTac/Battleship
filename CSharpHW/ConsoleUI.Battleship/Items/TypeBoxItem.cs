using System;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Items
{
    
    /// <summary>
    /// Describes Type Box Item UI Solution.
    /// </summary>
    public static class TypeBoxItem
    {
        
        /// <summary>
        /// Display Box For Typing in The Given Y Coordinate of Console Window.
        /// </summary>
        /// <param name="yPos">Y Coordinate Position for Type Box Display.</param>
        /// <param name="lenght">X Coordinate Lenght of the Text Box.</param>
        /// <returns>Captured In The Text Box Input.</returns>
        public static string? DisplayBox(int yPos, int lenght)
        {
            Console.CursorVisible = true;
            
            SF.GridBasedText(SF.StringRepeater("=", lenght), 2, 1, yPos - 1);
            SF.GridBasedText(SF.StringRepeater("|", lenght), 2, 1, yPos);
            SF.GridBasedText(SF.StringRepeater(" ", lenght - 2), 2, 1, yPos);
            SF.GridBasedText(SF.StringRepeater("=", lenght), 2, 1, yPos + 1);
            
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, yPos + 1);
            
            Console.CursorVisible = false;
            return Console.ReadLine();
        }


        /// <summary>
        /// Display Box For Typing in The Given Y Coordinate of Console Window.
        /// </summary>
        /// <param name="xPos">X Coordinate Position for Type Box Display.</param>
        /// <param name="yPos">Y Coordinate Position for Type Box Display.</param>
        /// <param name="xLenght">X Coordinate Lenght of Box Display.</param>
        /// <param name="yLenght">Y Coordinate Lenght of Box Display.</param>
        public static void DisplayBoxNoType(int xPos, int yPos, int xLenght, int yLenght)
        {
            for (var y = 0; y < yLenght; y++)
                SF.SetCursorAndDrawText(SF.StringRepeater("=", xLenght), xPos, yPos + y);
        }
    }
}