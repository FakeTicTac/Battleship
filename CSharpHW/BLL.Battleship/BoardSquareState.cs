using System.Collections.Generic;


namespace BLL.Battleship
{
    
    /// <summary>
    /// Represents State of Single Board Cell.
    /// </summary>
    public struct BoardSquareState
    {
        
        /// <summary>
        /// Indicates if Ship is Located on the Cell.
        /// </summary>
        public bool IsShip { get; set; }
        
        
        /// <summary>
        /// Indicates if Bomb is located on the cell.
        /// </summary>
        public bool IsBomb { get; set; }
        
        
        /// <summary>
        /// Indicates X Coordinate Position of the Cell on the Board.
        /// </summary>
        public int XCoordinate { get; init; }
        
        
        /// <summary>
        /// Indicates Y Coordinate Position of the Cell on the Board.
        /// </summary>
        public int YCoordinate { get; init; }

        
        /// <summary>
        /// List of Ships Serial Numbers that Reserved this Cell. (Others Ships can't be Placed Here)
        /// </summary>
        public List<int> ReservedByShips { get; init; }


        /// <summary>
        /// Board Square State Basic Constructor. Initializes Cell on the Board.
        /// </summary>
        /// <param name="xCoordinate">Cell X Coordinate on the Game Board.</param>
        /// <param name="yCoordinate">Cell Y Coordinate on the Game Board.</param>
        public BoardSquareState(int xCoordinate, int yCoordinate)
        {
            IsShip = false;
            IsBomb = false;
            
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            
            ReservedByShips = new List<int>();
        }

        
        /// <summary>
        /// Indicates if Cell is Reserved by Any Ship.
        /// </summary>
        /// <returns>Indicator of Ship Reservation.</returns>
        public bool IsReserved() => ReservedByShips.Count > 0;
        
        
        /// <summary>
        /// Indicates if Cell is Reserved by Given Ship.
        /// </summary>
        /// <param name="shipSerialNumber">Serial Number of Ship to define Reservation.</param>
        /// <returns>Indicator of Ship Reservation.</returns>
        public bool IsReservedByShip(int shipSerialNumber) => ReservedByShips.Contains(shipSerialNumber);
        
        
        /// <summary>
        /// String Representation of the Single Board Cell.
        /// </summary>
        /// <returns>String Representation of Current Cell.</returns>
        public override string ToString()
        {
            return (IsShip, IsBomb) switch
            {
                (false, false) => " ",
                (false, true) => "~",
                (true, false) => "0",
                (true, true) => "X"
            };
        }
    }
}