using System;

using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines GameBoard Mapper.
    /// </summary>
    public static class GameBoardMapper
    {
        
        /// <summary>
        /// Map Domain GameBoard Object To The Data Access Layer GameBoard Object.
        /// </summary>
        /// <returns>Data Access Layer GameBoard Object.</returns>
        public static DL.GameBoard MapToDal(DB.GameBoard? gameBoard)
        {
            if (gameBoard == null)
                throw new ArgumentException(
                    "Mapping to Data Access Layer is Impossible. GameBoard Object From Database equals Null.");
            
            return new DL.GameBoard
            {
                XBoardSize = gameBoard.XBoardSize,
                YBoardSize = gameBoard.YBoardSize,
                BoardShipCapacity = gameBoard.BoardShipCapacity,
                BoardSquareStates = gameBoard.BoardSquareStates?.ConvertAll(BoardSquareStateMapper.MapToDal)
            };
        }


        /// <summary>
        /// Map Data Access Layer Player Object To The Domain Player Object.
        /// </summary>
        /// <returns>Data Access Layer Player Object.</returns>
        public static DB.GameBoard MapToDb(DL.GameBoard? gameBoard)
        {
            if (gameBoard == null)
                throw new ArgumentException(
                    "Mapping to Database Layer is Impossible. GameBoard Object From Data Access Layer equals Null.");
            
            return new DB.GameBoard
            {
                XBoardSize = gameBoard.XBoardSize,
                YBoardSize = gameBoard.YBoardSize,
                BoardShipCapacity = gameBoard.BoardShipCapacity,
                BoardSquareStates = gameBoard.BoardSquareStates?.ConvertAll(BoardSquareStateMapper.MapToDb)
            };
        }
    }
}