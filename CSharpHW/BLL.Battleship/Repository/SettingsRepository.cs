using System;
using DAL.Battleship;
using BLL.Battleship.Mappers;
using System.Collections.Generic;


namespace BLL.Battleship.Repository
{
    
    /// <summary>
    /// Settings Business Logic Layer Repository Design Implementation.
    /// </summary>
    public class SettingsRepository
    {
        
        /// <summary>
        /// Definition of Database Connection.
        /// </summary>
        private DAL.Battleship.Repository.SettingsRepository _dalRepository { get; }
        
        /// <summary>
        /// Basic Settings Repository Constructor. Defines Connection to Database.
        /// </summary>
        /// <param name="dbContext">Definition of Database Connection.</param>
        /// <param name="localJsonSavePath">Definition of Local JSON File System Connection.</param>
        public SettingsRepository(AppDbContext dbContext, string localJsonSavePath) =>
            _dalRepository = new DAL.Battleship.Repository.SettingsRepository(dbContext, localJsonSavePath);
        
        
        /// <summary>
        /// Method Gets All Settings Saves From Database.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<Settings> GetAllSavesDatabase() => 
            _dalRepository.GetAllSavesDatabase().ConvertAll(SettingsMapper.MapToBll)!;


        /// <summary>
        /// Method Gets One Settings Save From Database.
        /// </summary>
        /// <param name="settingID">Settings ID To Load From Database.</param>
        /// <returns>Domain Settings Object -> Loaded from Database and Mapped to Dal Objects.</returns>
        public Settings GetSaveDatabase(Guid settingID) =>
            SettingsMapper.MapToBll(_dalRepository.GetSaveDatabase(settingID))!;


        /// <summary>
        /// Method Saves Settings To The Database.
        /// </summary>
        /// <param name="settings">Settings State From Brain.</param>
        /// <param name="saveName">Name of the Save.</param>
        public void SaveToDatabase(Settings settings, string? saveName) => 
            _dalRepository.SaveToDatabase(SettingsMapper.MapToDal(settings, saveName)!);


        /// <summary>
        /// Method Gets All Settings Saves From Local JSON.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<Settings> GetAllSavesLocal() => _dalRepository.GetAllSavesLocal().ConvertAll(SettingsMapper.MapToBll)!;


        /// <summary>
        /// Method Saves Settings To The Local JSON.
        /// </summary>
        /// <param name="settings">Settings State From Brain..</param>
        /// <param name="saveName">Name of the Save.</param>
        public void SaveToLocal(Settings settings, string? saveName) => _dalRepository.SaveToLocal(SettingsMapper.MapToDal(settings, saveName)!);
    }
}