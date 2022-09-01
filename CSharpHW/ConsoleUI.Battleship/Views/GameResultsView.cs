using System;
using BLL.Battleship;
using System.Collections.Generic;
using ConsoleUI.Battleship.Items;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Views
{
    
    /// <summary>
    /// Describes BattleShip Game Game Results View Solution.
    /// </summary>
    public static class GameResultsView
    {
        /// <summary>
        /// Displays Final View of the Game Results in Console.
        /// </summary>
        /// <param name="boardA">Board State of the First Player.</param>
        /// <param name="boardB">Board State of the Second Player.</param>
        /// <param name="winnerName">Name of the Winner.</param>
        /// <param name="isPlayerA">Indicator of Player A Winning the Game.</param>
        public static void RunView(BoardSquareState[,] boardA, BoardSquareState[,] boardB, string winnerName, bool isPlayerA)
        {
            Console.Clear();
            SF.GameHeader();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            SF.GridBasedText($"<=== General {winnerName} Won This Battle! ===>", 2, 1, Console.CursorTop + 1);
            Console.ResetColor();

            var cursorTop = Console.CursorTop + 3;


            PlayerAvatarDisplay(cursorTop, isPlayerA);
            DisplayBoards(boardA, boardB, cursorTop);
            
            const string? controlHelperMessage = "--> Please, Press Any Key To Finish the Game <--";
            SF.GridBasedText(controlHelperMessage, 2, 1, Console.CursorTop + 5);
            SF.GameFooter(Console.CursorTop + 1, 30, "─");
            
            Console.ReadKey();
        }


        /// <summary>
        /// Displays Players Avatars on Screen.
        /// </summary>
        /// <param name="yPosition">Y Coordinate Position for Avatars to be Displayed.</param>
        /// <param name="isPlayerA">Indicator of Player A Winning the Game.</param>
        private static void PlayerAvatarDisplay(int yPosition, bool isPlayerA)
        {
            var blueTeamColorScheme = new List<ConsoleColor> { ConsoleColor.DarkCyan, ConsoleColor.Black, 
                                      ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.DarkGray };
            
            var yellowTeamColorScheme = new List<ConsoleColor> {ConsoleColor.DarkYellow, ConsoleColor.Black, 
                                    ConsoleColor.Yellow, ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.DarkGray };

            const int avatarXSize = 8;
            const int avatarPadding = 3;
            
            var blueAvatarXCoordinate = Console.WindowWidth / 2 - avatarXSize - avatarPadding;
            var yellowAvatarXCoordinate = Console.WindowWidth / 2 + avatarPadding;

            var blueAvatar = new PictureBoxItemUI(blueAvatarXCoordinate, yPosition, avatarXSize, 6, "Blue");
            var yellowAvatar = new PictureBoxItemUI(yellowAvatarXCoordinate, yPosition, avatarXSize, 6, "Yellow");
            
            blueAvatar.DrawPictureBox(blueTeamColorScheme, false);
            yellowAvatar.DrawPictureBox(yellowTeamColorScheme, false);

            var prevCursorPos = Console.CursorTop + 1;
            var bannerXCoordinate = isPlayerA ? blueAvatarXCoordinate : yellowAvatarXCoordinate;

          
            WinnerBanner(bannerXCoordinate, prevCursorPos + 1, avatarXSize, ConsoleColor.White);
        }


        /// <summary>
        /// Draw Banner of The Winner In Console.
        /// </summary>
        private static void WinnerBanner(int xPos, int yPos, int bannerWidth, ConsoleColor color)
        {
            for (var y = 0; y < 3; y++)
            {
                for (var x = 0; x < bannerWidth; x++)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = color;
                    SF.SetCursorAndDrawText(" ", xPos + x, yPos + y);
                }
            }
            
            SF.SetCursorAndDrawText("WINNER", xPos + 1, yPos + 1);
            Console.ResetColor();
        }

        
        /// <summary>
        /// Displays Final View of Boards in the Console.
        /// </summary>
        /// <param name="boardA">Board State of the First Player.</param>
        /// <param name="boardB">Board State of the Second Player.</param>
        /// <param name="yPos">Y Coordinate Position for Boards to be Displayed.</param>
        private static void DisplayBoards(BoardSquareState[,] boardA, BoardSquareState[,] boardB, int yPos)
        {
            var boardADisplay = new GameBoardItemUI(14, yPos, boardA);
            var boardBDisplay = new GameBoardItemUI(-14, yPos, boardB);
            
            boardADisplay.DrawBoard((0, 0), (0, 0));
            boardBDisplay.DrawBoard((0, 0), (0, 0));
        }
    }
}