
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines StrategyCoordinate Mapper.
    /// </summary>
    public static class StrategyCoordinateMapper
    {
        
        /// <summary>
        /// Map Domain StrategyCoordinate Object To The Data Access Layer StrategyCoordinate Object.
        /// </summary>
        /// <returns>Data Access Layer StrategyCoordinate Object.</returns>
        public static DL.StrategyCoordinate MapToDal(DB.StrategyCoordinate strategyCoordinate)
        {
            return new DL.StrategyCoordinate
            {
                StrategyCoordinateType = strategyCoordinate.StrategyCoordinateType,
                Coordinate = CoordinateMapper.MapToDal(strategyCoordinate.Coordinate)
            };
        }


        /// <summary>
        /// Map Data Access Layer StrategyCoordinate Object To The Domain StrategyCoordinate Object.
        /// </summary>
        /// <returns>Data Access Layer StrategyCoordinate Object.</returns>
        public static DB.StrategyCoordinate MapToDb(DL.StrategyCoordinate strategyCoordinate)
        {
            return new DB.StrategyCoordinate
            {
                StrategyCoordinateType = strategyCoordinate.StrategyCoordinateType,
                Coordinate = CoordinateMapper.MapToDb(strategyCoordinate.Coordinate)
            };
        }
    }
}