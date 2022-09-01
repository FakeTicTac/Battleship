
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{

    /// <summary>
    /// Class Defines BattleShip Data Access Layer Mapper.
    /// </summary>
    public static class BattleShipMapper
    {
        
        /// <summary>
        /// Map Domain BattleShip Object To The Data Access Layer BattleShip Object.
        /// </summary>
        /// <returns>Data Access Layer BattleShip Object.</returns>
        public static DL.BattleShip MapToDal(DB.BattleShip battleShip)
        {
            return new DL.BattleShip
            {
                BattleShipId = battleShip.BattleShipId,
                Name = battleShip.Name,
                IsFinished = battleShip.IsFinished,
                SavedAt = battleShip.SavedAt,
                Settings = SettingsMapper.MapToDal(battleShip.Settings),
                PlayerOne = PlayerMapper.MapToDal(battleShip.PlayerOne),
                PlayerTwo = PlayerMapper.MapToDal(battleShip.PlayerTwo)
            };
        }


        /// <summary>
        /// Map Data Access Layer BattleShip Object To The Domain BattleShip Object.
        /// </summary>
        /// <returns>Database Layer BattleShip Object.</returns>
        public static DB.BattleShip MapToDb(DL.BattleShip battleShip)
        {
            return new DB.BattleShip
            {
                Name = battleShip.Name,
                IsFinished = battleShip.IsFinished,
                SavedAt = battleShip.SavedAt,
                Settings = SettingsMapper.MapToDb(battleShip.Settings),
                PlayerOne = PlayerMapper.MapToDb(battleShip.PlayerOne),
                PlayerTwo = PlayerMapper.MapToDb(battleShip.PlayerTwo)
            };
        }
    }
}