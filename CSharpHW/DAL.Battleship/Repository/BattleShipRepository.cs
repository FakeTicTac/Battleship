using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using DAL.Battleship.DTO;
using DAL.Battleship.Mappers;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace DAL.Battleship.Repository
{
    
    /// <summary>
    /// BattleShip Data Access Layer Repository Design Implementation.
    /// </summary>
    public class BattleShipRepository
    {
        
        /// <summary>
        /// Definition of Database Connection.
        /// </summary>
        private AppDbContext _appDbContext { get; }
        
        
        /// <summary>
        /// Definition of Local JSON File System Connection.
        /// </summary>
        private string _localJsonSavePath { get; }


        /// <summary>
        /// Basic BattleShip Repository Constructor. Defines Connection to Database.
        /// </summary>
        /// <param name="dbContext">Definition of Database Connection.</param>
        /// <param name="localJsonSavePath">Definition of Local JSON File System Connection.</param>
        public BattleShipRepository(AppDbContext dbContext, string localJsonSavePath)
        {
            _appDbContext = dbContext;
            _localJsonSavePath = localJsonSavePath;
        } 
        
        
        /// <summary>
        /// Method Gets All BattleShip Game Saves From Database.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<BattleShip> GetAllSavesDatabase()
        {
            var query = _appDbContext.Battleships;

            return query.ToList().ConvertAll(BattleShipMapper.MapToDal);
        }

        
        /// <summary>
        /// Method Gets One BattleShip Game Save From Database.
        /// </summary>
        /// <param name="gameID">Game ID To Load From Database.</param>
        /// <returns>Domain BattleShip Object -> Loaded from Database and Mapped to Dal Objects.</returns>
        public BattleShip GetSaveDatabase(Guid gameID)
        {
            var battleShip = 
                _appDbContext.Battleships.Where(x => x.BattleShipId == gameID)
                    
                .Include(battleShip => battleShip.Settings)
                .Include(battleShip => battleShip.PlayerOne)
                .ThenInclude(playerOne => playerOne!.Strategy)
                
                .Include(battleShip => battleShip.PlayerOne)
                .ThenInclude(playerOne => playerOne!.Strategy)
                .ThenInclude(strategy => strategy!.StrategyCoordinates)
                .ThenInclude(cell => cell!.Coordinate)

                .Include(battleShip => battleShip.PlayerOne)
                .ThenInclude(playerOne => playerOne!.OwnBoard)
                .ThenInclude(board => board!.BoardSquareStates)

                .Include(battleShip => battleShip.PlayerOne)
                .ThenInclude(playerOne => playerOne!.FiringBoard)
                .ThenInclude(board => board!.BoardSquareStates)

                .Include(battleShip => battleShip.PlayerOne)
                .ThenInclude(playerOne => playerOne!.Ships)

                .Include(battleShip => battleShip.PlayerOne)
                .ThenInclude(playerOne => playerOne!.PlayerShots)
                .ThenInclude(cell => cell.Coordinate)

                .Include(battleShip => battleShip.PlayerTwo)
                .ThenInclude(playerOne => playerOne!.Strategy)
                .ThenInclude(strategy => strategy!.StrategyCoordinates)
                .ThenInclude(cell => cell!.Coordinate)

                .Include(battleShip => battleShip.PlayerTwo)
                .ThenInclude(playerTwo => playerTwo!.OwnBoard)
                .ThenInclude(board => board!.BoardSquareStates)

                .Include(battleShip => battleShip.PlayerTwo)
                .ThenInclude(playerTwo => playerTwo!.FiringBoard)
                .ThenInclude(board => board!.BoardSquareStates)

                .Include(battleShip => battleShip.PlayerTwo)
                .ThenInclude(playerTwo => playerTwo!.Ships)

                .Include(query => query.PlayerTwo)
                .ThenInclude(playerTwo => playerTwo!.PlayerShots)
                .ThenInclude(cell => cell.Coordinate)
                .AsSplitQuery();

            return BattleShipMapper.MapToDal(battleShip.ToList()[0]);
        }


        /// <summary>
        /// Method Saves Game To The Database.
        /// </summary>
        /// <param name="battleShip">Game State From Brain Mapped to Data Access Layer.</param>
        public void SaveToDatabase(BattleShip battleShip)
        {
            _appDbContext.Battleships.Add(BattleShipMapper.MapToDb(battleShip));
            _appDbContext.SaveChanges();
        }
        
        
        /// <summary>
        /// Method Gets All BattleShip Game Saves From Local JSON.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<BattleShip> GetAllSavesLocal()
        {
            var files = Directory.GetFiles(_localJsonSavePath, "*.json");
            var battleShips = files
                .Select(file => JsonSerializer.Deserialize<BattleShip>(File.ReadAllText(file))!).ToList();

            return battleShips;
        }
        
        
        /// <summary>
        /// Method Saves Game To The Local JSON.
        /// </summary>
        /// <param name="battleShip">Game State From Brain Mapped to Data Access Layer.</param>
        public void SaveToLocal(BattleShip battleShip) =>
            File.WriteAllText(_localJsonSavePath + $"{battleShip.Name}.json", JsonSerializer.Serialize(battleShip));
    }
}