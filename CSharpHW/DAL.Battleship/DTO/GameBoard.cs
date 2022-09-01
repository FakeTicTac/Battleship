using System.Collections.Generic;


namespace DAL.Battleship.DTO
{
    
    /// <summary>
    /// Class Defines BattleShip Game Data Access Layer GameBoard Object.
    /// </summary>
    public class GameBoard
    {
        
        /// <summary>
        /// Stores X Coordinate Size of the Board.
        /// </summary>
        public int XBoardSize { get; init; }
        
        /// <summary>
        /// Stores Y Coordinate Size of the Board.
        /// </summary>
        public int YBoardSize { get; init; }

        /// <summary>
        /// Stores Amount of Cells for Ships That Can This Board Handle.
        /// </summary>
        public int BoardShipCapacity { get; init; }
        
        /// <summary>
        /// Stores All Related to The Board Cells.
        /// </summary>
        public List<BoardSquareState>? BoardSquareStates { get; init; }
    }
}