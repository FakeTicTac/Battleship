using System;
using System.Collections.Generic;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game Setting Entity.
    /// </summary>
    public class Settings
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Guid SettingsId { get; set; }

        /// <summary>
        /// Stores Representation of The AIs' Difficulty Level.
        /// </summary>
        public int? AiLevel { get; init; }
        
        /// <summary>
        /// Stores Representation of Game Mode.
        /// </summary>
        public int GameMode { get; init; }
        
        /// <summary>
        /// Stores Representation of Game Touching Rule.
        /// </summary>
        public int ShipTouchRule { get; init; }
        
        /// <summary>
        /// Stores Representation of Game Ship Usage Rule.
        /// </summary>
        public int ShipUsageRule { get; init; }
        
        /// <summary>
        /// Stores Representation of the Setting Save Name.
        /// </summary>
        public string? Name { get; init; } 

        /// <summary>
        /// Stores Indicator of InGame Setting of Manually Created and Saved Setting.
        /// </summary>
        public bool IsManualCreation { get; init; }
        
        /// <summary>
        /// Stores Game Board X Size.
        /// </summary>
        public int BoardXSize { get; init; }

        /// <summary>
        /// Stores Game Board Y Size.
        /// </summary>
        public int BoardYSize { get; init; }

        /// <summary>
        /// Stores Setting Save Time.
        /// </summary>
        public DateTime SavedAt { get; init; } = DateTime.Now;
        
        /// <summary>
        /// Stores All Related to Setting Games.
        /// </summary>
        public List<BattleShip>? BattleShips { get; set; }
    }
}