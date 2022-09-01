using System.Linq;
using BLL.Battleship.Enums;
using System.Collections.Generic;

using DL = DAL.Battleship.DTO;


namespace BLL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Player Business Logic Layer Mapper.
    /// </summary>
    public static class PlayerMapper
    {
        
        /// <summary>
        /// Map Data Access Layer Player Object To The Business Logic Layer Player Object.
        /// </summary>
        /// <returns>Business Logic Layer Player Object.</returns>
        public static Player? MapToBll(DL.Player? player, EAILevel? aiLevel)
        {
            if (player == null) return null;

            var shots = new List<PlayerShot>();

            player.PlayerShots?.GroupBy(x => x.ShotNumber).ToList().ForEach(y => shots.Add(new PlayerShot
            {
                PlayerShots = y.ToList()
                    .ConvertAll(z => (z.Coordinate!.xCoordinateValue, z.Coordinate!.yCoordinateValue))
            }));

            return new Player
            {
                Name = player.Name,
                IsTurn = player.IsTurn,
                PlayerType = (EPlayerType)player.PlayerType,
                IsLost = player.IsLost,
                IsPlacementCompleted = player.IsPlacementCompleted,
                OwnBoard = GameBoardMapper.MapToBll(player.OwnBoard),
                FiringBoard = GameBoardMapper.MapToBll(player.FiringBoard),
                OwnShips = player.Ships?.ConvertAll(ShipMapper.MapToBll),
                Strategy = StrategyMapper.MapToBll(player.Strategy, aiLevel),
                PlayerShots = shots
            };
        }


        /// <summary>
        /// Map Business Logic Layer Player Object To Data Access Layer Player Object.
        /// </summary>
        /// <returns>Data Access Layer Player Object.</returns>
        public static DL.Player? MapToDal(Player? player)
        {
            if (player == null) return null;

            var shots = new List<DL.PlayerShot>();
            
            player.PlayerShots?.ForEach(x => x.PlayerShots?.ForEach(y => shots.Add(new DL.PlayerShot {
                ShotNumber = player.PlayerShots!.IndexOf(x),
                Coordinate = new DL.Coordinate
                {
                    xCoordinateValue = y.Item1,
                    yCoordinateValue = y.Item2
                }
            })));
            
            return new DL.Player
            {
                Name = player.Name,
                IsTurn = player.IsTurn,
                PlayerType = (int) player.PlayerType,
                IsLost = player.IsLost,
                IsPlacementCompleted = player.IsPlacementCompleted,
                OwnBoard = GameBoardMapper.MapToDal(player.OwnBoard),
                FiringBoard = GameBoardMapper.MapToDal(player.FiringBoard),
                Ships = player.OwnShips?.ConvertAll(ShipMapper.MapToDal),
                Strategy = StrategyMapper.MapToDal(player.Strategy),
                PlayerShots = shots
            };
        }
    }
}