using System;
using ConsoleUI.Menu;
using System.Collections.Generic;
using ConsoleUI.Battleship.Items;

using BM  = BLL.Menu;
using SF_Game = ConsoleUI.Battleship.SharedUI;
using SF_Menu = ConsoleUI.Menu.Shared.SharedUI;


namespace ConsoleUI.Battleship.Extensions
{
    
    /// <summary>
    /// Describes Loading Menu Console View Solution.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LoadingMenuView : SelectionMenuUI
    {
        /// <summary>
        /// Display Selection Menu in Console Application.
        /// </summary>
        /// <param name="menus">Reference to the Menus for Displaying.</param>
        /// <param name="selectedMenu">Indicator of Currently Selected Menu for Displaying.</param>
        /// <param name="topPos">Position on X Coordinate to Display Menu.</param>
        public static void DisplayMenu(List<BM.Menu> menus, int selectedMenu, int topPos = 4)
        {
            Console.Clear();
            
            var afterMenu = menus[0].MenuItems.Count > menus[1].MenuItems.Count ? menus[0].MenuItems.Count : menus[1].MenuItems.Count;
            
            foreach (var menu in menus)
            {
                var titlePos = menus.IndexOf(menu) == 0 ? Console.WindowWidth / 2 - ($"{menu}".Length + 5) : Console.WindowWidth / 2 + 5;
                
                TypeBoxItem.DisplayBoxNoType(titlePos - 1, topPos - 1, $"{menu}".Length + 2, 3);
                
                if (menus.IndexOf(menu) != selectedMenu) Console.ForegroundColor = ConsoleColor.DarkGray;
                
                SF_Game.SetCursorAndDrawText($"{menu}", titlePos, topPos);

                var menuItems = menu.CopyOfMenuItems();

                foreach (var item in menuItems)
                {
                    var isSelected = menu.XCursorPosition == menuItems.IndexOf(item);
                    var textToDraw = isSelected ? $"~~ {item} ~~" : $"{item}";

                    SF_Menu.ColorDecider(isSelected, ConsoleColor.Cyan, Console.ForegroundColor);

                    if (menus.IndexOf(menu) != selectedMenu) Console.ForegroundColor = ConsoleColor.DarkGray;

                    if (menuItems.IndexOf(item) >= menuItems.Count - menu.ReservedMenuItems)
                    {
                        SF_Game.GridBasedText(textToDraw, 2, 1, topPos + 4 + afterMenu);
                        afterMenu++;
                    }
                    else SharedUI.SetCursorAndDrawText(textToDraw, 
                        titlePos + $"{menu}".Length / 2 - textToDraw.Length / 2, topPos + 4 + menuItems.IndexOf(item));
                    
                    Console.ResetColor();
                }

                afterMenu -= 2;
            }
           
            SF_Game.GridBasedText("Press F to Switch Between Menus", 2, 1, Console.CursorTop + 3);
            SF_Menu.MenuFooter(20, Console.CursorTop + 2);
        }
    }
}