using System.Collections.Generic;


namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer Strategy Object.
    /// </summary>
    public class Strategy
    {

        /// <summary>
        /// Stores X Coordinate Size of the Board.
        /// </summary>
        public int XSize { get; init; }
        
        /// <summary>
        /// Stores Y Coordinate Size of the Board.
        /// </summary>
        public int YSize { get; init; }

        /// <summary>
        /// Stores Coordinates that Are Used in Strategy..
        /// </summary>
        public List<StrategyCoordinate>? StrategyCoordinates { get; init; }
    }
}