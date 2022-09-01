using System;
using System.Collections.Generic;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game Strategy Entity.
    /// </summary>
    public class Strategy
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        public Guid StrategyId { get; set; }
        
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