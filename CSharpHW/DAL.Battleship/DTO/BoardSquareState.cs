

namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer BoardSquareState Object.
    /// </summary>
    public class BoardSquareState
    {
        
        /// <summary>
        /// Stores Indicator of Ship Allocation on the Cell.
        /// </summary>
        public bool IsShip { get; init; }
        
        /// <summary>
        /// Stores Indicator of Bomb Allocation on the Cell.
        /// </summary>
        public bool IsBomb { get; init; }
        
        /// <summary>
        /// Stores X Coordinate Position of the Cell on the Board.
        /// </summary>
        public int XCoordinate { get; init; }
        
        /// <summary>
        /// Stores Y Coordinate Position of the Cell on the Board.
        /// </summary>
        public int YCoordinate { get; init; }
        
        /// <summary>
        /// **JSON Serialized String** Stores List of Ships Serial Numbers that Reserved this Cell.
        /// </summary>
        public string? ReservedByShips { get; init; }
    }
}