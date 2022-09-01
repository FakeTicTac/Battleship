using System;


namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer BattleShip Object.
    /// </summary>
    public class BattleShip
    {
        
        /// <summary>
        /// Entity ID in Database.
        /// </summary>
        public Guid BattleShipId { get; init; }
        
        /// <summary>
        /// Stores Saving Name of The Game.
        /// </summary>
        public string? Name { get; init; }
        
        /// <summary>
        /// Stores Indicator of Game End State.
        /// </summary>
        public bool IsFinished { get; init; }

        /// <summary>
        /// Stores Game Save Time.
        /// </summary>
        public DateTime SavedAt { get; init; } = DateTime.Now;
        
        /// <summary>
        /// Stores Reference to the BattleShip Game Settings.
        /// </summary>
        public Settings? Settings { get; init; }
        
        /// <summary>
        /// Stores Reference to the First InGame Player.
        /// </summary>
        public Player? PlayerOne { get; init; }
        
        /// <summary>
        /// Stores Reference to the Second InGame Player.
        /// </summary>
        public Player? PlayerTwo { get; init; }
    }
}