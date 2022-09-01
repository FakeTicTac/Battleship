using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Views
{
    
    /// <summary>
    /// Describes BattleShip Game Loading Screen View Solution.
    /// </summary>
    public static class LoadingScreenView
    {
        
        /// <summary>
        /// Indicator of Threading Lock.
        /// </summary>
        private static readonly object _object = new();  
        
        
        /// <summary>
        /// Represents The List of Messages To Be Displayed on Loading Screen;
        /// </summary>
        private static readonly List<string> _messages = new()  
        {
            "Preparing Battlefields.",
            "Placing Ships on the Fields.",
            "Declaring War to the Enemy.",
            "Encouraging Soldiers to Fight.",
            "Generals are Making their Plans."
        };
        
        
        /// <summary>
        /// Displays Loading Screen View in the Console.
        /// </summary>
        public static void RunView()
        {
            Console.Clear();

            SF.GameHeader();
            SF.GameFooter(Console.CursorTop + 15, 50, "─");

            var cursorTop = Console.CursorTop - 7;
            
            Task lineLoadingBar = Task.Factory.StartNew(() => DisplayLoadingBar(cursorTop));
            Task messageLoadingBar = Task.Factory.StartNew(() => DisplayLoadingMessages(new Random().Next(2, 3), cursorTop - 3));
            
            Task.WaitAll(lineLoadingBar, messageLoadingBar);
        }


        /// <summary>
        /// Displays Loading Bar Progress in Console.
        /// </summary>
        /// <param name="yPos">Y Coordinate Position for Loading Bar to be Displayed.</param>
        private static void DisplayLoadingBar(int yPos)
        { 
            for (var x = 0; x < 40; x++) 
            {
                lock (_object)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    SF.SetCursorAndDrawText(" ", Console.WindowWidth / 2 - 20 + x, yPos);
                }
                
                Thread.Sleep(70); 
            }
            
            Console.ResetColor();
        }


       /// <summary>
       /// Displays Loading Bar in the Console.
       /// </summary>
       /// <param name="repeatCounter">Number of Phrases to be Drawn.</param>
       /// <param name="yPos">Y Coordinate Position for Loading Bar to be Displayed.</param>
        private static void DisplayLoadingMessages(int repeatCounter, int yPos)
        {

            for (var x = 0; x < repeatCounter; x++)
            {
                lock (_object)
                {
                    Console.ResetColor();
                    SF.ClearCurrentConsoleLine(yPos + 1);
                }

                var randomMessage = new Random().Next(_messages.Count);

                lock (_object)
                {
                    SF.GridBasedText(_messages[randomMessage], 2, 1, yPos);
                        
                    for (var y = 0; y < 2; y++)
                    {
                        Thread.Sleep(500);
                        Console.Write(".");
                    }
                }
                _messages.RemoveAt(randomMessage);

                Thread.Sleep(1000);
            }
        }
    }
}