
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Player Mapper.
    /// </summary>
    public static class PlayerMapper
    {
        
        /// <summary>
        /// Map Domain Player Object To The Data Access Layer Player Object.
        /// </summary>
        /// <returns>Data Access Layer Player Object.</returns>
        public static DL.Player? MapToDal(DB.Player? player)
        {
            if (player == null) return null;

            return new DL.Player
            {
              Name = player.Name,
              IsTurn = player.IsTurn,
              IsLost = player.IsLost,
              IsPlacementCompleted = player.IsPlacementCompleted,
              PlayerType = player.PlayerType,
              Strategy = StrategyMapper.MapToDal(player.Strategy),
              OwnBoard = GameBoardMapper.MapToDal(player.OwnBoard),
              FiringBoard = GameBoardMapper.MapToDal(player.FiringBoard),
              Ships = player.Ships?.ConvertAll(ShipMapper.MapToDal),
              PlayerShots = player.PlayerShots?.ConvertAll(PlayerShotMapper.MapToDal)
            };
        }


        /// <summary>
        /// Map Data Access Layer Player Object To The Domain Player Object.
        /// </summary>
        /// <returns>Data Access Layer Player Object.</returns>
        public static DB.Player? MapToDb(DL.Player? player)
        {
            if (player == null) return null;

            return new DB.Player
            {
                Name = player.Name,
                IsTurn = player.IsTurn,
                IsLost = player.IsLost,
                IsPlacementCompleted = player.IsPlacementCompleted,
                PlayerType = player.PlayerType,
                Strategy = StrategyMapper.MapToDb(player.Strategy),
                OwnBoard = GameBoardMapper.MapToDb(player.OwnBoard),
                FiringBoard = GameBoardMapper.MapToDb(player.FiringBoard),
                Ships = player.Ships?.ConvertAll(ShipMapper.MapToDb),
                PlayerShots = player.PlayerShots?.ConvertAll(PlayerShotMapper.MapToDb)
            };
        }
    }
}