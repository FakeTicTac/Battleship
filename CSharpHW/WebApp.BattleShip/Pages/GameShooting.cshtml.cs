using System;
using DAL.Battleship;
using Microsoft.AspNetCore.Mvc;
using BLL.Battleship.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.BattleShip.Pages
{
    
    /// <summary>
    /// Shooting Page: Represents Shooting View.
    /// </summary>
    public class GameShooting : PageModel
    {
        
        /// <summary>
        /// Repository Connection for Loading Games.
        /// </summary>
        private static BattleShipRepository Repository { get; } = new(
            new AppDbContext(), 
            AppContext.BaseDirectory.Replace(@"bin\Debug\net5.0\", "") + @"State\");

        
        /// <summary>
        /// Game State Loading.
        /// </summary>
        public BLL.Battleship.BattleShip BattleShip { get; } = Repository.GetAllSavesLocal()[0];


        /// <summary>
        /// Indicator of Own Board Show.
        /// </summary>
        public bool ShowHomeBoard { get; private set; } 


        /// <summary>
        /// Show Players Own Board.
        /// </summary>
        public void OnPostShowHomeBoard()
        {
            switch (Request.Form["IsShowBoard"])
            {
                case "False":
                    ShowHomeBoard = true;
                    break;
            }
        }
        
        
        /// <summary>
        /// Save Game State to Database.
        /// </summary>
        public void OnPostSaveGame() => Repository.SaveToDatabase(BattleShip, Request.Form["SaveName"]);
        

        /// <summary>
        /// Rewind Players Moves.
        /// </summary>
        public void OnPostRewind()
        {
            BattleShip.DoubleRewind();
            Repository.SaveToLocal(BattleShip, "state");
        }

        
        /// <summary>
        /// Shoot on the Opponents Board.
        /// </summary>
        /// <returns>Redirects to Game End or Shooting View.</returns>
        public RedirectToPageResult? OnPostShoot()
        {
            try
            {
                BattleShip.PlaceBomb(
                    int.Parse(Request.Form["xCor"]), 
                    int.Parse(Request.Form["yCor"]));
            }
            catch (Exception)
            {
                return null;
            }
            
            if (!BattleShip.PlayerByTurn().isHuman())
            {
                while (!BattleShip.PlayerByTurn().isHuman())
                {
                    BattleShip.PlaceBomb();
                }
            }
            
            Repository.SaveToLocal(BattleShip, "state");
            
            return BattleShip.IsFinished ? RedirectToPage("GameEnd") : null;
        }

        
        /// <summary>
        /// Exit from Game to Main Menu.
        /// </summary>
        /// <returns>Redirect to Main Menu.</returns>
        public RedirectToPageResult OnPostToMainMenu() => RedirectToPage("MainMenu");
    }
}