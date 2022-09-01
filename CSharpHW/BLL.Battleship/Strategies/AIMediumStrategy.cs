using System;
using System.Collections.Generic;


namespace BLL.Battleship.Strategies
{
    
    /// <summary>
    /// Implementation of Medium AI and It's Decisions During the Game Process.
    /// </summary>
    public class AIMediumStrategy : AIBaseStrategy
    {
        
        /// <summary>
        /// Basic Medium AI Constructor. Defines Available Cells for Hitting.
        /// </summary>
        /// <param name="xSize">X Coordinate Board Size.</param>
        /// <param name="ySize">Y Coordinate Board Size.</param>
        public AIMediumStrategy(int xSize, int ySize) : base(xSize, ySize) { }


        public AIMediumStrategy()
        {
        }

        /// <summary>
        /// AI Chooses Position for Bomb to be Placed.
        /// </summary>
        /// <returns>X and Y Coordinate Position for Bomb Placemen.</returns>
        public override (int, int) AIShootPosition()
        {
            if (HuntingCells!.Count == 0) return base.AIShootPosition();

            var shotPosition = HuntingCells[new Random().Next(HuntingCells.Count)];
            HuntingCells.Remove(shotPosition);
            
            return shotPosition;
        }

        
        /// <summary>
        /// AI Analyzes Shoot Data to Make Future Shoots.
        /// </summary>
        /// <param name="states">States of the Board that Were Affected by Shoot.</param>
        public override void AIShootAnalyze(List<BoardSquareState> states)
        {
            base.AIShootAnalyze(states);
            
            if (states.Count > 1)
            {
                HuntingCells = new List<(int, int)>();
                return;
            }

            var shotCell = states[0];
            if (!shotCell.IsShip) return;
            
            IsHuntingCell(shotCell.XCoordinate - 1, shotCell.YCoordinate);
            IsHuntingCell(shotCell.XCoordinate + 1, shotCell.YCoordinate);
            IsHuntingCell(shotCell.XCoordinate, shotCell.YCoordinate - 1);
            IsHuntingCell(shotCell.XCoordinate, shotCell.YCoordinate + 1);
        }
    }
}