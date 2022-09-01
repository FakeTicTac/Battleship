
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Strategy Mapper.
    /// </summary>
    public static class StrategyMapper
    {
        
        /// <summary>
        /// Map Domain Strategy Object To The Data Access Layer Strategy Object.
        /// </summary>
        /// <returns>Data Access Layer Strategy Object.</returns>
        public static DL.Strategy? MapToDal(DB.Strategy? strategy)
        {
            if (strategy == null) return null;
            
            return new DL.Strategy
            {
                XSize = strategy.XSize,
                YSize = strategy.YSize,
                StrategyCoordinates = strategy.StrategyCoordinates?.ConvertAll(StrategyCoordinateMapper.MapToDal)
            };
        }


        /// <summary>
        /// Map Data Access Layer Strategy Object To The Domain Strategy Object.
        /// </summary>
        /// <returns>Data Access Layer Strategy Object.</returns>
        public static DB.Strategy? MapToDb(DL.Strategy? strategy)
        {
            if (strategy == null) return null;
            
            return new DB.Strategy
            {
                XSize = strategy.XSize,
                YSize = strategy.YSize,
                StrategyCoordinates = strategy.StrategyCoordinates?.ConvertAll(StrategyCoordinateMapper.MapToDb)
            };
        }
    }
}