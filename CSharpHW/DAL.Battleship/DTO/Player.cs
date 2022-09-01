using System.Collections.Generic;


namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer Player Object.
    /// </summary>
    public class Player
    {
        
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
        public Strategy? Strategy { get; init; }
        
        /// <summary>
        /// Stores Reference to the Players Own Board.
        /// </summary>
        public GameBoard? OwnBoard { get; init; }
        
        /// <summary>
        /// Stores Reference to the Players Firing Board.
        /// </summary>
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