

namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer StrategyCoordinate Object.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class StrategyCoordinate
    {
        
        /// <summary>
        /// Stores Type of Strategy Coordinate Type.
        /// </summary>
        public int StrategyCoordinateType { get; init; }
        
        /// <summary>
        /// Stores Reference to the Coordinate which Is Used.
        /// </summary>
        public Coordinate? Coordinate { get; init; }
    }
}