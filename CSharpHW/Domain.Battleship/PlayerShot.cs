using System;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game Player Shot Entity.
    /// </summary>
    public class PlayerShot
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        public Guid PlayerShotId { get; set; }
        
        /// <summary>
        /// Stores Serial Number of The Shot to Preserve Order.
        /// </summary>
        public int ShotNumber { get; init; }
        
        /// <summary>
        /// Stores Reference to the Player whose Shot Is.
        /// </summary>
        public Guid PlayerId { get; set; }
        public Player? Player { get; set; }
        
        /// <summary>
        /// Stores Reference to the Coordinate which Is Used.
        /// </summary>
        public Guid CoordinateId { get; set; }
        public Coordinate? Coordinate { get; init; }
    }
}