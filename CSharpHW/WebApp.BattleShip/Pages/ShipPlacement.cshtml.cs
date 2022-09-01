using System;
using DAL.Battleship;
using BLL.Battleship.Enums;
using Microsoft.AspNetCore.Mvc;
using BLL.Battleship.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.BattleShip.Pages
{
    
    /// <summary>
    /// Ship Placement Page: Represents Ship Placement View.
    /// </summary>
    public class ShipPlacement : PageModel
    {
        
        /// <summary>
        /// Repository Connection for Loading Games.
        /// </summary>
        private static BattleShipRepository Repository { get; } = new(
            new AppDbContext(), AppContext.BaseDirectory.Replace(@"bin\Debug\net5.0\", "") + @"State\");

        
        /// <summary>
        /// Game State Loading.
        /// </summary>
        public BLL.Battleship.BattleShip BattleShip { get; } = Repository.GetAllSavesLocal()[0];

        
        /// <summary>
        /// Message to be Displayed in Message Box.
        /// </summary>
        public string Message { get; private set; } = "Please, Place Your Ships!";

        
        /// <summary>
        /// Load The Game from Database and Check It's Stage.
        /// </summary>
        /// <returns>Ship Placement or Shooting View.</returns>
        public RedirectToPageResult OnGetLoadGame()
        {
            if (!BattleShip.PlayerByTurn().IsPlacementCompleted) return RedirectToPage("ShipPlacement");
            
            BattleShip.SwitchTurns();

            return RedirectToPage(BattleShip.PlayerByTurn().IsPlacementCompleted ? "GameShooting" : "ShipPlacement");
        }

        
        /// <summary>
        /// Method Creates and Places Ship on the Players Board.
        /// </summary>
        public void OnPostCreateShip()
        {
            try
            {
                var canCreate = BattleShip.CreatePlayerShip(
                    Request.Form["ShipName"],
                    int.Parse(Request.Form["XSize"]),
                    int.Parse(Request.Form["YSize"])
                );
                
                if (!canCreate)
                {
                    Message = "Ship With Give Sizes Cannot Be Created on This Board.";
                    return;
                }
                
                var canPlace = BattleShip.PlacePlayerShip(BattleShip.PlayerByTurn().OwnShips!.Count - 1,
                    int.Parse(Request.Form["XCoord"]),
                    int.Parse(Request.Form["YCoord"])
                );

                if (!canPlace)
                {
                    Message = "Ship Cannot Be Placed on The Given Location!.";
                    return;
                }
            }
            catch (FormatException)
            {
                Message = "Stop DDOS-ing Our Game!";
                return;
            }
            
            Repository.SaveToLocal(BattleShip, "state");
        }

        
        /// <summary>
        /// Finish Ship Placement for Player.
        /// </summary>
        /// <returns>Ship Placement View for Second Player or Shooting View.</returns>
        public RedirectToPageResult? OnPostFinishPlacement()
        {
            if (!BattleShip.IsPlayerHaveEnoughShips())
            {
                Message = "You Have to Place at Least 1 Ship on the Board!";
                return null;
            }
            
            BattleShip.PlayerByTurn().IsPlacementCompleted = true;
            BattleShip.SwitchTurns();

            if (BattleShip.Settings!.GameMode == EGameMode.SinglePlayer)
            {
                BattleShip.AICreatePlaceShip();
                BattleShip.PlayerByTurn().IsPlacementCompleted = true;
                BattleShip.SwitchTurns();
                Repository.SaveToLocal(BattleShip, "state");
                return RedirectToPage("GameShooting");
            }
            
            Repository.SaveToLocal(BattleShip, "state");

            return RedirectToPage(BattleShip.PlayerByTurn().IsPlacementCompleted ? "GameShooting" : "ShipPlacement");
        }

        
        /// <summary>
        /// Method Saves Game State in Database.
        /// </summary>
        public void OnPostSaveGame()
        {
            Repository.SaveToDatabase(BattleShip, Request.Form["SaveName"]);
        }

        
        /// <summary>
        /// Method Places Random Ships on the Board.
        /// </summary>
        public void OnPostRandomShips()
        {
            BattleShip.AICreatePlaceShip();
            Repository.SaveToLocal(BattleShip, "state");
        }

        
        /// <summary>
        /// Return To Game Settings Page.
        /// </summary>
        /// <returns></returns>
        public RedirectToPageResult OnPostToGameSettings()
        {
            return RedirectToPage("GameSettings");
        }

        
        /// <summary>
        /// Method Deletes All Ships From the Board.
        /// </summary>
        public void OnPostDeleteAll()
        {
            BattleShip.DeleteAllShips();
            Repository.SaveToLocal(BattleShip, "state");
        }

        
        /// <summary>
        /// Method Deletes Last Ship From the Board.
        /// </summary>
        public void OnPostDeleteLast()
        {
            BattleShip.DeleteLastShip();
            Repository.SaveToLocal(BattleShip, "state");
        }
    }
}