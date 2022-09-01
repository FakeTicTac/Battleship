using Figgle;
using System;
using BLL.Battleship;
using ConsoleUI.Battleship.Items;
using System.Collections.Generic;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Views
{
    
    /// <summary>
    /// Describes BattleShip Game Player Creation View Solution.
    /// </summary>
    public static class PlayerCreationView
    {

        /// <summary>
        /// Draws Player Creation View and Collects Needed Data. (Users Names)
        /// </summary>
        /// <param name="isSingle">Indicator Single Player Game or Multiplayer.</param>
        /// <returns>Users Names for Game Creation.</returns>
        public static (string?, string?) RunView(bool isSingle)
        {
            var namePlayerB = isSingle ? Player.AINameInitialization() : null;
            var ((blueXPos, blueYPos), (yellowXPos, yellowYPos)) = DrawView(namePlayerB);
                
            Console.CursorVisible = true;
            
            SF.SetCursorAndDrawText("-> ", blueXPos, blueYPos);
            var namePlayerA = Console.ReadLine()?.Trim();
            
            if (!isSingle) SF.SetCursorAndDrawText("-> ", yellowXPos, yellowYPos);
            namePlayerB = isSingle ? namePlayerB : Console.ReadLine()?.Trim();

            Console.CursorVisible = false;

            return (namePlayerA, namePlayerB);
        }
       
        
        /// <summary>
        /// Draws the View For Player Creation.
        /// </summary>
        /// <param name="yellowName">Name of the AI if Game is SinglePlayer.</param>
        /// <returns>X and Y Coordinates of Last Letter Written in Console of Each Avatar.</returns>
        private static ((int, int), (int, int)) DrawView(string? yellowName)
        {
            Console.Clear();
            SF.GameHeader();

            Console.ForegroundColor = ConsoleColor.Cyan;
            SF.GridBasedText($"<=== Now, It's Your Turn to Introduce Yourselves! ===>", 2, 1, Console.CursorTop + 2);
            Console.ResetColor();
            SF.GridBasedText($"Please, Write Your Names, My Generals.", 2, 1, Console.CursorTop);

            var cursorTop = Console.CursorTop + 3;
            
            var (blueTextPos, yellowTextXPos) = PlayerAvatarDisplay(cursorTop, yellowName);

            var versusText = FiggleFonts.EftiRobot.Render("VS");
            SF.GridBasedText(versusText, 2, 1, cursorTop + 1);
            
            SF.GameFooter(Console.CursorTop + 6, 30, "~");

            return (blueTextPos, yellowTextXPos);
        }


        /// <summary>
        /// Displays Players Avatars on Screen.
        /// </summary>
        /// <param name="yPosition">Y Coordinate Position for Avatars to be Displayed.</param>
        /// <param name="yellowName">Name of the AI if Game is SinglePlayer.</param>
        /// <returns>X and Y Coordinates of Last Letter Written in Console of Each Avatar.</returns>
        private static ((int, int), (int, int)) PlayerAvatarDisplay(int yPosition, string? yellowName)
        {
            var blueTeamColorScheme = new List<ConsoleColor> { ConsoleColor.DarkCyan, ConsoleColor.Black, 
                                      ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.DarkGray };
            
            var yellowTeamColorScheme = new List<ConsoleColor> {ConsoleColor.DarkYellow, ConsoleColor.Black, 
                                    ConsoleColor.Yellow, ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.DarkGray };

            const int avatarXSize = 16;
            const int avatarPadding = 8;
            
            var blueAvatarXCoordinate = Console.WindowWidth / 2 - avatarXSize - avatarPadding;
            var yellowAvatarXCoordinate = Console.WindowWidth / 2 + avatarPadding;

            var blueAvatar = new PictureBoxItemUI(blueAvatarXCoordinate, yPosition, avatarXSize, 10, "Blue");
            var yellowAvatar = new PictureBoxItemUI(yellowAvatarXCoordinate, yPosition, avatarXSize, 10, "Yellow");
            
            blueAvatar.DrawPictureBox(blueTeamColorScheme, false);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            SF.SetCursorAndDrawText("Name: ", blueAvatarXCoordinate, Console.CursorTop + 1);
            Console.ResetColor();
            
            var blueXPos = Console.CursorLeft;
            
            yellowAvatar.DrawPictureBox(yellowTeamColorScheme, false);
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            SF.SetCursorAndDrawText($"Name: ", yellowAvatarXCoordinate, Console.CursorTop + 1);
            Console.ResetColor();
            Console.Write(yellowName);
            
            var yellowXPos = Console.CursorLeft;
            
            return ((blueXPos, Console.CursorTop), (yellowXPos, Console.CursorTop));
        }
    }
}