using System;
using DAL.Battleship;
using BLL.Battleship.Mappers;
using System.Collections.Generic;


namespace BLL.Battleship.Repository
{
    
    /// <summary>
    /// BattleShip Business Logic Layer Repository Design Implementation.
    /// </summary>
    public class BattleShipRepository
    {
        
        /// <summary>
        /// Definition of Database Connection.
        /// </summary>
        private DAL.Battleship.Repository.BattleShipRepository _dalRepository { get; }
        

        /// <summary>
        /// Basic BattleShip Repository Constructor. Defines Connection to Database.
        /// </summary>
        /// <param name="dbContext">Definition of Database Connection.</param>
        /// <param name="localJsonSavePath">Definition of Local JSON File System Connection.</param>
        public BattleShipRepository(AppDbContext dbContext, string localJsonSavePath) =>
             _dalRepository = new DAL.Battleship.Repository.BattleShipRepository(dbContext, localJsonSavePath);
        
        
        /// <summary>
        /// Method Gets All BattleShip Game Saves From Database.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<BattleShip> GetAllSavesDatabase() => 
                                _dalRepository.GetAllSavesDatabase().ConvertAll(BattleShipMapper.MapToBll);


        /// <summary>
        /// Method Gets One BattleShip Game Save From Database.
        /// </summary>
        /// <param name="gameID">Game ID To Load From Database.</param>
        /// <returns>Domain BattleShip Object -> Loaded from Database and Mapped to Dal Objects.</returns>
        public BattleShip GetSaveDatabase(Guid gameID) =>
                             BattleShipMapper.MapToBll(_dalRepository.GetSaveDatabase(gameID));


        /// <summary>
        /// Method Saves Game To The Database.
        /// </summary>
        /// <param name="battleShip">Game State From Brain.</param>
        /// <param name="saveName">Name of the Save.</param>
        public void SaveToDatabase(BattleShip battleShip, string? saveName) => 
                                 _dalRepository.SaveToDatabase(BattleShipMapper.MapToDal(battleShip, saveName));


        /// <summary>
        /// Method Gets All BattleShip Game Saves From Local JSON.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<BattleShip> GetAllSavesLocal() =>
                                     _dalRepository.GetAllSavesLocal().ConvertAll(BattleShipMapper.MapToBll);


        /// <summary>
        /// Method Saves Game To The Local JSON.
        /// </summary>
        /// <param name="battleShip">Game State From Brain..</param>
        /// <param name="saveName">Name of the Save.</param>
        public void SaveToLocal(BattleShip battleShip, string? saveName) =>
                                _dalRepository.SaveToLocal(BattleShipMapper.MapToDal(battleShip, saveName));
    }
}