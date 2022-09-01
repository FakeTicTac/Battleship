
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines PlayerShot Mapper.
    /// </summary>
    public static class PlayerShotMapper
    {
        
        /// <summary>
        /// Map Domain PlayerShot Object To The Data Access Layer PlayerShot Object.
        /// </summary>
        /// <returns>Data Access Layer PlayerShot Object.</returns>
        public static DL.PlayerShot MapToDal(DB.PlayerShot playerShot)
        {
            return new DL.PlayerShot
            {
                ShotNumber = playerShot.ShotNumber,
                Coordinate = CoordinateMapper.MapToDal(playerShot.Coordinate)
            };
        }


        /// <summary>
        /// Map Data Access Layer PlayerShot Object To The Domain PlayerShot Object.
        /// </summary>
        /// <returns>Data Access Layer PlayerShot Object.</returns>
        public static DB.PlayerShot MapToDb(DL.PlayerShot playerShot)
        {
            return new DB.PlayerShot
            {
                ShotNumber = playerShot.ShotNumber,
                Coordinate = CoordinateMapper.MapToDb(playerShot.Coordinate)
            };
        }
    }
}