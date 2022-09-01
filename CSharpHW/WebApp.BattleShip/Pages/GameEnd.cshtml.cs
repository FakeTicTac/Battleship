using System;
using DAL.Battleship;
using BLL.Battleship.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.BattleShip.Pages
{
    
    /// <summary>
    /// Game End Page: Represents Game End View.
    /// </summary>
    public class GameEnd : PageModel
    {
        
        /// <summary>
        /// Repository Connection for Loading Game State.
        /// </summary>
        private static BattleShipRepository Repository { get; set; } = new(
            new AppDbContext(), AppContext.BaseDirectory.Replace(@"bin\Debug\net5.0\", "") + @"State\");

        /// <summary>
        /// Game State.
        /// </summary>
        public BLL.Battleship.BattleShip BattleShip { get; } = Repository.GetAllSavesLocal()[0];
    }
}