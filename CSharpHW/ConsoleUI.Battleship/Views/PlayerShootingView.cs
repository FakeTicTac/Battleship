using System;
using BLL.Battleship;
using System.Threading;
using ConsoleUI.Battleship.Enums;
using ConsoleUI.Battleship.Items;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Views
{
    
    /// <summary>
    /// Describes BattleShip Game Shooting View Solution.
    /// </summary>
    public class PlayerShootingView
    {
        
        /// <summary>
        /// Indicator of View Action.
        /// </summary>
        public ViewAction ViewAction { get; private set; }
        
        
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
        /// Indicator of Own Board or Message box Display.
        /// </summary>
        private bool isMessageBox { get; set; }

        
        /// <summary>
        /// Indicates if Bomb Placement Process is Completed.
        /// </summary>
        private bool IsPlacementCompleted { get; set; }
        
        
        /// <summary>
        /// Describes Current Position of Cursor on Game Board.
        /// </summary>
        public (int, int) CursorPosition { get; private set; }
        
        
        /// <summary>
        /// Basic Constructor for Player Shooting.
        /// </summary>
        /// <param name="board">Battleship Board to be Process Shooting at.</param>
        /// <param name="playerName">Name of the Making Move Player.</param>
        public PlayerShootingView(BoardSquareState[,] board, string playerName)
        {
            CursorPosition = (0, 0);
            
            PlayerName = playerName;
            IsPlacementCompleted = false;
            
            isMessageBox = true;

            YBoardLimit = board.GetLength(0);
            XBoardLimit = board.GetLength(1);
        }
        
        
        /// <summary>
        /// Displays Shooting View in the Console.
        /// </summary>
        public void RunView(BoardSquareState[,] firingBoard, BoardSquareState[,] ownBoard, bool isControlled, string message)
        {
            ViewAction = ViewAction.Shot;
            IsPlacementCompleted = false;
            
            var firingBoardUI = new GameBoardItemUI(-8, 12, firingBoard);
            var ownBoardUI = new GameBoardItemUI(8, 12, ownBoard);
            var messageBox = new MessageBoxItemUI(Console.WindowWidth / 2 + 8, 12, 31, ownBoard.GetLength(1));
            
            do
            {
                Console.Clear();
                SF.GameHeader();
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                SF.GridBasedText($"<=== Now, It's {PlayerName} Turn! ===>", 2, 1, Console.CursorTop + 1);
                Console.ResetColor();
                
                firingBoardUI.DrawBoard(CursorPosition, isControlled ? (1, 1) : (0, 0));
                if (!isMessageBox) ownBoardUI.DrawBoard((0, 0), (0, 0));

                else messageBox.DisplayMessageBox(message);
                    
                const string? controlHelperMessage = "← ↑ → ↓ : Choose Position for Bomb \n\n" +
                                                     "Enter : Place Bomb \n\n" + 
                                                     "F : Message Box/Own Board \n\n" +
                                                     "R : Rewind \n\n" +
                                                     "C : Main Menu \n\n";
                
                SF.GridBasedText(controlHelperMessage, 2, 1, Console.CursorTop + 2);
                SF.GameFooter(Console.CursorTop + 1, 30, "─");
                
                if (!isControlled)
                {
                    Thread.Sleep(1000);
                    break;
                }
                
                KeyboardControl();

            } while (!IsPlacementCompleted);
        }

        
        /// <summary>
        /// Allow User to Use Navigation System.
        /// </summary>
        private void KeyboardControl()
        {
            var (xPos, yPos) = CursorPosition;
            
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    yPos = yPos > 0 ? yPos - 1 : yPos;
                    break;
                case ConsoleKey.DownArrow:
                    yPos = yPos < YBoardLimit ? yPos + 1 : yPos;
                    break;
                case ConsoleKey.LeftArrow:
                    xPos = xPos > 0 ? xPos - 1 : xPos;
                    break;
                case ConsoleKey.RightArrow:
                    xPos = xPos < XBoardLimit ? xPos + 1 : xPos;
                    break;
                case ConsoleKey.Enter:
                    IsPlacementCompleted = true;
                    break;
                case ConsoleKey.F:
                    isMessageBox = !isMessageBox;
                    break;
                case ConsoleKey.R:
                    ViewAction = ViewAction.Rewind;
                    IsPlacementCompleted = true;
                    break;
                case ConsoleKey.C:
                    ViewAction = ViewAction.MenuRun;
                    IsPlacementCompleted = true;
                    break;
            }

            CursorPosition = (xPos, yPos);
        }
    }
}