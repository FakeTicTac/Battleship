using System;
using MenuSystem;
using ConsoleUI.Menu;
using BLL.Menu.MenuEnums;
using System.Collections.Generic;

using BM = BLL.Menu;
using SF = ConsoleUI.Menu.Shared.SharedUI;


namespace ConsoleUI.Battleship.Extensions
{
    
    /// <summary>
    /// Class Describes Loading Menu Controller -> Connection Between UI and Logic.
    /// </summary>
    public class LoadingMenuController : MenuController
    {
        
        /// <summary>
        /// Indicator of Selected Menu in the List.
        /// </summary>
        private int SelectedMenu { get; set; }

        
        /// <summary>
        /// List of Menu To Be Displayed and Processed.
        /// </summary>
        private List<BM.Menu> Menus { get; }


        /// <summary>
        /// Menu Controller Basic Constructor.
        /// </summary>
        /// <param name="menuList">Defines Connection to Menu Logics.</param>
        public LoadingMenuController(List<BM.Menu> menuList) : base(menuList[0])
        {
            if (menuList.Count < 1)
                throw new ArgumentException("There Should be at Least One Menu.");

            Menus = menuList;
            SelectedMenu = 0;
        }

        
        /// <summary>
        /// Decider Menu Which to Process.
        /// </summary>
        /// <returns>Menu Which to Process</returns>
        protected override BM.Menu SelectedMenuDecider() => Menus[SelectedMenu];


        /// <summary>
        /// Chooses Appropriate UI Solution for Menu Drawing Based on Menu Type.
        /// </summary>
        protected override void MenuDisplayChooser()
        {
            switch (Menu.MenuType)
            {
                case EMenuType.Selection:
                    LoadingMenuView.DisplayMenu(Menus, SelectedMenu,  5);
                    break;
                case EMenuType.Option:
                    OptionMenuUI.DisplayMenu(Menu, 5);
                    break;
                default:
                    return;
            }
        }
        
        
        /// <summary>
        /// Activate Keyboard Control for the Menu.
        /// </summary>
        /// <returns>Indicator of Enter Key Activation.</returns>
        protected override bool KeyboardControl()
        {
            var isEnterPressed = false;

            var menuItems = Menus[SelectedMenu].MenuItems;
            
            var xPos = Menus[SelectedMenu].XCursorPosition;
            var yPos = menuItems[xPos].YCursorPosition;
            
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:

                    do
                    {
                        xPos = xPos > 0 ? xPos - 1 : 0;

                        if (!menuItems[xPos].IsHidden) break;

                        if (xPos != 0 || !menuItems[0].IsHidden) continue;
                        
                        xPos = Menus[SelectedMenu].XCursorPosition;
                        
                    } while (true);
                    
                    yPos = menuItems[xPos].YCursorPosition;
                    break;
                
                case ConsoleKey.DownArrow:
                    
                    do
                    {
                        xPos = xPos < menuItems.Count - 1 ? xPos + 1 : xPos;

                        if (!menuItems[xPos].IsHidden) break;

                        if (xPos != menuItems.Count - 1 || !menuItems[xPos].IsHidden) continue;
                        
                        xPos = Menus[SelectedMenu].XCursorPosition;
                        
                    } while (true);
                    
                    yPos = menuItems[xPos].YCursorPosition;
                    break;
                
                case ConsoleKey.RightArrow:
                    yPos = yPos < menuItems[xPos].MenuItemOptions.Count - 1 ? yPos + 1 : yPos;
                    break;
                case ConsoleKey.LeftArrow:
                    yPos = yPos > 0 ? yPos - 1 : 0;
                    break;
                case ConsoleKey.Enter:
                    isEnterPressed = true;
                    break;
                case ConsoleKey.F:
                    SelectedMenu = SelectedMenu + 1 <= Menus.Count - 1 ? SelectedMenu + 1 : 0;
                    return false;
            }
            
            Menus[SelectedMenu].MoveCursorPosition((xPos, yPos));

            return isEnterPressed;
        }
    }
}