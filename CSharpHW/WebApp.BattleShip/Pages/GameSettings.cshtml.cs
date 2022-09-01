using System;
using BLL.Menu;
using DAL.Battleship;
using BLL.Battleship;
using BLL.Battleship.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApp.BattleShip.Initializers;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SettingsRepository = BLL.Battleship.Repository.SettingsRepository;
using BattleShipRepository = BLL.Battleship.Repository.BattleShipRepository;


namespace WebApp.BattleShip.Pages
{
    
    /// <summary>
    /// Game Settings Page: Represents Game Settings View.
    /// </summary>
    public class GameSettings : PageModel
    {
        
        /// <summary>
        /// Setting Menu Initialization.
        /// </summary>
        public Menu SettingMenu { get; } = MenuInitializers.SettingMenuInitialize();

        
        /// <summary>
        /// Repository Connection for Loading Games.
        /// </summary>
        private BattleShipRepository BattleShipRepository { get; } = new(
            new AppDbContext(), AppContext.BaseDirectory.Replace(@"bin\Debug\net5.0\", "") + @"State\");
        
        
        /// <summary>
        /// Repository Connection for Loading Settings.
        /// </summary>
        private static SettingsRepository SettingsRepository { get; } = new(new AppDbContext(), "");
        

        /// <summary>
        /// Method Collects Settings Options from View and Creates/Saves Settings.
        /// </summary>
        /// <returns>Redirects to Ship Placement View.</returns>
        public RedirectToPageResult OnPostReceiveGameSettings()
        {
            
            var firstPlayerName = Request.Form["FirstPlayerName"];
            var secondPlayerName = Request.Form["SecondPlayerName"];

            var settings = new Settings();

            foreach (var (key, value) in Request.Form)
            {
                switch (key)
                {
                    case "Game Mode":
                        settings.GameMode = value == "Single Player" ? EGameMode.SinglePlayer : EGameMode.MultiPlayer;
                        break;
                    case "AI Level":
                        if (settings.GameMode == EGameMode.MultiPlayer)
                        {
                            settings.AILevel = null;
                            break;
                        }
                        
                        switch (value)
                        {
                            case "Medium":
                                settings.AILevel = EAILevel.Medium;
                                break;
                            case "Hard":
                                settings.AILevel = EAILevel.Hard;
                                break;
                            default:
                                settings.AILevel = EAILevel.Easy;
                                break;
                        }
                        break;
                    case "Board Width":
                        try
                        {
                            settings.XBoardSize = int.Parse(value);
                        }
                        catch (Exception)
                        {
                            settings.XBoardSize = 10;
                        }
                        break;
                    case "Board Lenght":
                        try
                        {
                            settings.YBoardSize = int.Parse(value);
                        }
                        catch (Exception)
                        {
                            settings.YBoardSize = 10;
                        }
                        break;
                    case "Ship Usage Mode":
                        settings.ShipUsageRule = EShipUsageRule.DifferentShips;
                        break;
                    case "Ship Collision Mode":
                        if (value == "Ship Corners Can Touch Mode")
                            settings.ShipTouchRule = EShipTouchRule.CornersCanTouch;
                        else if (value == "Ship All Sides Can Touch Player")
                            settings.ShipTouchRule = EShipTouchRule.SidesCanTouch;
                        else
                            settings.ShipTouchRule = EShipTouchRule.NoTouching;
                        break;
                }
            }

            if (Request.Form.ContainsKey("SaveSettingsFlag"))
            {
                settings.IsManual = true;
                SettingsRepository.SaveToDatabase(settings, Request.Form["SettingsSaveName"]);
            }
            
            var bl = new BLL.Battleship.BattleShip(firstPlayerName, secondPlayerName, settings);

            BattleShipRepository.SaveToLocal(bl, "state");

            return RedirectToPage("ShipPlacement");
        }
    }
}