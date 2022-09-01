using System;
using System.Linq;
using System.Collections.Generic;


namespace BLL.Battleship.Strategies
{
    
    /// <summary>
    /// Implementation of Hard AI and It's Decisions During the Game Process.
    /// </summary>
    public class AIHardStrategy : AIBaseStrategy
    {
      
        /// <summary>
        /// Basic Hard AI Constructor. Defines Available Cells for Hitting.
        /// </summary>
        /// <param name="xSize">X Coordinate Board Size.</param>
        /// <param name="ySize">Y Coordinate Board Size.</param>
        public AIHardStrategy(int xSize, int ySize) : base(xSize, ySize) { }
        
        
        public AIHardStrategy() {}

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
                HuntingMisses = new List<(int, int)>();
                HuntingCells = new List<(int, int)>();
                HuntingHits = new List<(int, int)>();
                return;
            }

            var shotCell = states[0];
            
            if (!shotCell.IsShip)
            {
                HitBorderAnalyzer(shotCell.XCoordinate, shotCell.YCoordinate);
                
                HuntingCells!.RemoveAll(x => HuntingMisses!.Any(y => y.Item2 == x.Item2));
                HuntingCells.RemoveAll(x => HuntingMisses!.Any(y => y.Item1 == x.Item1));
                return;
            }

            HuntingHits!.Add((shotCell.XCoordinate, shotCell.YCoordinate));
            
            IsHuntingCell(shotCell.XCoordinate - 1, shotCell.YCoordinate);
            IsHuntingCell(shotCell.XCoordinate + 1, shotCell.YCoordinate);
            IsHuntingCell(shotCell.XCoordinate, shotCell.YCoordinate - 1);
            IsHuntingCell(shotCell.XCoordinate, shotCell.YCoordinate + 1);
        }

        
        /// <summary>
        /// Analyzes Ship Predictable Boards.
        /// </summary>
        /// <param name="xCoordinate">Miss X Coordinate Position.</param>
        /// <param name="yCoordinate">Miss Y Coordinate Position.</param>
        private void HitBorderAnalyzer(int xCoordinate, int yCoordinate)
        {
            var xHunting = HuntingHits!.FindAll(x => x.Item1 == xCoordinate);
            var yHunting = HuntingHits.FindAll(y => y.Item2 == yCoordinate);
            
            var isYMax = xHunting.Count != 0 ? xHunting.Max(y => y.Item2) : -1;
            var isYMin = xHunting.Count != 0 ? xHunting.Min(y => y.Item2) : -1;
            var isXMax = yHunting.Count != 0 ? yHunting.Max(x => x.Item1) : -1;
            var isXMin = yHunting.Count != 0 ? yHunting.Min(x => x.Item1) : -1;
            
            if (isYMax != -1 && (isYMax < yCoordinate || isYMin > yCoordinate)) HuntingMisses!.Add((-1, yCoordinate));
            if (isXMax != -1 && (isXMax < xCoordinate || isXMin > xCoordinate)) HuntingMisses!.Add((xCoordinate, -1));
        }
    }
}