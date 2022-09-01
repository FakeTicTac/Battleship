using System;
using BLL.Battleship.Enums;
using System.Collections.Generic;


namespace BLL.Battleship
{
    
    /// <summary>
    /// Represents State of the Game Board.
    /// </summary>
    public class GameBoard
    {

        /// <summary>
        /// Size of the Board on X Coordinate.
        /// </summary>
        public int XBoardSize { get; init; }
        
        
        /// <summary>
        /// Size of the Board on Y Coordinate.
        /// </summary>
        public int YBoardSize { get; init; }
        
        
        /// <summary>
        /// Indicates The Amount of Ships' Cells That Can this Board Handle.
        /// </summary>
        public int BoardShipCapacity { get; set; }
        

        /// <summary>
        /// Indicates The Board State.
        /// </summary>
        public BoardSquareState[,]? Board { get; init; } 
        
        
        /// <summary>
        /// Basic Parameterless GameBoard Constructor.
        /// </summary>
        public GameBoard() {}
        
        
        /// <summary>
        /// Basic Game Board Constructor. Defines Board and it's Sizes..
        /// </summary>
        /// <param name="xSize">X Coordinate Size for Board.</param>
        /// <param name="ySize">Y Coordinate Size for Board.</param>
        public GameBoard(int xSize, int ySize)
        {
            XBoardSize = xSize;
            YBoardSize = ySize;
            
            BoardShipCapacity = xSize * ySize / 4;
            Board = BoardInitialization();
        }
        
        
        /// <summary>
        /// Initializes Board. Creates an Array of Board Square States.
        /// </summary>
        /// <returns>Array of Board Square States.</returns>
        private BoardSquareState[,] BoardInitialization()
        {
            var board = new BoardSquareState[XBoardSize, YBoardSize];
                
            for (var y = 0; y < YBoardSize; y++)
            {
                for (var x = 0; x < XBoardSize; x++)  board[x, y] = new BoardSquareState(x, y);
            }

            return board;
        }


        /// <summary>
        /// Places the Bomb on the Board.
        /// </summary>
        /// <param name="xCoordinate">X Coordinate to Place Bomb.</param>
        /// <param name="yCoordinate">Y Coordinate to Place Bomb.</param>
        /// <param name="cellState">State of Cell to be Changed.</param>
        public void PlaceBomb(int xCoordinate, int yCoordinate, BoardSquareState cellState)
        {
            cellState.IsBomb = true;
            Board![xCoordinate, yCoordinate] = cellState;
        }
        
        
        /// <summary>
        /// Calculates Fitting of the Given Ship on the Board.
        /// </summary>
        /// <param name="xSize">X Coordinate Size of Ship.</param>
        /// <param name="ySize">Y Coordinate Size of Ship.</param>
        /// <returns>Indicator of Fitting.</returns>
        public bool CanShipFit(int xSize, int ySize) => xSize * ySize <= BoardShipCapacity && xSize < XBoardSize && ySize < YBoardSize;
        
        
        /// <summary>
        /// Calculate Reserved Cells Around the Sunk Ship.
        /// </summary>
        /// <param name="shipNumber">Ship Serial Number to Look for Reservation.</param>
        /// <returns>List of Reserved by Ship Cells.</returns>
        public List<BoardSquareState> CalculateShipSunkCells(int shipNumber)
        {
            var boardCopy = CopyBoard();
            var boardStates = new List<BoardSquareState>();
            
            for (var y = 0; y < YBoardSize; y++)
            {
                for (var x = 0; x < XBoardSize; x++)
                {
                    var boardState = boardCopy[x, y];
                    
                    if (boardState.IsReservedByShip(shipNumber)) boardStates.Add(boardState);
                }
            }

            return boardStates;
        }
        
        
        /// <summary>
        /// Remove Ship From the Board.
        /// </summary>
        /// <param name="shipNumber">Ship Number for Ship to be Removed.</param>
        public void EraseShip(int shipNumber)     
        {
            for (var y = 0; y < YBoardSize; y++)
            {
                for (var x = 0; x < XBoardSize; x++)
                {
                    if (!Board![x, y].IsReservedByShip(shipNumber)) continue;
                    
                    Board![x, y].ReservedByShips.Remove(shipNumber);
                    Board![x, y].IsShip = false;
                }
            }
        }
        
        
        /// <summary>
        /// Calculates if Ship Can be Placed on the Board in the Given Location.
        /// </summary>
        /// <param name="ship">Ship to Place.</param>
        /// <param name="xCoordinate">X Coordinate for Ship Placement.</param>
        /// <param name="yCoordinate">Y Coordinate for Ship Placement.</param>
        /// <returns>Indicator of Placing Success.</returns>
        public bool CanShipBeLocated(Ship ship, int xCoordinate, int yCoordinate)
        {
            var (xMaxSize, yMaxSize) = (xCoordinate + ship.GetSizes().Item1, yCoordinate + ship.GetSizes().Item2);

            if (xCoordinate < 0 || xMaxSize > XBoardSize || yCoordinate < 0 || yMaxSize > YBoardSize) return false;
            
            for (var y = yCoordinate; y < yMaxSize; y++)
            {
                for (var x = xCoordinate; x < xMaxSize; x++) if (Board![x, y].IsShip || Board[x, y].IsReserved()) return false;
            }

            return true;
        }


        /// <summary>
        /// Place Ship on the Board.
        /// </summary>
        /// <param name="ship">Ship to Place.</param>
        /// <param name="xCoordinate">X Coordinate to Place Ship.</param>
        /// <param name="yCoordinate">Y Coordinate to Place Ship.</param>
        /// <param name="shipTouchRule">Indicator of Ship Touching Rule for Ship Placement.</param>
        public void LocateShip(Ship ship, int xCoordinate, int yCoordinate, EShipTouchRule shipTouchRule)
        {
            var (xSize, ySize) = ship.GetSizes();
            var (xMaxSize, yMaxSize) = (xCoordinate + xSize, yCoordinate + ySize);

            ReserveCellsAroundShip(ship, xCoordinate, yCoordinate, shipTouchRule);
            
            for (var y = yCoordinate; y < yMaxSize; y++)
            {
                for (var x = xCoordinate; x < xMaxSize; x++) Board![x, y].IsShip = true;
            }
            
            BoardShipCapacity -= xSize * ySize;
        }


        /// <summary>
        /// Mark Cells Around and Inside The Ship as Reserved by It.
        /// </summary>
        /// <param name="ship">Ship to be Placed.</param>
        /// <param name="xCoordinate">X Coordinate for Ship Placement.</param>
        /// <param name="yCoordinate">Y Coordinate for Ship Placement.</param>
        /// <param name="shipTouchRule">Indicator of Ship Touching Rule for Ship Placement.</param>
        private void ReserveCellsAroundShip(Ship ship, int xCoordinate, int yCoordinate, EShipTouchRule shipTouchRule)
        {
            var canSidesTouch = shipTouchRule == EShipTouchRule.SidesCanTouch;
            
            var (xSize, ySize) = ship.GetSizes();
            var (xMaxSize, yMaxSize) = (xCoordinate + xSize, yCoordinate + ySize);

            var xReserved = xCoordinate <= 0 || canSidesTouch ? xCoordinate : xCoordinate - 1 ;
            var yReserved = yCoordinate <= 0 || canSidesTouch ? yCoordinate : yCoordinate - 1;
            
            var xLenght = xMaxSize == XBoardSize || canSidesTouch ? xMaxSize - 1 : xMaxSize;
            var yLenght = yMaxSize == YBoardSize || canSidesTouch ? yMaxSize - 1 : yMaxSize;
            
            for (var y = yReserved ; y <= yLenght; y++)
            {
                for (var x = xReserved ; x <= xLenght ; x++) Board![x, y].ReservedByShips.Add(ship.ShipNumber);
            }

            if (shipTouchRule != EShipTouchRule.CornersCanTouch || canSidesTouch) return;
            
            xReserved = xReserved == xCoordinate ? -1 : xReserved;
            yReserved = yReserved == yCoordinate ? -1 : yReserved;
            
            CornerHandler(xReserved, yReserved, ship.ShipNumber);
            CornerHandler(xMaxSize, yReserved, ship.ShipNumber);
            CornerHandler(xReserved, yMaxSize, ship.ShipNumber);
            CornerHandler(xMaxSize, yMaxSize, ship.ShipNumber);
        }
        
        
        /// <summary>
        /// Handles Corners' Reservation.
        /// </summary>
        /// <param name="xPos">X Coordinate Position for Corner Handling.</param>
        /// <param name="yPos">Y Coordinate Position for Corner Handling.</param>
        /// <param name="shipNumber">Ship Number to be Handled at Corner.</param>
        private void CornerHandler(int xPos, int yPos, int shipNumber)
        {
            if (xPos < 0 || xPos == XBoardSize || yPos < 0 || yPos == YBoardSize) return;

            Board![xPos, yPos].ReservedByShips.Remove(shipNumber);
        }


        /// <summary>
        /// Generate Random Ship Sizes to Fit the Board Fully.
        /// </summary>
        /// <returns>List of Randomly Generated Ship Sizes.</returns>
        public List<(int, int)> RandomShipGenerator()
        {
            var random = new Random();
            var shipSizes = new List<(int, int)>();
            var leftCapacity = BoardShipCapacity;
            var shipCount = random.Next(Convert.ToInt32(Math.Ceiling(leftCapacity / 1.5)));
            shipCount = shipCount == 0 ? 1 : shipCount;

            for (var x = 0; x < shipCount; x++)
            {
                shipSizes.Add((1, 1));
                leftCapacity -= 1;
            }
            
            while (leftCapacity != 0)
            {
                var isXSize = random.Next(2) == 0;
                var itemIndex = random.Next(shipSizes.Count);
                
                var (oldX, oldY) = shipSizes[itemIndex];
                var newShipSize = isXSize ? (oldX + 1, oldY) : (oldX, oldY + 1);

                if (leftCapacity - newShipSize.Item1 * newShipSize.Item2 < 0) break;

                leftCapacity += oldX * oldY;
                leftCapacity -= newShipSize.Item1 * newShipSize.Item2;

                shipSizes[itemIndex] = newShipSize;
            }

            return shipSizes;
        }

        
        /// <summary>
        /// Finds Free Cells For Ship Placement.
        /// </summary>
        /// <returns>List of Free Cells for Placement.</returns>
        public List<(int, int)> FreeBoardCells()
        {
            var freeCells = new List<(int, int)>();
            
            for (var y = 0; y < YBoardSize; y++)
            {
                for (var x = 0; x < XBoardSize; x++) if (!Board![x, y].IsReserved()) freeCells.Add((x, y));
            }

            return freeCells;
        }
        
        
        /// <summary>
        /// Get the Copy of the Needed Board.
        /// </summary>
        /// <returns>Deep Copy of the Needed Board.</returns>
        public BoardSquareState[,] CopyBoard()
        {
            var copyBoard = new BoardSquareState[XBoardSize, YBoardSize];
            
            for (var y = 0; y < YBoardSize; y++)
            {
                for (var x = 0; x < XBoardSize; x++) copyBoard[x, y] = Board![x, y];
            }

            return copyBoard;
        }
    }
}