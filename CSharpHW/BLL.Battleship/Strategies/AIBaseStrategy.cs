using System;
using System.Linq;
using BLL.Battleship.Interfaces;
using System.Collections.Generic;


namespace BLL.Battleship.Strategies
{
    
    /// <summary>
    /// Implementation of Base AI and It's Decisions During the Game Process.
    /// </summary>
    public class AIBaseStrategy : IStrategy
    {
        
        /// <summary>
        /// X Coordinate Size of the Board.
        /// </summary>
        public int XSize { get; init; }
        
        
        /// <summary>
        /// Y Coordinate Size of the Board.
        /// </summary>
        public int YSize { get; init; }
        
        
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
        public List<(int, int)>? AvailableCells { get; init; }

        
        /// <summary>
        /// Basic Parameterless AI Constructor.
        /// </summary>
        protected AIBaseStrategy() {}
        
        
        /// <summary>
        /// Basic AI Constructor. Defines Available Cells for Hitting.
        /// </summary>
        /// <param name="xSize">X Coordinate Board Size.</param>
        /// <param name="ySize">Y Coordinate Board Size.</param>
        protected AIBaseStrategy(int xSize, int ySize)
        {
            HuntingHits = new List<(int, int)>();
            HuntingCells = new List<(int, int)>();
            AvailableCells = new List<(int, int)>();
            HuntingMisses = new List<(int, int)>();

            XSize = xSize;
            YSize = ySize;
            
            AvailableCellGenerator(xSize, ySize);
        }

        
        /// <summary>
        /// Generator of Available Cells to Hit.
        /// </summary>
        /// <param name="xSize">X Coordinate Board Size.</param>
        /// <param name="ySize">Y Coordinate Board Size.</param>
        private void AvailableCellGenerator(int xSize, int ySize)
        {
            for (var y = 0; y < ySize; y++)
            {
                for (var x = 0; x < xSize; x++) AvailableCells!.Add((x, y));
            }
        }

        
        /// <summary>
        /// Check if Cell Should be Hunted and Adds Hunting Cell to List.
        /// </summary>
        protected void IsHuntingCell(int xCoordinate, int yCoordinate)
        {
            if (xCoordinate < 0 || yCoordinate < 0 || xCoordinate > XSize || yCoordinate > YSize) return;

            if (!AvailableCells!.Contains((xCoordinate, yCoordinate))) return;

            if (HuntingCells!.Any(x => x.Item1 == xCoordinate && x.Item2 == yCoordinate)) return;

            if (HuntingMisses!.Any(x => x.Item1 == xCoordinate || x.Item2 == yCoordinate)) return;
            
            HuntingCells!.Add((xCoordinate, yCoordinate));
        }
        

        /// <summary>
        /// AI Chooses Position for Bomb to be Placed.
        /// </summary>
        /// <returns>X and Y Coordinate Position for Bomb Placemen.</returns>
        public virtual (int, int) AIShootPosition() => AvailableCells![new Random().Next(AvailableCells.Count)];


        /// <summary>
        /// AI Analyzes Shoot Data to Make Future Shoots.
        /// </summary>
        /// <param name="states">States of the Board that Were Affected by Shoot.</param>
        public virtual void AIShootAnalyze(List<BoardSquareState> states) =>
                                states.ForEach(x => AvailableCells!.Remove((x.XCoordinate, x.YCoordinate)));
    }
}