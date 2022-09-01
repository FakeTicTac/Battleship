

namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer Ship Object.
    /// </summary>
    public class Ship
    {
        
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
    }
}