using System;
using System.Linq;
using BLL.Battleship;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Items
{
    
    /// <summary>
    /// Describes BattleShip Game Board Item UI Solution.
    /// </summary>
    public class GameBoardItemUI
    {

        /// <summary>
        /// Max Size of the Board on X Coordinate.
        /// </summary>
        private int BoardXLimit { get; }
        
        
        /// <summary>
        /// Max Size of the Board on Y Coordinate.
        /// </summary>
        private int BoardYLimit { get; }


        /// <summary>
        /// Describes X Coordinate Cursor Position For Item to be Placed.
        /// </summary>
        private int ItemXPosition { get; }
            
            
        /// <summary>
        /// Describes Y Coordinate Cursor Position For Item to be Placed.
        /// </summary>
        private int ItemYPosition { get; }
        
        
        /// <summary>
        /// Game Board to be Drawn in Console.
        /// </summary>
        private BoardSquareState[,] GameBoard { get; }


        /// <summary>
        /// Basic Game Board Item Constructor. Defines Board Position on the Screen and Board State.
        /// </summary>
        /// <param name="xCoordinate">X Coordinate for Game Board Item to be Drawn. (Starting Point is Center of Console)</param>
        /// <param name="yCoordinate">Y Coordinate for Game Board Item to be Drawn.</param>
        /// <param name="gameBoard">Game Board to be Drawn in Console.</param>
        public GameBoardItemUI(int xCoordinate, int yCoordinate, BoardSquareState[,] gameBoard)
        {
            GameBoard = gameBoard;
            BoardXLimit = GameBoard.GetLength(0);
            BoardYLimit = GameBoard.GetLength(1);

            
            ItemYPosition = yCoordinate;
            ItemXPosition = xCoordinate > 0 ? Console.WindowWidth / 2 + xCoordinate : 
                                Console.WindowWidth / 2 - (BoardYLimit.ToString().Length + 3 + BoardXLimit * 2 + Math.Abs(xCoordinate));
        }
        
        
        /// <summary>
        /// Draw Game Board Item in the Console.
        /// </summary>
        /// <param name="selectedCell">Cell which is Selected on Game Board.</param>
        /// <param name="selectSize">Sizes of the Selection on X and Y Coordinate.</param>
        public void DrawBoard((int, int) selectedCell, (int, int) selectSize)
        {
            var (xSize, ySize) = selectSize;
            var (xPosition, yPosition) = selectedCell;

            for (var y = 0; y <= BoardYLimit; y++)
            {
                DrawBoardLine(xPosition, xSize, y, yPosition < y && y <= yPosition + ySize);
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Draw the Header with Letters and Line with Numbers of the Game Board.
        /// </summary>
        /// <param name="xSelectedCell">Cell which is Selected on X Coordinate on the Game Board.</param>
        /// <param name="xSelectSize">Sizes of the Selection on X Coordinate.</param>
        /// <param name="yPosition">Current Position on Y Coordinate of Game Board.</param>
        /// <param name="isYSelected">Indicator of Y Coordinate Selection.</param>
        private void DrawBoardLine(int xSelectedCell, int xSelectSize, int yPosition, bool isYSelected = false)
        {
            var yStringLenght = $"{BoardYLimit}".Length;
            
            var boardLine = yPosition == 0
                ? SF.StringRepeater(" ", yStringLenght + 3)
                : $"{SF.StringRepeater(" ", yStringLenght - $"{yPosition}".Length + 1)}{yPosition} |";
            
            SF.SetCursorAndDrawText(boardLine, ItemXPosition, ItemYPosition + yPosition);
            
            for (var x = 0; x < BoardXLimit; x++)
            {
                if (yPosition != 0 && (!Enumerable.Range(xSelectedCell, xSelectSize).Contains(x) || !isYSelected))
                {
                    ColorfulBoardState(GameBoard[x, yPosition - 1]);
                    Console.Write("|");
                    continue;
                }

                boardLine = yPosition == 0 ? $"{Convert.ToChar(0x0041 + x).ToString()} " : "o|";

                if (boardLine == "o|") ColorfulCursor((GameBoard[x, yPosition - 1]));

                Console.Write(boardLine);
                Console.ResetColor();
            }
        }


        /// <summary>
        /// Colorize Cursor Based on State.
        /// </summary>
        /// <param name="boardSquareState">State of the Current Board Square</param>
        private static void ColorfulCursor(BoardSquareState boardSquareState)
        {
            Console.ForegroundColor = (boardSquareState.IsShip, boardSquareState.IsReserved(), boardSquareState.IsBomb) switch
                {
                    (true, true, false) => ConsoleColor.Red,
                    (false, true, false) => ConsoleColor.Red,
                    _ => Console.ForegroundColor
                };
        }
        
        
        /// <summary>
        /// Colorize Board Square Based on State.
        /// </summary>
        /// <param name="boardSquareState">State of the Current Board Square.</param>
        private static void ColorfulBoardState(BoardSquareState boardSquareState)
        {
            Console.ForegroundColor = (boardSquareState.IsShip, boardSquareState.IsBomb) switch
            {
                (false, true) => ConsoleColor.Blue,
                (true, false) => ConsoleColor.Green,
                (true, true) => ConsoleColor.Red,
                _ => Console.ForegroundColor
            };
            
            Console.Write($"{boardSquareState}");
            
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}