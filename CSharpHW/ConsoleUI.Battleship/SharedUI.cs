using Figgle;
using System;
using System.Linq;


namespace ConsoleUI.Battleship
{
    
    /// <summary>
    /// Describes Shared Functionality for Battleship Game UI Solution.
    /// </summary>
    public static class SharedUI
    {
        
        /// <summary>
        /// Draw Header of the Battleship Game.
        /// </summary>
        public static void GameHeader()
        {
            var headerTitle = FiggleFonts.Small.Render("Battleship");
            var headerUnderline = StringRepeater("─", headerTitle.Split("\n")[0].Length + 6);

            GridBasedText(headerTitle, 2, 1, 0);
            GridBasedText(headerUnderline, 2, 1, Console.CursorTop);
        }


        /// <summary>
        /// Draw Footer of the Battleship Game.
        /// </summary>
        /// <param name="yPos">Position on Y Coordinate for the Footer.</param>
        /// <param name="size">Lenght of the Footer.</param>
        /// <param name="text">String Char to be Drawn as Footer Component.</param>
        public static void GameFooter(int yPos, int size, string text)
        {
            var footerUnderline = StringRepeater(text, size);
            
            GridBasedText(footerUnderline, 2, 1, yPos);
        }
        
        
        /// <summary>
        /// Draw Text on the Console Grid. Grid is Relative to Console Window Width.
        /// </summary>
        /// <param name="drawText">String to Draw on Console Grid.</param>
        /// <param name="xGridDelimiter">Console Width Delimiter for Layout Creation.</param>
        /// <param name="xPosition">Position on X Coordinate for text to be Drawn.</param>
        /// <param name="yPosition">Position on Y Coordinate for text to be Drawn.</param>
        /// <param name="reserveSpace">Space Reservation for Text.</param>
        public static void GridBasedText(string drawText, int xGridDelimiter, int xPosition, int yPosition, int reserveSpace = 0)
        {
            Console.CursorTop = yPosition;

            var consoleLayout = Console.WindowWidth / xGridDelimiter;
            var textLines = drawText.Split('\n');

            foreach (var line in textLines)
            {
                var len = line.Length;
                if (reserveSpace != 0) len = reserveSpace;
                
                Console.SetCursorPosition(consoleLayout * xPosition - len / 2, Console.CursorTop + 1);
                Console.Write(line);
            }
        }
        
        
        /// <summary>
        /// Draw text in the Concrete Position of the Console.
        /// </summary>
        /// <param name="textToDraw">Text to be Drawn in Console.</param>
        /// <param name="xPos">Position on X Coordinate for Text.</param>
        /// <param name="yPos">Position on Y Coordinate for Text.</param>
        public static void SetCursorAndDrawText(string textToDraw, int? xPos = null, int? yPos = null)
        {
            Console.SetCursorPosition(xPos ?? Console.CursorLeft, yPos ?? Console.CursorTop);
            Console.Write(textToDraw);
        }
        
        
        /// <summary>
        /// Clear the Given Line in Console.
        /// </summary>
        /// <param name="xPos">X Coordinate Position in Console to be Cleared. (Null if Current)</param>
        public static void ClearCurrentConsoleLine(int? xPos = null)
        {
            var currentLineCursor = xPos ?? Console.CursorTop;
            
            Console.SetCursorPosition(0, currentLineCursor);
            Console.Write(StringRepeater(" ", Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor);
        }
        
        
        /// <summary>
        /// Repeat the Provided Text for Certain Amount of Times.
        /// </summary>
        /// <param name="repeatText">Text to be Repeated.</param>
        /// <param name="repeatCount">Amount of Repetitions.</param>
        /// <returns>Repeated text for Certain Amount of Times.</returns>
        public static string StringRepeater(string repeatText, int repeatCount) => 
                                                            string.Concat(Enumerable.Repeat(repeatText, repeatCount));
        
    }
}