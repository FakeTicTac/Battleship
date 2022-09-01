using System;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship
{
    
    /// <summary>
    /// Describes Shared Functionality for Data Animation.
    /// </summary>
    public static class AnimationUtils
    {
        
        /// <summary>
        /// Screen Erasing Animation in Console.
        /// </summary>
        /// <param name="xPos">X Coordinate Position for Erasing Start.</param>
        /// <param name="yPos">Y Coordinate Position for Erasing Start.</param>
        /// <param name="xSize">X Erasing Size.</param>
        /// <param name="ySize">Y Erasing Size.</param>
        public static void ErasingAnimation(int xPos, int yPos, int xSize, int ySize)
        {
            var random = new Random();
            
            for (var x = 0; x < 1000; x++)
            {
                Console.SetCursorPosition(xPos + random.Next(xSize), yPos + random.Next(ySize));
                Console.Write(" "); 
            }
        }
    }
}