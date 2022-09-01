using System;
using System.Collections.Generic;

using DL = DAL.Battleship.DTO;


namespace BLL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines GameBoard Business Logic Layer Mapper.
    /// </summary>
    public static class GameBoardMapper
    {
        
        /// <summary>
        /// Map Data Access Layer GameBoard Object To The Business Logic Layer GameBoard Object.
        /// </summary>
        /// <returns>Business Logic Layer GameBoard Object.</returns>
        public static GameBoard MapToBll(DL.GameBoard? gameBoard)
        {
            if (gameBoard == null)
                throw new ArgumentException(
                    "Mapping to Business Logic is Impossible. GameBoard Object From Data Access equals Null.");

            return new GameBoard
            {
                XBoardSize = gameBoard.XBoardSize,
                YBoardSize = gameBoard.YBoardSize,
                BoardShipCapacity = gameBoard.BoardShipCapacity,
                Board = ListUnpack(gameBoard.XBoardSize, gameBoard.YBoardSize, gameBoard.BoardSquareStates)
            };
        }

        
        /// <summary>
        /// Unpack List of Data Access States and Transform Into Business Logic Layer States.
        /// </summary>
        /// <returns>Matrix of Board Square States.</returns>
        private static BoardSquareState[,] ListUnpack(int xSize, int ySize, List<DL.BoardSquareState>? boardSquareStates)
        {
            if (boardSquareStates == null)
                throw new ArgumentException(
                    "Mapping to Business Logic is Impossible. BoardSquareStates Object From Data Access equals Null.");
            
            var matrix = new BoardSquareState[xSize, ySize];

            for (var y = 0; y < ySize; y++)
            {
                for (var x = 0; x < xSize; x++)
                {
                    matrix[x, y] = BoardSquareStateMapper.MapToBll(
                        boardSquareStates.Find(z => z.XCoordinate == x && z.YCoordinate == y)!);
                }
            }

            return matrix;
        }
        
        
        /// <summary>
        /// Map Business Logic Layer GameBoard Object To Data Access Layer GameBoard Object.
        /// </summary>
        /// <returns>Data Access Layer Player Object.</returns>
        public static DL.GameBoard MapToDal(GameBoard? gameBoard)
        {
            if (gameBoard == null)
                throw new ArgumentException(
                    "Mapping to Data Access is Impossible. GameBoard Object From Business Logic equals Null.");
            
            return new DL.GameBoard
            {
                XBoardSize = gameBoard.XBoardSize,
                YBoardSize = gameBoard.YBoardSize,
                BoardShipCapacity = gameBoard.BoardShipCapacity,
                BoardSquareStates = ListPack(gameBoard.Board)
            };
        }
        
        
        /// <summary>
        /// Pack List of Data Access States from Business Logic Layer States.
        /// </summary>
        /// <returns>List of Data Access States.</returns>
        private static List<DL.BoardSquareState> ListPack(BoardSquareState[,]? boardSquareStates)
        {
            if (boardSquareStates == null)
                throw new ArgumentException(
                    "Mapping to Data Access is Impossible. BoardSquareStates Object From Business Logic equals Null.");

            var statesList = new List<DL.BoardSquareState>();
            
            for (var y = 0; y < boardSquareStates.GetLength(1); y++)
            {
                for (var x = 0; x < boardSquareStates.GetLength(0); x++)
                    statesList.Add(BoardSquareStateMapper.MapToDal(boardSquareStates[x, y]));
            }

            return statesList;
        }
    }
}