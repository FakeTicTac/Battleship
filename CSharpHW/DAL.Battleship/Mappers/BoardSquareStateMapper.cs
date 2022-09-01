
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines BoardSquareState Mapper.
    /// </summary>
    public static class BoardSquareStateMapper
    {
        
        /// <summary>
        /// Map Domain BoardSquareState Object To The Data Access Layer BoardSquareState Object.
        /// </summary>
        /// <returns>Data Access Layer  BoardSquareState Object.</returns>
        public static DL.BoardSquareState MapToDal(DB.BoardSquareState boardSquareState)
        {
            return new DL.BoardSquareState
            {
                IsShip = boardSquareState.IsShip,
                IsBomb = boardSquareState.IsBomb,
                XCoordinate = boardSquareState.XCoordinate,
                YCoordinate = boardSquareState.YCoordinate,
                ReservedByShips = boardSquareState.ReservedByShips
            };
        }


        /// <summary>
        /// Map Data Access Layer BoardSquareState Object To The Domain BoardSquareState Object.
        /// </summary>
        /// <returns>Data Access Layer BoardSquareState Object.</returns>
        public static DB.BoardSquareState MapToDb(DL.BoardSquareState boardSquareState)
        {
            return new DB.BoardSquareState
            {
                IsShip = boardSquareState.IsShip,
                IsBomb = boardSquareState.IsBomb,
                XCoordinate = boardSquareState.XCoordinate,
                YCoordinate = boardSquareState.YCoordinate,
                ReservedByShips = boardSquareState.ReservedByShips
            };
        }
    }
}