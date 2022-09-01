using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using DAL.Battleship.DTO;
using DAL.Battleship.Mappers;
using System.Collections.Generic;


namespace DAL.Battleship.Repository
{
    
    /// <summary>
    /// Settings Data Access Layer Repository Design Implementation.
    /// </summary>
    public class SettingsRepository
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
        /// Basic Settings Repository Constructor. Defines Connection to Database.
        /// </summary>
        /// <param name="dbContext">Definition of Database Connection.</param>
        /// <param name="localJsonSavePath">Definition of Local JSON File System Connection.</param>
        public SettingsRepository(AppDbContext dbContext, string localJsonSavePath)
        {
            _appDbContext = dbContext;
            _localJsonSavePath = localJsonSavePath;
        } 
        
        
        /// <summary>
        /// Method Gets All Settings Saves From Database.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<Settings> GetAllSavesDatabase()
        {
            var query = _appDbContext.Settings.Where(x => x.IsManualCreation);

            return query.ToList().ConvertAll(SettingsMapper.MapToDal)!;
        }

        
        /// <summary>
        /// Method Gets One Settings Save From Database.
        /// </summary>
        /// <param name="settingsID">Settings ID To Load From Database.</param>
        /// <returns>Domain Settings Object -> Loaded from Database and Mapped to Dal Objects.</returns>
        public Settings GetSaveDatabase(Guid settingsID)
        {
            var settings = _appDbContext.Settings.Where(x => x.SettingsId == settingsID);
            
            return SettingsMapper.MapToDal(settings.ToList()[0])!;
        }


        /// <summary>
        /// Method Saves Settings To The Database.
        /// </summary>
        /// <param name="settings">Settings state From Brain Mapped to Data Access Layer.</param>
        public void SaveToDatabase(Settings settings)
        {
            _appDbContext.Settings.Add(SettingsMapper.MapToDb(settings)!);
            _appDbContext.SaveChanges();
        }
        
        
        /// <summary>
        /// Method Gets All Settings Game Saves From Local JSON.
        /// </summary>
        /// <returns>List of Game Saves.</returns>
        public List<Settings> GetAllSavesLocal()
        {
            var files = Directory.GetFiles(_localJsonSavePath, "*.json");
            var settings = files
                .Select(file => JsonSerializer.Deserialize<Settings>(File.ReadAllText(file))!).ToList();

            return settings;
        }
        
        
        /// <summary>
        /// Method Saves Game To The Local JSON.
        /// </summary>
        /// <param name="settings">Settings State From Brain Mapped to Data Access Layer.</param>
        public void SaveToLocal(Settings settings) =>
            File.WriteAllText(_localJsonSavePath + $"{settings.Name}.json", JsonSerializer.Serialize(settings));
    }
}