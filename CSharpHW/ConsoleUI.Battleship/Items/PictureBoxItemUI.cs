using System;
using System.Collections.Generic;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Items
{
    
    /// <summary>
    /// Class Represents The Picture Box to be Drawn in Console.
    /// </summary>
    public class PictureBoxItemUI
    {
        
        /// <summary>
        /// Lenght of the Picture Box on X Coordinate.
        /// </summary>
        private int YLenght { get; }
        
        
        /// <summary>
        /// Lenght of the Picture Box on Y Coordinate.
        /// </summary>
        private int XLenght { get; }


        /// <summary>
        /// Describes X Coordinate Cursor Position For Item to be Placed.
        /// </summary>
        private int ItemXPosition { get; }
            
            
        /// <summary>
        /// Describes Y Coordinate Cursor Position For Item to be Placed.
        /// </summary>
        private int ItemYPosition { get; }
        
        
        /// <summary>
        /// Describes Title of the Picture.
        /// </summary>
        private string Title { get; }
        
        
        /// <summary>
        /// Basic Constructor For Picture Box Item. Describes Sizes, Position and Title.
        /// </summary>
        /// <param name="xPos">X Coordinate Cursor Position For Item to be Placed</param>
        /// <param name="yPos">Y Coordinate Cursor Position For Item to be Placed</param>
        /// <param name="xLenght">Lenght of the Picture Box on X Coordinate.</param>
        /// <param name="yLenght">Lenght of the Picture Box on Y Coordinate.</param>
        /// <param name="title">Title of the Picture Box.</param>
        public PictureBoxItemUI(int xPos, int yPos, int xLenght, int yLenght, string title)
        {
            ItemXPosition = xPos;
            ItemYPosition = yPos;
            
            YLenght = yLenght;
            XLenght = xLenght;

            Title = title;
        }


        /// <summary>
        /// Draws an Image Box with Given Color Ratio.
        /// </summary>
        /// <param name="pictureColors">Colors to be Drawn In Picture Box.</param>
        /// <param name="isSideBorders">Indicates if the Image Should Contain Side Borders.</param>
        public void DrawPictureBox(List<ConsoleColor> pictureColors, bool isSideBorders = true)
        {
            for (var y = 0; y < YLenght; y++)
            {
                for (var x = 0; x < XLenght; x++)
                {
                    if (y == 0 || y == YLenght - 1 || isSideBorders && (x == 0 || x == XLenght - 1))
                    {
                        SF.SetCursorAndDrawText("─", ItemXPosition + x, ItemYPosition + y);
                        
                        if (y == 0) 
                            SF.SetCursorAndDrawText($" {Title} ", ItemXPosition + XLenght / 2 - $" {Title} ".Length / 2, ItemYPosition + y);
                        
                        continue;
                    }

                    if (x <= 1 || x >= XLenght - 2 || y == 1 || y == YLenght - 2) continue;
                    
                    Console.BackgroundColor = pictureColors[new Random().Next(pictureColors.Count)];
                    SF.SetCursorAndDrawText(" ", ItemXPosition + x, ItemYPosition + y);
                    Console.ResetColor();
                }
            }
        }
    }
}