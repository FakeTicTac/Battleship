using System;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game Ship Entity.
    /// </summary>
    public class Ship
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        public Guid ShipId { get; set; }
        
        /// <summary>
        /// Stores Ship X Coordinate Lenght.
        /// </summary>
        public int XSize { get; init; }
        
        /// <summary>
        /// Stores Ship Y Coordinate Lenght.
        /// </summary>
        public int YSize { get; init; }
        
        /// <summary>
        /// Stores Ship Health.
        /// </summary>
        public int Health { get; init; }
        
        /// <summary>
        /// Stores Indicator of Ship Allocation on The Board.
        /// </summary>
        public bool IsPlaced { get; init; }
        
        /// <summary>
        /// Stores Ships Name.
        /// </summary>
        public string? Name { get; init; }
        
        /// <summary>
        /// Stores Ships' Serial Number.
        /// </summary>
        public int ShipNumber { get; init;}
        
        /// <summary>
        /// Stores Reference to the Player which Ship is.
        /// </summary>
        public Guid PlayerId { get; set; }
        public Player? Player { get; set; }
    }
}