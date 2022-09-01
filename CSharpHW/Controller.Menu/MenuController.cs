using System;
using BLL.Menu;
using ConsoleUI.Menu;
using BLL.Menu.MenuEnums;

using SF = ConsoleUI.Menu.Shared.SharedUI;


namespace MenuSystem
{
    
    /// <summary>
    /// Class Describes Controller between UI and Logic.
    /// </summary>
    public class MenuController
    {
        
        /// <summary>
        /// Defines connection to Menu Logic.
        /// </summary>
        protected Menu Menu { get; }

        
        /// <summary>
        /// Menu Controller basic constructor.
        /// </summary>
        /// <param name="menu">Defines connection to Menu Logic.</param>
        public MenuController(Menu menu) => Menu = menu;
        
        
        /// <summary>
        /// Run Menu Logic with Needed UI Solution.
        /// </summary>
        /// <returns>Returns the Menu State.</returns>
        public Menu? Run()
        {
            do
            {
                MenuDisplayChooser();

                var menu = SelectedMenuDecider();
                var isEnterPressed = KeyboardControl();
                
                var menuItem = menu.MenuItems[menu.XCursorPosition];
                
                if (menu.MenuType == EMenuType.Option && isEnterPressed && (menuItem.MenuItemAction == EMenuItemAction.Continue))
                {
                    return menu;

                }

                if (menu.MenuType == EMenuType.Option && isEnterPressed && (menuItem.MenuItemAction == EMenuItemAction.Return))
                {
                    return null;
                }
                
                
                if (menu.MenuType == EMenuType.Option && menuItem.MenuItemAction == EMenuItemAction.Accept)
                {
                    menu.MenuItems[menu.XCursorPosition].triggerOnChose();
                    continue;
                }
                
                
                if (isEnterPressed && menuItem.MenuItemAction == EMenuItemAction.Accept)
                {
                    var itemExecutionResult = menuItem.ExecuteFunction();
                    menu = itemExecutionResult is Menu ? itemExecutionResult : Menu;
                    
                    menuItem = menu.MenuItems[menu.XCursorPosition];

                }


                if (isEnterPressed && MenuReturnChooser(menuItem.MenuItemAction))
                    return menu;
                
                if (isEnterPressed && menuItem.MenuItemAction == EMenuItemAction.Return) 
                    break;
                

            } while (true);

            return null;
        }


        protected virtual Menu SelectedMenuDecider()
        {
            return Menu;
        }
        

        /// <summary>
        /// Activate Keyboard Control for the Menu.
        /// </summary>
        /// <returns>Indicator of Enter Key Activation.</returns>
        protected virtual bool KeyboardControl()
        {
            var isEnterPressed = false;

            var menuItems = Menu.MenuItems;
            
            var xPos = Menu.XCursorPosition;
            var yPos = menuItems[xPos].YCursorPosition;
            
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    
                    do
                    {
                        xPos = xPos > 0 ? xPos - 1 : 0;

                        if (Menu.MenuItems[xPos].IsHidden)
                        {
                            continue;
                        }

                        if (xPos == 0 && Menu.MenuItems[0].IsHidden)
                        {
                            xPos = Menu.XCursorPosition;
                            break;
                        }

                        if (!Menu.MenuItems[xPos].IsHidden)
                        {
                            break;
                        }
                    } while (true);
                    
                   
                    
                    yPos = menuItems[xPos].YCursorPosition;
                    break;
                case ConsoleKey.DownArrow:
                    
                    do
                    {
                        xPos = xPos < menuItems.Count - 1 ? xPos + 1 : xPos;

                        if (Menu.MenuItems[xPos].IsHidden)
                        {
                            continue;
                        }

                        if (xPos == menuItems.Count - 1 && Menu.MenuItems[xPos].IsHidden)
                        {
                            xPos = Menu.XCursorPosition;
                            break;
                        }

                        if (!Menu.MenuItems[xPos].IsHidden)
                        {
                            break;
                        }
                        
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
            }
            
            Menu.MoveCursorPosition((xPos, yPos));

            return isEnterPressed;
        }
        
        
        /// <summary>
        /// Chooses the Menu to return based on Predicates.
        /// </summary>
        /// <param name="itemAction">Item Action for Decision.</param>
        /// <returns>Boolean of Menu Choice. False if "new" and True if "old".</returns>
        private bool MenuReturnChooser(EMenuItemAction itemAction) => 
            itemAction == EMenuItemAction.ToMainMenu && Menu.MenuLevel != EMenuLevel.Root || itemAction == EMenuItemAction.ToExit;

        
        /// <summary>
        /// Chooses appropriate UI Solution for Menu Drawing based on Menu Type.
        /// </summary>
        protected virtual void MenuDisplayChooser()
        {
            switch (Menu.MenuType)
            {
                case EMenuType.Selection:
                    SelectionMenuUI.DisplayMenu(Menu, 5);
                    break;
                case EMenuType.Option:
                    OptionMenuUI.DisplayMenu(Menu, 5);
                    break;
            }
        }
    }
}