using Figgle;
using System;
using System.Threading;
using System.Collections.Generic;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Views
{
    
    /// <summary>
    /// Describes BattleShip Game Startup Intro View Solution.
    /// </summary>
    public static class StartupView
    {

        /// <summary>
        /// Displays Startup View in Console.
        /// </summary>
        public static void RunView()
        {
            Console.CursorVisible = false;
            //SoundUtils.PlayMusic("Intro.wav");
            
            var random = new Random();
            var introColors = new List<ConsoleColor> { ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.White };
           
            var gameTitle = FiggleFonts.Doom.Render("Battleship");
            var headerUnderline = SF.StringRepeater("─", gameTitle.Split("\n")[0].Length + 6);

            do
            {

                Console.ForegroundColor = introColors[random.Next(introColors.Count)];
                
                SF.GridBasedText(gameTitle, 2, 1, 11);
                SF.GridBasedText(headerUnderline, 2, 1, Console.CursorTop);

                var cursorPos = Console.CursorTop;
                
                AnimationUtils.ErasingAnimation(0, 0, Console.WindowWidth, cursorPos + 1);
                
                Console.ResetColor(); 
                SF.GridBasedText("--> Press Any Key to Continue <--", 2, 1, cursorPos + 1);
                Thread.Sleep(50);

                if (!Console.KeyAvailable) continue;
                
                Console.ReadKey(true);
                break;
                
            } while (true);
        }
    }
}