

namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer Player Shot Object.
    /// </summary>
    public class PlayerShot
    {
        
        /// <summary>
        /// Stores Serial Number of The Shot to Preserve Order.
        /// </summary>
        public int ShotNumber { get; init; }
        
        
        /// <summary>
        /// Stores Reference to the Coordinate which Is Used.
        /// </summary>
        public Coordinate? Coordinate { get; init; }
    }
}