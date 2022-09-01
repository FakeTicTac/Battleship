using System;


namespace Domain.Battleship
{
    
    /// <summary>
    /// Class Defines BattleShip Game Board Square State Entity.
    /// </summary>
    public class BoardSquareState
    {
        
        /// <summary>
        /// Stores Entity ID.
        /// </summary>
        public Guid BoardSquareStateId { get; set; }
        
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
        
        /// <summary>
        /// Stores Reference to The Game Board. 
        /// </summary>
        public Guid GameBoardId { get; set; }
        public GameBoard? GameBoard { get; set; }
    }
}