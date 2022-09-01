using System;
using DAL.Battleship;
using Microsoft.AspNetCore.Mvc;
using BLL.Battleship.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.BattleShip.Pages
{
    
    /// <summary>
    /// Load Settings Page: Represents Load Settings View.
    /// </summary>
    public class LoadSettings : PageModel
    {
        
        /// <summary>
        /// Repository Connection for Loading Games.
        /// </summary>
        private static SettingsRepository SettingsRepository { get; } = new(new AppDbContext(), "");
        
        
        /// <summary>
        /// BattleShip Game State Saving Repository.
        /// </summary>
        private static BattleShipRepository BattleshipRepository { get; } = new(
            new AppDbContext(), AppContext.BaseDirectory.Replace(@"bin\Debug\net5.0\", "") + @"State\");

        
        /// <summary>
        /// All Settings Saves From Database.
        /// </summary>
        public List<BLL.Battleship.Settings> SettingsList { get; } = SettingsRepository.GetAllSavesDatabase();

        
        /// <summary>
        /// Load Settings from Database and Run the Game.
        /// </summary>
        /// <returns>Ship Placement View Redirection.</returns>
        public RedirectToPageResult? OnPostSettingsLoading()
        {
            try
            {
                var uuid = Guid.Parse(Request.Form["SettingsId"]);
                var saveSettings = SettingsRepository.GetSaveDatabase(uuid);
                
                var bs = new BLL.Battleship.BattleShip(null, null, saveSettings);
                
                BattleshipRepository.SaveToLocal(bs, "state");
                return RedirectToPage("ShipPlacement", "LoadGame");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}