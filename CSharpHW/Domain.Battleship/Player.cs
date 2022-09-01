using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game Player Entity.
    /// </summary>
    public class Player
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Stores Nickname of The Player.
        /// </summary>
        public string? Name { get; init; }
        
        /// <summary>
        /// Stores Indicator of Players' Turn.
        /// </summary>
        public bool IsTurn { get; init; }
        
        /// <summary>
        /// Stores Indicator of Players' Loss.
        /// </summary>
        public bool IsLost { get; init; }

        /// <summary>
        /// Stores Indicator of Ship Placement Process Completion.
        /// </summary>
        public bool IsPlacementCompleted { get; init; }
        
        /// <summary>
        /// Stores Indicator of the Players' Type.
        /// </summary>
        public int PlayerType { get; init; }
        
        /// <summary>
        /// Stores Reference to the Players Strategy.
        /// </summary>
        public Guid? StrategyId { get; set; }
        public Strategy? Strategy { get; init; }
        
        /// <summary>
        /// Stores Reference to the Players Own Board.
        /// </summary>
        [ForeignKey("OwnBoard")]
        public Guid OwnBoardId { get; set; }
        public GameBoard? OwnBoard { get; init; }
        
        /// <summary>
        /// Stores Reference to the Players Firing Board.
        /// </summary>
        [ForeignKey("FiringBoard")]
        public Guid FiringBoardId { get; set; }
        public GameBoard? FiringBoard { get; init; }

        /// <summary>
        /// Stores All Related to The Player Ships.
        /// </summary>
        public List<Ship>? Ships { get; init; } 
        
        /// <summary>
        /// Stores All Related to The Player Shots.
        /// </summary>
        public List<PlayerShot>? PlayerShots { get; init; }
    }
}