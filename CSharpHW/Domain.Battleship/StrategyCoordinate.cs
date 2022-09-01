using System;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Strategy Coordinate State Entity.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class StrategyCoordinate
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        public Guid StrategyCoordinateId { get; set; }
        
        /// <summary>
        /// Stores Type of Strategy Coordinate Type.
        /// </summary>
        public int StrategyCoordinateType { get; init; }
        
        /// <summary>
        /// Stores Reference to the Strategy which Uses Coordinate.
        /// </summary>
        public Guid StrategyId { get; set; }
        public Strategy? Strategy { get; set; }
        
        /// <summary>
        /// Stores Reference to the Coordinate which Is Used.
        /// </summary>
        public Guid CoordinateId { get; set; }
        public Coordinate? Coordinate { get; init; }
    }
}