using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game BattleShip Entity.
    /// </summary>
    public class BattleShip
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid BattleShipId { get; set; }
        
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
        public Guid SettingsId { get; set; }
        public Settings? Settings { get; init; }
        
        /// <summary>
        /// Stores Reference to the First InGame Player.
        /// </summary>
        [ForeignKey("PlayerOne")]
        public Guid PlayerOneId { get; set; }
        public Player? PlayerOne { get; init; }
        
        /// <summary>
        /// Stores Reference to the Second InGame Player.
        /// </summary>
        [ForeignKey("PlayerTwo")]
        public Guid PlayerTwoId { get; set; }
        public Player? PlayerTwo { get; init; }
    }
}