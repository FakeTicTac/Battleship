using System;
using System.Collections.Generic;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game Coordinate Entity.
    /// </summary>
    public class Coordinate
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        public Guid CoordinateId { get; set; }
        
        /// <summary>
        /// Stores Coordinate X Value.
        /// </summary>
        public int xCoordinateValue { get; init; }
        
        /// <summary>
        /// Stores Coordinate Y Value.
        /// </summary>
        public int yCoordinateValue { get; init; }
        
        /// <summary>
        /// Stores Coordinates that Are Used In Strategy.
        /// </summary>
        public List<StrategyCoordinate>? StrategyCoordinates { get; set; }
    }
}