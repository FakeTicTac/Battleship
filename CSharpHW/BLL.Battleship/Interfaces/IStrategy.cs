using System.Collections.Generic;


namespace BLL.Battleship.Interfaces
{
    
    /// <summary>
    /// Basic Implementation of Game Strategy.
    /// Strategy Holds Players Moves During the Game Process.
    /// </summary>
    public interface IStrategy
    {
        
        /// <summary>
        /// X Coordinate Size of the Board.
        /// </summary>
        public int XSize { get; }
        
        
        /// <summary>
        /// Y Coordinate Size of the Board.
        /// </summary>
        public int YSize { get; }
        
        
        /// <summary>
        /// List of Cells that Were Successfully Hit During Hunting Stage.
        /// </summary>
        public List<(int, int)>? HuntingHits { get; set; }
        
        
        /// <summary>
        /// List of Cells that Were Missed During Hunting Stage.
        /// </summary>
        public List<(int, int)>? HuntingMisses { get; set; }
        
        
        /// <summary>
        /// List of Cells that Considered Holding Ship.
        /// </summary>
        public List<(int, int)>? HuntingCells { get; set; }
        
        
        /// <summary>
        /// List of Cells Available for Hit on the Board.
        /// </summary>
        public List<(int, int)>? AvailableCells { get; }
        
        
        /// <summary>
        /// AI Chooses Position for Bomb to be Placed.
        /// </summary>
        /// <returns>X and Y Coordinate Position for Bomb Placemen.</returns>
        public (int, int) AIShootPosition();

        
        /// <summary>
        /// AI Analyzes Shoot Data to Make Future Shoots.
        /// </summary>
        /// <param name="states">States of the Board that Were Affected by Shoot.</param>
        public void AIShootAnalyze(List<BoardSquareState> states);
    }
}