using System;
using Figgle;
using System.Threading;
using System.Collections.Generic;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Views
{

    /// <summary>
    /// Describes BattleShip Game Cheats View Solution.
    /// </summary>
    public static class CheatsView
    {

        /// <summary>
        /// Displays Cheats View in Console.
        /// </summary>
        public static void RunView()
        {
            var random = new Random();
            var blinkColors = new List<ConsoleColor> { ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.White };
           
            var textLineOne = FiggleFonts.Doom.Render("You Have Been Rick Rolled!");
            var textLineTwo = FiggleFonts.Doom.Render("Play Fair!");

            do
            {

                Console.ForegroundColor = blinkColors[random.Next(blinkColors.Count)];
                
                SF.GridBasedText(textLineOne, 2, 1, Console.WindowHeight / 2 - 10);
                SF.GridBasedText(textLineTwo, 2, 1, Console.CursorTop);

                var cursorPos = Console.CursorTop;
                
                AnimationUtils.ErasingAnimation(0, 0, Console.WindowWidth, cursorPos + 1);
                Console.ResetColor(); 
                
                SF.GridBasedText("--> Press Any Key to Return <--", 2, 1, cursorPos + 2);
                Thread.Sleep(50);

                if (!Console.KeyAvailable) continue;
                
                Console.ReadKey(true);
                break;
                
            } while (true);
        }
    }
}
