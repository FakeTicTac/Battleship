using BLL.Menu;
using BLL.Menu.MenuEnums;
using BLL.Battleship.Enums;
using BLL.Menu.MenuInterfaces;
using System.Collections.Generic;


namespace WebApp.BattleShip.Initializers
{
    
    /// <summary>
    /// In Game Menu Initializers.
    /// </summary>
    public static class MenuInitializers
     {
        
         /// <summary>
         /// Initialize Main Menu Logic.
         /// </summary>
         /// <returns>Main Menu Logic.</returns>
         public static Menu MainMenuInitialize()
         {
             var menu = new Menu("Main Menu", EMenuType.Selection, EMenuLevel.Root);

             menu.AddMenuItems(new List<IMenuItem>
             {
                 new MenuItem<string>("New Game", () => "GameSettings"),
                 new MenuItem<string>("Load Games", () => "LoadGames"),
                 new MenuItem<string>("Load Settings", () => "LoadSettings"),
                 new MenuItem<string>("Cheats", () => "MainMenu")
             });

             return menu;
         }
         
         
        /// <summary>
        /// Initialize Settings Menu Logic.
        /// </summary>
        /// <returns>Settings Menu Logic.</returns>
        public static Menu SettingMenuInitialize()
        {
           var settingMenu = new Menu("Settings Menu", EMenuType.Option, EMenuLevel.Root);

            var itemGameMode = new MenuItem<bool?>("Game Mode", () => null);
            var itemAiLevel = new MenuItem<int?>("AI Level", () => null);
            var itemBoardSizeX = new MenuItem<int?>("Board Width", () => null);
            var itemBoardSizeY = new MenuItem<int?>("Board Lenght", () => null);
            var itemShipUsage = new MenuItem<bool?>("Ship Usage Mode", () => null);
            var itemShipCollision = new MenuItem<int?>("Ship Collision Mode", () => null);


            itemGameMode.AddMenuItemOptions(new List<IMenuItemOption>
            {
                new MenuItemOption<EGameMode>("Single Player", () => EGameMode.SinglePlayer),
                new MenuItemOption<EGameMode>("Multi Player", () => EGameMode.MultiPlayer)
            });
            
            itemAiLevel.AddMenuItemOptions(new List<IMenuItemOption>
            {
                new MenuItemOption<EAILevel>("Easy", () => EAILevel.Easy),
                new MenuItemOption<EAILevel>("Medium", () => EAILevel.Medium),
                new MenuItemOption<EAILevel>("Hard", () => EAILevel.Hard),
            });
            
            for (var x = 4; x <= 20; x++)
            {
                var sizeValue = x;

                itemBoardSizeX.AddMenuItemOption(new MenuItemOption<int>($"{sizeValue}", () => sizeValue));
                itemBoardSizeY.AddMenuItemOption(new MenuItemOption<int>($"{sizeValue}", () => sizeValue));
            }

            itemBoardSizeX.MoveCursorPosition(7);
            itemBoardSizeY.MoveCursorPosition(7);

            itemShipUsage.AddMenuItemOptions(new List<IMenuItemOption>
            {
                new MenuItemOption<EShipUsageRule>("Players Use Own Ships", () => EShipUsageRule.DifferentShips)
            });

            itemShipCollision.AddMenuItemOptions(new List<IMenuItemOption>
            {
                new MenuItemOption<EShipTouchRule>("Standard Collision Mode", () => EShipTouchRule.NoTouching),
                new MenuItemOption<EShipTouchRule>("Ship Corners Can Touch Mode", () => EShipTouchRule.CornersCanTouch),
                new MenuItemOption<EShipTouchRule>("Ship All Sides Can Touch Player",
                    () => EShipTouchRule.SidesCanTouch)
            });

            settingMenu.AddMenuItems(new List<IMenuItem>
            {
                itemGameMode,
                itemAiLevel,
                itemBoardSizeX,
                itemBoardSizeY,
                itemShipUsage,
                itemShipCollision
            });
            
            return settingMenu;
        }
     }
}