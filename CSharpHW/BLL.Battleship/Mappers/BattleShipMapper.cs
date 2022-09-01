using BLL.Battleship.Enums;

using DL = DAL.Battleship.DTO;


namespace BLL.Battleship.Mappers
{

    /// <summary>
    /// Class Defines BattleShip Business Logic Layer Mapper.
    /// </summary>
    public static class BattleShipMapper
    {
        
        /// <summary>
        /// Map Data Access Layer BattleShip Object To The Business Logic Layer BattleShip Object.
        /// </summary>
        /// <returns>Business Logic Layer BattleShip Object.</returns>
        public static BattleShip MapToBll(DL.BattleShip battleShip)
        {
            return new BattleShip
            {
                BattleShipId = battleShip.BattleShipId,
                Name = battleShip.Name,
                PLayerOne = PlayerMapper.MapToBll(battleShip.PlayerOne, null),
                PlayerTwo = PlayerMapper.MapToBll(battleShip.PlayerTwo, battleShip.Settings?.AiLevel != null ? (EAILevel) battleShip.Settings!.AiLevel : null),
                Settings = SettingsMapper.MapToBll(battleShip.Settings),
                IsFinished = battleShip.IsFinished
            };
        }


        /// <summary>
        /// Map Business Logic Layer BattleShip Object To Data Access Layer BattleShip Object.
        /// </summary>
        /// <returns>Data Access Layer BattleShip Object.</returns>
        public static DL.BattleShip MapToDal(BattleShip battleShip, string? saveName)
        {
            return new DL.BattleShip
            {
                Name = saveName,
                PlayerOne = PlayerMapper.MapToDal(battleShip.PLayerOne),
                PlayerTwo = PlayerMapper.MapToDal(battleShip.PlayerTwo),
                Settings = SettingsMapper.MapToDal(battleShip.Settings),
                IsFinished = battleShip.IsFinished,
            };
        }
    }
}