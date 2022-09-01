using System;
using BLL.Battleship;
using ConsoleUI.Battleship.Enums;
using ConsoleUI.Battleship.Items;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Views
{
    
    /// <summary>
    /// Describes BattleShip Game Ship Placing View Solution.
    /// </summary>
    public class ShipPlacementView
    {
       
        /// <summary>
        /// Max Size of the Board on Y Coordinate.
        /// </summary>
        private int YBoardLimit { get; }
        
        
        /// <summary>
        /// Max Size of the Board on X Coordinate.
        /// </summary>
        private int XBoardLimit { get; }
        
        
        /// <summary>
        /// Name of the Making Move Player.
        /// </summary>
        private string PlayerName { get; }
        
        
        /// <summary>
        /// Describes Name of the Current Ship.
        /// </summary>
        public string? ShipName { get; private set; }
        
        
        /// <summary>
        /// Describes current Ship X Coordinate and Y Coordinate Sizes.
        /// </summary>
        public (int, int) ShipSizes { get; private set; }
    
        
        /// <summary>
        /// Describes Current Position of Cursor on Game Board.
        /// </summary>
        public (int, int) CursorPosition { get; private set; }
        
        
        /// <summary>
        /// Indicates if Ship Placement Process is Completed.
        /// </summary>
        public bool isPlacementCompleted { get; set; }
        
        
        /// <summary>
        /// Indicator of View Action.
        /// </summary>
        public ViewAction ViewAction { get; private set; }
        
        
        /// <summary>
        /// Basic Constructor for Ship Placement.
        /// </summary>
        /// <param name="board">Battleship Board to be Displayed and Ships to be Located at.</param>
        /// <param name="playerName">Name of the Making Move Player.</param>
        public ShipPlacementView(BoardSquareState[,] board, string playerName)
        {
            ShipSizes = (1, 1);
            CursorPosition = (0, 0);
            
            PlayerName = playerName;
            isPlacementCompleted = false;
            
            YBoardLimit = board.GetLength(0);
            XBoardLimit = board.GetLength(1);
            ViewAction = ViewAction.Placement;
        }


        /// <summary>
        /// User Places Ship on his Own Board: Drawing ship sizes and it's location on the Board.
        /// </summary>
        /// <param name="board">Board where Ship should be Added to.</param>
        /// <param name="cellCount">Left Amount of Cells for Ship.</param>
        /// <param name="message">Message to be Displayed in Message Box.</param>
        /// <returns>Ship X and Y Coordinates, Ship Sizes and it's Name.</returns>
        public void RunView(BoardSquareState[,] board, int cellCount, string? message = null)
        {
            var gameBoard = new GameBoardItemUI(-8, 14, board);

            var round = 0;
            if (message == null) DropOldShipUI();
            
            do
            {
                ViewAction = ViewAction.Placement;
                
                Console.Clear();
                SF.GameHeader();

                Console.ForegroundColor = ConsoleColor.Cyan;
                SF.GridBasedText($"<=== Now, It's {PlayerName} Turn! ===>", 2, 1, Console.CursorTop + 1);
                Console.ResetColor();

                var leftCells = ShipName != null ? cellCount - ShipSizes.Item1 * ShipSizes.Item2 : cellCount;
                var leftCellMessage = leftCells < 0 ? $"You've Taken {Math.Abs(leftCells)} Cells More than You Can." 
                                                            : $"You Have {leftCells} Cells Available for Ships.";
                SF.GridBasedText($"{leftCellMessage}", 2, 1, Console.CursorTop + 1);

                var gridCursorTop = Console.CursorTop + 3;
                var boxMessage = ShipName == null ? "You Should Create a Ship. Look ↓ For Control Help." 
                    : $"The Name  '{ShipName}'  is Chosen! Now, Shape and Locate Your Ship on the Field.";
                
                gameBoard.DrawBoard(CursorPosition, ShipSizes);
                
                var messageBoxLenght = Console.CursorTop - gridCursorTop - 1;
                
                var messageBox = new MessageBoxItemUI(Console.WindowWidth / 2 + 8, gridCursorTop, 31, messageBoxLenght);
                messageBox.DisplayMessageBox(round == 0 && message != null ? message : boxMessage);
                
                var controlHelperMessage = ShipName == null
                    ? "Q : to Finish Ship Placement\n\n" +
                      "W : to Create New Ship\n\n" +
                      "E : To Generate Random Board\n\n" +
                      "Z : Delete Last Ship\n\n" +
                      "X : Delete All Ships\n\n" +
                      "C : To Main Menu\n\n"
                    : "← ↑ → ↓ : to Choose Position\n\n" +
                      "WASD : to Expand and Narrow Ship\n\n" +
                      "Enter : to Place Ship\n\n" +
                      "G : to Finish Ship Placement.";
                
                SF.GridBasedText(controlHelperMessage, 2, 1, Console.CursorTop + 4);
                SF.GameFooter(Console.CursorTop + 1, 30, "─");

                round++;
                
                if (ShipName == null)
                {
                    if (!IsNewShip()) return;
                    
                    boxMessage = "It's Time to Create Your Ship! Type its Name Here : ";
                    var (xPos, yPos) = messageBox.DisplayMessageBox(boxMessage);
                    
                    Console.SetCursorPosition(xPos, yPos);
                    Console.CursorVisible = true;
                    ShipName = Console.ReadLine();
                    Console.CursorVisible = false;
                    continue;
                }
                
                if (KeyboardControl()) return;
                
            } while (true);
        }
        
         
        /// <summary>
        /// Allow User to use Navigation System for Ship Placement.
        /// </summary>
        /// <returns>Indicator of Ship Placement.</returns>
        private bool KeyboardControl()
        {
            var (xPos, yPos) = CursorPosition;
            var (xSize, ySize) = ShipSizes;
            
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    yPos = yPos > 0 ? yPos - 1 : yPos;
                    break;
                case ConsoleKey.DownArrow:
                    yPos = yPos + ySize - 1 < YBoardLimit - 1 ? yPos + 1 : yPos;
                    break;
                case ConsoleKey.LeftArrow:
                    xPos = xPos > 0 ? xPos - 1 : xPos;
                    break;
                case ConsoleKey.RightArrow:
                    xPos = xPos + xSize - 1 < XBoardLimit - 1 ? xPos + 1 : xPos;
                    break;
                case ConsoleKey.W:
                    ySize = ySize > 1 ? ySize - 1 : ySize;
                    break;
                case ConsoleKey.S:
                    ySize = ySize < YBoardLimit - 1 - yPos + 1 ? ySize + 1 : ySize;
                    break;
                case ConsoleKey.A:
                    xSize = xSize > 1 ? xSize - 1 : xSize;
                    break;
                case ConsoleKey.D:
                    xSize = xSize < XBoardLimit - 1 - xPos + 1 ? xSize + 1 : xSize;
                    break;
                case ConsoleKey.Enter:
                    return true;
            }

            CursorPosition = (xPos, yPos);
            ShipSizes = (xSize, ySize);
            
            return false;
        }


        /// <summary>
        /// Decide Should We Place the New Ship.
        /// </summary>
        /// <returns>Indicator of new Ship Placement.</returns>
        private bool IsNewShip()
        {
            do
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        isPlacementCompleted = true;
                        return false;
                    case ConsoleKey.W:
                        return true;
                    case ConsoleKey.E:
                        ViewAction = ViewAction.Random;
                        return false;
                    case ConsoleKey.Z:
                        ViewAction = ViewAction.DeleteLast;
                        return false;
                    case ConsoleKey.X:
                        ViewAction = ViewAction.DeleteAll;
                        return false;
                    case ConsoleKey.C:
                        ViewAction = ViewAction.MenuRun;
                        return false;
                }
                
            } while (true);
        }
        
        
        /// <summary>
        /// Drop Old Ship Data for Placement the New one.
        /// </summary>
        private void DropOldShipUI()
        {
            ShipSizes = (1, 1);
            ShipName = null;
        }
    } 
}