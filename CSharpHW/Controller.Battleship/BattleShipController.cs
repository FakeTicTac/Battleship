using System;
using BLL.Menu;
using MenuSystem;
using BLL.Battleship;
using DAL.Battleship;
using BLL.Menu.MenuEnums;
using System.Diagnostics;
using BLL.Battleship.Enums;
using ConsoleUI.Battleship;
using BLL.Menu.MenuInterfaces;
using BLL.Battleship.Repository;
using ConsoleUI.Battleship.Views;
using System.Collections.Generic;
using ConsoleUI.Battleship.Enums;
using ConsoleUI.Battleship.Items;
using Controller.Battleship.Enums;
using ConsoleUI.Battleship.Extensions;

using SF  = ConsoleUI.Battleship.SharedUI;


namespace Controller.Battleship
{

    /// <summary>
    /// Class Describes Battleship Controller Between UI and Logic Solutions.
    /// </summary>
    public class BattleShipController
    {

        /// <summary>
        /// Defines Connection to Battleship Game Logic.
        /// </summary>
        private static BattleShip? BattleShipLogic { get; set; }
        
        
        /// <summary>
        /// Settings Repository Access Definition. (Settings State Loading and Saving)
        /// </summary>
        private SettingsRepository SettingsRepository { get; }
        
        
        /// <summary>
        /// BattleShip Repository Access Definition. (Game State Loading and Saving)
        /// </summary>
        private BattleShipRepository BattleShipRepository { get; }
        
        
        /// <summary>
        /// Basis Battleship Controller Constructor. Defines Repository Connections.
        /// </summary>
        /// <param name="appDbContext">Database Layer Connection Definition.</param>
        public BattleShipController(AppDbContext appDbContext)
        {
            var commonSaveDirectory = AppContext.BaseDirectory.Replace("\\ConsoleApp.BattleShip\\bin\\Debug\\net5.0", "") + "Saves";

            SettingsRepository = new SettingsRepository(appDbContext, $"{commonSaveDirectory}\\SettingSaves\\");
            BattleShipRepository = new BattleShipRepository(appDbContext, $"{commonSaveDirectory}\\GameSaves\\");
        }
        
        
        /// <summary>
        /// Turn On Default Console Settings For Game Run.
        /// </summary>
        private static void ConsolePreset()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }
        
        
        /// <summary>
        /// Method Runs' Controller -> Runs Battleship Game.
        /// </summary>
        public void RunController()
        {
            ConsolePreset();
            StartupView.RunView();
            MainMenuRun();
        }
        
        
        /// <summary>
        /// Battleship Game Main Menu Initialization.
        /// </summary>
        private void MainMenuRun()
        {
            SF.GameHeader();

            var mainMenu = new Menu(" Main Menu ", EMenuType.Selection, EMenuLevel.Root);

            mainMenu.AddMenuItems(new List<IMenuItem>
            {
                new MenuItem<EMenuItemAction>("New Game", () => RunGame()),
                new MenuItem<Menu?>("Load Games", LoadGameMenuRun),
                new MenuItem<Menu?>("Load Setting", LoadSettingsMenuRun),
                new MenuItem<bool?>("Cheats", CheatsViewRun),
            });

            new MenuController(mainMenu).Run();
        }

        
        /// <summary>
        /// Battleship Menu Credit Selection Trigger.
        /// </summary>
        /// <returns>Boolean as An Indicator of Run Success.</returns>
        private static bool? CheatsViewRun()
        {
            var processStartInfo = new ProcessStartInfo("https://www.youtube.com/watch?v=dQw4w9WgXcQ4")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(processStartInfo);

            CheatsView.RunView();
            return null;
        }
        
        
        /// <summary>
        /// Battleship Menu Game Load Selection Trigger.
        /// </summary>
        /// <returns>Menu of User Activity in This Section.</returns>
        private Menu? LoadGameMenuRun()
        {
            var loadDBMenu = new Menu(" Database Game Saves ", EMenuType.Selection, EMenuLevel.First);

            BattleShipRepository.GetAllSavesDatabase().ForEach(x => loadDBMenu.AddMenuItem(
                new MenuItem<EMenuItemAction>(x.Name ?? "Unknown Save", () => RunGame(BattleShipRepository.GetSaveDatabase(x.BattleShipId)))));
            
            
            var loadJSONMenu = new Menu(" Local Game Saves ", EMenuType.Selection, EMenuLevel.First);
            
            BattleShipRepository.GetAllSavesLocal().ForEach(x => loadJSONMenu.AddMenuItem(
                new MenuItem<EMenuItemAction>(x.Name ?? "Unknown Save", () => RunGame(x))));
            
            return new LoadingMenuController(new List<Menu> { loadDBMenu, loadJSONMenu }).Run();
        }
        
        
        /// <summary>
        /// Battleship Menu Game Preset Selection Trigger.
        /// </summary>
        /// <returns>Menu of User Activity in This Section.</returns>
        private Menu? LoadSettingsMenuRun()
        {
            var loadDBMenu = new Menu(" Database Settings Saves ", EMenuType.Selection, EMenuLevel.First);

            SettingsRepository.GetAllSavesDatabase().ForEach(x => loadDBMenu.AddMenuItem(
                new MenuItem<EMenuItemAction>(x.Name ?? "Unknown Save", () => RunGame(settings: SettingsRepository.GetSaveDatabase(x.SettingsId)))));
            
            loadDBMenu.AddMenuItem(new MenuItem<Menu?>("Create New Preset", () => GameSettingMenuRun(true)));
            
            var loadJSONMenu = new Menu(" Local Settings Saves ", EMenuType.Selection, EMenuLevel.First);
            
            SettingsRepository.GetAllSavesLocal().ForEach(x => loadJSONMenu.AddMenuItem(
                new MenuItem<EMenuItemAction>(x.Name ?? "Unknown Save", () => RunGame(settings: x))));
            
            loadJSONMenu.AddMenuItem(new MenuItem<Menu?>("Create New Setting", () => GameSettingMenuRun(true, ESaveType.JSON)));
            
            return new LoadingMenuController(new List<Menu> { loadDBMenu, loadJSONMenu }).Run();
        }
        
        
        /// <summary>
        /// Run Battleship Game Logic with Needed Views.
        /// </summary>
        /// <returns>Action to perform in Main Menu.</returns>
        private EMenuItemAction RunGame(BattleShip? battleship = null, Settings? settings = null)
        {
            BattleShipLogic = battleship ?? GameCreation(settings);

            if (BattleShipLogic == null) return EMenuItemAction.Continue;
            
            LoadingScreenView.RunView();

            switch (ShipPlacement())
            {
                case EMenuItemAction.ToMainMenu:
                    return EMenuItemAction.ToMainMenu;
                case EMenuItemAction.ToExit:
                    return EMenuItemAction.ToExit;
            }
    
            LoadingScreenView.RunView();
            
            switch (PlayerShooting())
            {
                case EMenuItemAction.ToMainMenu:
                    return EMenuItemAction.ToMainMenu;
                case EMenuItemAction.ToExit:
                    return EMenuItemAction.ToExit;
            }
            
            EndGame();
            return EMenuItemAction.Continue;
        }
        
        
        /// <summary>
        /// Create an Instance of the Game with Settings.
        /// </summary>
        /// <param name="predefinedSetting">Loaded Settings to Build Game On.</param>
        /// <returns>Assembled Game State.</returns>
        private BattleShip? GameCreation(Settings? predefinedSetting = null)
        {
            var settings = predefinedSetting;

            if (settings == null)
            {
                var userSettingsChoices = GameSettingMenuRun(false);

                if (userSettingsChoices == null) return null;
                
                    var settingsList = userSettingsChoices.TriggerChosenMenuItemOptions();

                settings = new Settings(settingsList[0], settingsList[1], settingsList[2],
                    settingsList[3], settingsList[4], settingsList[5]);
            }

            var (playerAName, playerBName) = PlayerCreationView.RunView(settings.GameMode == EGameMode.SinglePlayer);

            return new BattleShip(playerAName, playerBName, settings);
        }
        
        
        
        /// <summary>
        /// Battleship Menu Game Settings Initialization.
        /// </summary>
        /// <param name="isSave">Indicator of Saving Process.</param>
        /// <param name="saveType">Indicator of Save Type.</param>
        /// <returns>Menu of User Activity in This Section.</returns>
        private Menu? GameSettingMenuRun(bool isSave, ESaveType saveType = ESaveType.Database)
        {
            var settingMenu = new Menu(" Game Settings Menu ", EMenuType.Option, EMenuLevel.Root);

            var itemGameMode = new MenuItem<bool?>("Game Mode", () => null);
            var itemAILevel = new MenuItem<int?>("AI Level", () => null);
            var itemBoardSizeX = new MenuItem<int?>("Board Size on X Coordinate", () => null);
            var itemBoardSizeY = new MenuItem<int?>("Board Size on Y Coordinate", () => null);
            var itemShipUsage = new MenuItem<bool?>("Ship Usage Mode", () => null);
            var itemShipCollision = new MenuItem<int?>("Ship Collision Mode", () => null);

            itemGameMode.AddMenuItemOptions(new List<IMenuItemOption>
            {
                new MenuItemOption<EGameMode>("Single Player", () =>
                {
                    itemAILevel.IsHidden = false;
                    return EGameMode.SinglePlayer;
                }),
                new MenuItemOption<EGameMode>("Multi Player", () =>
                {
                    itemAILevel.IsHidden = true;
                    return EGameMode.MultiPlayer;
                })
            });

            itemAILevel.AddMenuItemOptions(new List<IMenuItemOption>
            {
                new MenuItemOption<EAILevel>("Easy", () => EAILevel.Easy),
                new MenuItemOption<EAILevel>("Medium", () => EAILevel.Medium),
                new MenuItemOption<EAILevel>("Hard", () => EAILevel.Hard),
            });

            for (var x = 4; x <= 50; x++)
            {
                var sizeValue = x;

                itemBoardSizeX.AddMenuItemOption(new MenuItemOption<int>($"{sizeValue}", () => sizeValue));
                itemBoardSizeY.AddMenuItemOption(new MenuItemOption<int>($"{sizeValue}", () => sizeValue));
            }

            itemBoardSizeX.MoveCursorPosition(6);
            itemBoardSizeY.MoveCursorPosition(6);

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
                itemAILevel,
                itemBoardSizeX,
                itemBoardSizeY,
                itemShipUsage,
                itemShipCollision
            });

            var menu = new MenuController(settingMenu).Run();

            if (!isSave || menu == null) return menu;

            var res = menu.TriggerChosenMenuItemOptions();
            var settings = new Settings(res[0], res[1], res[2], res[3], res[4], res[5], true);

            SaveSettings(settings, saveType);

            return null;
        }
        
        
        /// <summary>
        /// Perform Setting Save to The Needed Saving Source.
        /// </summary>
        /// <param name="settings">Setting to be Saved.</param>
        /// <param name="saveType">Indicator of Save Type.</param>
        /// <exception cref="ArgumentException">Thrown if Not Supported Yet Saving Method Executed.</exception>
        private void SaveSettings(Settings settings, ESaveType saveType)
        {
            string? saveName;

            do
            {
                saveName = TypeBoxItem.DisplayBox(14, 30);
                
            } while (string.IsNullOrEmpty(saveName));
            
            switch (saveType)
            {
                case ESaveType.Database:
                    SettingsRepository.SaveToDatabase(settings, saveName);
                    break;
                case ESaveType.JSON:
                    SettingsRepository.SaveToLocal(settings, saveName);
                    break;
                default:
                    throw new ArgumentException("Not Implemented Saving Type.");
            }
        }
        
        
        /// <summary>
        /// Places Ships on the Players Fields Based on Settings Preference.
        /// </summary>
        private EMenuItemAction ShipPlacement()
        {
            for (var x = 0; x < 2; x++)
            {
                switch (DifferentShipPlacement())
                {
                    case EMenuItemAction.ToMainMenu:
                        return EMenuItemAction.ToMainMenu;
                    case EMenuItemAction.ToExit:
                        return EMenuItemAction.ToExit;
                }

                BattleShipLogic!.SwitchTurns();
            }

            return EMenuItemAction.Continue;
        }
        
        
        /// <summary>
        /// User Place His Unique Ships on Own Board.
        /// </summary>
        private EMenuItemAction DifferentShipPlacement()
        {
            string? specialMessage = null;
            var shipPlacementView = new ShipPlacementView(BattleShipLogic!.GetPlayerBoard(true, false), BattleShipLogic.MakingMovePlayerName());
            
            if (BattleShipLogic.PlayerByTurn().IsPlacementCompleted) return EMenuItemAction.Continue;
            
            if (!BattleShipLogic.IsHumanTurn())
            {
                BattleShipLogic.AICreatePlaceShip();
                return EMenuItemAction.Continue;
            }

            do
            {
                shipPlacementView.RunView(BattleShipLogic.GetPlayerBoard(true, false),
                                    BattleShipLogic.MakingMovePlayerBoardCapacity(), specialMessage);

                
                switch (shipPlacementView.ViewAction)
                {
                    case ViewAction.Random:
                        BattleShipLogic.AICreatePlaceShip();
                        SoundUtils.PlayMusic("Splash.wav");
                        continue;
                    case ViewAction.DeleteAll:
                        BattleShipLogic.DeleteAllShips();
                        continue;
                    case ViewAction.DeleteLast:
                        BattleShipLogic.DeleteLastShip();
                        continue;
                    case ViewAction.MenuRun:
                    {
                        var result = InGameMenuRun();
                        switch (result?.MenuItems[result.XCursorPosition].MenuItemAction)
                        {
                            case EMenuItemAction.ToMainMenu:
                                return EMenuItemAction.ToMainMenu;
                            case EMenuItemAction.ToExit:
                                return EMenuItemAction.ToExit;
                        }
                        continue;
                    }
                }


                if (shipPlacementView.ShipName != null)
                {
                    var (xSize, ySize) = shipPlacementView.ShipSizes;
                    var (xCoordinate, yCoordinate) = shipPlacementView.CursorPosition;

                    if (!BattleShipLogic.CreatePlayerShip(shipPlacementView.ShipName, xSize, ySize))
                    {
                        specialMessage = "Ship Cannot be Created, because It Size is Too Big for this Board!";
                        continue;
                    }

                    if (!BattleShipLogic.PlacePlayerShip(BattleShipLogic.PlayerByTurn().OwnShips!.Count - 1, xCoordinate, yCoordinate))
                    {
                        specialMessage = "Ship Cannot be Placed, because It Affects Already Taken Cells on Board!";
                        continue;
                    }
                }

                if (!BattleShipLogic.IsPlayerHaveEnoughShips())
                {
                    specialMessage = "You Have to Place at Least 1 Ship on the Board!";
                    shipPlacementView.isPlacementCompleted = false;
                    continue;
                }
                
                SoundUtils.PlayMusic("Splash.wav");
                
                specialMessage = null;

            } while (!shipPlacementView.isPlacementCompleted);

            BattleShipLogic.PlayerByTurn().IsPlacementCompleted = true;
            
            return EMenuItemAction.Continue;
        }
        
        
        /// <summary>
        /// Makes Players To Shoot on the Boards.
        /// </summary>
        /// <returns>Indicator of Game Exit.</returns>
        private EMenuItemAction PlayerShooting()
        {
            var playerOneShotView = new PlayerShootingView(BattleShipLogic!.GetPlayerBoard(true, true),
                BattleShipLogic.PLayerOne!.Name!);
            var playerTwoShotView = new PlayerShootingView(BattleShipLogic.GetPlayerBoard(false, true),
                BattleShipLogic.PlayerTwo!.Name!);

            var playerList = new List<PlayerShootingView> { playerOneShotView, playerTwoShotView };
            var isFirst = true;

            do
            {
                var lastView = isFirst;
                var message = "Please, Place your Bomb and Destroy Your Enemy!";
                var view = playerList[isFirst ? 0 : 1];

                if (BattleShipLogic.IsHumanTurn())
                {

                    view.RunView(BattleShipLogic.GetPlayerBoard(true, true),
                        BattleShipLogic.GetPlayerBoard(true, false), true, message);
                    
                    
                    switch (view.ViewAction)
                    {
                        case ViewAction.Rewind:
                            BattleShipLogic.DoubleRewind();
                            continue;
                        case ViewAction.MenuRun:
                        {
                            var result = InGameMenuRun();
                            switch (result?.MenuItems[result.XCursorPosition].MenuItemAction)
                            {
                                case EMenuItemAction.ToMainMenu:
                                    return EMenuItemAction.ToMainMenu;
                                case EMenuItemAction.ToExit:
                                    return EMenuItemAction.ToExit;
                            }
                            continue;
                        }
                    }
                    
                    isFirst = BattleShipLogic.PlaceBomb(playerList[isFirst ? 0 : 1].CursorPosition.Item1, 
                       playerList[isFirst ? 0 : 1].CursorPosition.Item2) ? isFirst : !isFirst;
                }
                else isFirst = BattleShipLogic.PlaceBomb() ? isFirst : !isFirst;
                
                if (lastView == isFirst)
                {
                    message = "It's A Hit! A Little Bit More and We Won!";
                    SoundUtils.PlayMusic("Explosion.wav");
                    view.RunView(BattleShipLogic.GetPlayerBoard(true, true),
                        BattleShipLogic.GetPlayerBoard(true, false), false, message);
                }
                else
                {
                    message = "It's Miss! Here is Nothing My General!";
                    SoundUtils.PlayMusic("Splash.wav");
                    view.RunView(BattleShipLogic.GetPlayerBoard(false, true),
                        BattleShipLogic.GetPlayerBoard(false, false), false, message);
                }

                if (BattleShipLogic.IsFinished) break;

            } while (true);

            return EMenuItemAction.Continue;
        }
        
        
        /// <summary>
        /// Displays The End Game Results.
        /// </summary>
        private static void EndGame()
        {
            var isPlayerA = BattleShipLogic!.IsPlayerAWon();
            var winnerName = BattleShipLogic.WinnerPlayerName();
            var playerABoard = BattleShipLogic.PLayerOne?.GetCopiedBoard(false);
            var playerBBoard = BattleShipLogic.PlayerTwo?.GetCopiedBoard(false);

            GameResultsView.RunView(playerBBoard!, playerABoard!, winnerName, isPlayerA);
        }
        
        
        /// <summary>
        /// Battleship Game Main Menu Initialization.
        /// </summary>
        private Menu? InGameMenuRun()
        {
            SF.GameHeader();

            var mainMenu = new Menu(" In Game Menu ", EMenuType.Selection, EMenuLevel.SecondOrMore);

            mainMenu.AddMenuItems(new List<IMenuItem>
            {
                new MenuItem<EActionType>("Save Game To Local", () =>
                {
                    SaveGame(ESaveType.JSON);
                    return EActionType.SaveGame;
                }),
                new MenuItem<EActionType>("Save Game To Database", () =>
                {
                    SaveGame(ESaveType.Database);
                    return EActionType.SaveGame;
                })
            });
            
            // Remove Exit.
            mainMenu.MenuItems.RemoveAt(mainMenu.MenuItems.Count - 1);

            return new MenuController(mainMenu).Run();
        }
        
        
        /// <summary>
        /// Perform Setting Save to The Needed Saving Source.
        /// </summary>
        /// <param name="saveType">Indicator of Save Type.</param>
        /// <exception cref="ArgumentException">Thrown if Not Supported Yet Saving Method Executed.</exception>
        private void SaveGame(ESaveType saveType)
        {
            string? saveName;

            do
            {
                saveName = TypeBoxItem.DisplayBox(14, 30);

            } while (string.IsNullOrEmpty(saveName));
            
            switch (saveType)
            {
                case ESaveType.JSON:
                    BattleShipRepository.SaveToLocal(BattleShipLogic!, saveName);
                    break;
                case ESaveType.Database:
                    BattleShipRepository.SaveToDatabase(BattleShipLogic!, saveName);
                    break;
                default:
                    throw new ArgumentException("Not Implemented Saving Type.");
            }
        }
    }
}