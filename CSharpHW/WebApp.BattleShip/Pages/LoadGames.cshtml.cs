using System;
using DAL.Battleship;
using Microsoft.AspNetCore.Mvc;
using BLL.Battleship.Repository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.BattleShip.Pages
{
    
    /// <summary>
    /// Load Games Page: Represents Load Games View.
    /// </summary>
    public class LoadGames : PageModel
    {
        
        /// <summary>
        /// Repository Connection for Loading Games.
        /// </summary>
        private static BattleShipRepository Repository { get; } = new(
            new AppDbContext(), AppContext.BaseDirectory.Replace(@"bin\Debug\net5.0\", "") + @"State\");

        
        /// <summary>
        /// All BattleShip Saves From Database.
        /// </summary>
        public List<BLL.Battleship.BattleShip> Games { get; } = Repository.GetAllSavesDatabase();

        
        /// <summary>
        /// Load Game from Database and Run the Game.
        /// </summary>
        /// <returns>View Redirection Based on Saving Stage.</returns>
        public RedirectToPageResult? OnPostGameLoading()
        {
            try
            {
                var uuid = Guid.Parse(Request.Form["GameId"]);
                var save = Repository.GetSaveDatabase(uuid);
                
                Repository.SaveToLocal(save, "state");
                return RedirectToPage("ShipPlacement", "LoadGame");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}