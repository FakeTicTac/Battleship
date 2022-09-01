using System;

using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Coordinate Mapper.
    /// </summary>
    public static class CoordinateMapper
    {
        
        /// <summary>
        /// Map Domain Coordinate Object To The Data Access Layer Coordinate Object.
        /// </summary>
        /// <returns>Data Access Layer Coordinate Object.</returns>
        public static DL.Coordinate MapToDal(DB.Coordinate? coordinate)
        {
            if (coordinate == null)
                throw new ArgumentException(
                    "Mapping to Data Access Layer is Impossible. Coordinate Object From Database equals Null.");
            
            return new DL.Coordinate
            {
                xCoordinateValue = coordinate.xCoordinateValue,
                yCoordinateValue = coordinate.yCoordinateValue
            };
        }


        /// <summary>
        /// Map Data Access Layer Coordinate Object To The Domain Coordinate Object.
        /// </summary>
        /// <returns>Data Access Layer Coordinate Object.</returns>
        public static DB.Coordinate MapToDb(DL.Coordinate? coordinate)
        {
            if (coordinate == null)
                throw new ArgumentException(
                    "Mapping to Database Layer is Impossible. Coordinate Object From Data Access Layer equals Null.");
            
            return new DB.Coordinate
            {
                xCoordinateValue = coordinate.xCoordinateValue,
                yCoordinateValue = coordinate.yCoordinateValue
            };
        }
    }
}