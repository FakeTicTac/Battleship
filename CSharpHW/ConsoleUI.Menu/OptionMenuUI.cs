using System;
using BLL.Menu.MenuEnums;
using BLL.Menu.MenuInterfaces;

using BM  = BLL.Menu;
using SF = ConsoleUI.Menu.Shared.SharedUI;


namespace ConsoleUI.Menu
{
    
    /// <summary>
    /// Describes Option Menu Console UI Solution.
    /// </summary>
    public static class OptionMenuUI
    {
        
        /// <summary>
        /// 
        /// </summary>
        private const int _menuLenght = 70;


        /// <summary>
        /// Display Option Menu in Console Application.
        /// </summary>
        /// <param name="menu">Reference to the Menu for Displaying.</param>
        /// <param name="topPos">Position on X Coordinate to Display Menu.</param>
        public static void DisplayMenu(BM.Menu menu, int topPos)
        {
            Console.Clear();
            SF.MenuHeader($"{menu}", topPos, _menuLenght);
            
            var menuItems = menu.CopyOfMenuItems();

            foreach (var item in menuItems)
            {
                var isSelected = menu.XCursorPosition == menuItems.IndexOf(item);
                
                if (!PlaceReservedMenuItems(item, isSelected)) PlaceOptionMenuItems(item, isSelected);
                
                Console.ResetColor();
            }
            
            SF.MenuFooter(_menuLenght, Console.CursorTop + 2);
        }


        /// <summary>
        /// Define Shape of Menu Item Option Place.
        /// </summary>
        /// <param name="menuItem">Menu Item for Placing.</param>
        /// <param name="isSelected">Indicator of Item Selection.</param>
        private static void PlaceOptionMenuItems(IMenuItem menuItem, bool isSelected)
        {
            if (menuItem.IsHidden)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                SF.SetCursorAndDrawText($"{menuItem} :", Console.WindowWidth / 2 - _menuLenght / 2, Console.CursorTop + 1);
                
                SF.SetCursorAndDrawText(menuItem.YCursorPosition != 0 ? "<=" : "  ", Console.WindowWidth / 2);
            }
            else
            {
                SF.SetCursorAndDrawText($"{menuItem} :", Console.WindowWidth / 2 - _menuLenght / 2, Console.CursorTop + 1);
                
                SF.SetCursorAndDrawText(menuItem.YCursorPosition != 0 ? "<=" : "  ", Console.WindowWidth / 2);
                SF.ColorDecider(isSelected, ConsoleColor.Cyan, Console.ForegroundColor);
            }

            Console.Write($"  {menuItem.MenuItemOptions[menuItem.YCursorPosition]}  ");
            
            Console.Write(menuItem.MenuItemOptions.Count - 1 > menuItem.YCursorPosition ? "=>" : "  ");
            Console.WriteLine();
        }


        /// <summary>
        /// Define Shape of Bottom Reserved Items.
        /// </summary>
        /// <param name="menuItem">Menu Item for Reserved Check and Placement.</param>
        /// <param name="isSelected">Indicator of Item Selection.</param>
        /// <returns>Indicator of Placement Success.</returns>
        private static bool PlaceReservedMenuItems(IMenuItem menuItem, bool isSelected)
        {
            var yCursorPos = 0;
            string? textToDraw = null;

            switch (menuItem.MenuItemAction)
            {
                case EMenuItemAction.Continue:
                    textToDraw = $"|| {menuItem} ===>";
                    yCursorPos = Console.WindowWidth / 2 + 1;
                    break;
                case EMenuItemAction.Return:
                    textToDraw = $"<=== {menuItem} ||";
                    yCursorPos = Console.WindowWidth / 2 - textToDraw.Length;
                    break;
            }

            if (textToDraw == null) return false;
            
            SF.ColorDecider(isSelected, ConsoleColor.Cyan, Console.ForegroundColor);
            Console.SetCursorPosition(yCursorPos, Console.CursorTop + 2);
            Console.Write(textToDraw);

            return true;
        }
    }
}