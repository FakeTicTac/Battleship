using System.Text.Json;
using System.Collections.Generic;

using DL = DAL.Battleship.DTO;


namespace BLL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines BoardSquareState Business Logic Layer Mapper.
    /// </summary>
    public static class BoardSquareStateMapper
    {
        
        /// <summary>
        /// Map Data Access Layer BoardSquareState Object To The Business Logic Layer BoardSquareState Object.
        /// </summary>
        /// <returns>Business Logic Layer BoardSquareState Object.</returns>
        public static BoardSquareState MapToBll(DL.BoardSquareState boardSquareState)
        {
            return new BoardSquareState
            {
                IsShip = boardSquareState.IsShip,
                IsBomb = boardSquareState.IsBomb,
                XCoordinate = boardSquareState.XCoordinate,
                YCoordinate = boardSquareState.YCoordinate,
                ReservedByShips = JsonSerializer.Deserialize<List<int>>(boardSquareState.ReservedByShips!)!
            };
        }


        /// <summary>
        /// Map Business Logic Layer BoardSquareState Object To Data Access Layer BoardSquareState Object.
        /// </summary>
        /// <returns>Data Access Layer BoardSquareState Object.</returns>
        public static DL.BoardSquareState MapToDal(BoardSquareState boardSquareState)
        {
            return new DL.BoardSquareState
            {
                IsShip = boardSquareState.IsShip,
                IsBomb = boardSquareState.IsBomb,
                XCoordinate = boardSquareState.XCoordinate,
                YCoordinate = boardSquareState.YCoordinate,
                ReservedByShips = JsonSerializer.Serialize(boardSquareState.ReservedByShips)
            };
        }
    }
}