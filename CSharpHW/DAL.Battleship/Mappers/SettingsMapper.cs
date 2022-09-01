
using DB = Domain.Battleship;
using DL = DAL.Battleship.DTO;


namespace DAL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Settings Mapper.
    /// </summary>
    public static class SettingsMapper
    {
        
        /// <summary>
        /// Map Domain Settings Object To The Data Access Layer Settings Object.
        /// </summary>
        /// <returns>Data Access Layer Settings Object.</returns>
        public static DL.Settings? MapToDal(DB.Settings? settings)
        {
            if (settings == null) return null;

                return new DL.Settings
            {
                SettingsId = settings.SettingsId,
                AiLevel = settings.AiLevel,
                GameMode = settings.GameMode,
                ShipTouchRule = settings.ShipTouchRule,
                ShipUsageRule = settings.ShipUsageRule,
                Name = settings.Name,
                IsManualCreation = settings.IsManualCreation,
                BoardXSize = settings.BoardXSize,
                BoardYSize = settings.BoardYSize,
                SavedAt = settings.SavedAt
            };
        }


        /// <summary>
        /// Map Data Access Layer Settings Object To The Domain Settings Object.
        /// </summary>
        /// <returns>Data Access Layer Settings Object.</returns>
        public static DB.Settings? MapToDb(DL.Settings? settings)
        {
            if (settings == null) return null;

                return new DB.Settings
            {
                AiLevel = settings.AiLevel,
                GameMode = settings.GameMode,
                ShipTouchRule = settings.ShipTouchRule,
                ShipUsageRule = settings.ShipUsageRule,
                Name = settings.Name,
                IsManualCreation = settings.IsManualCreation,
                BoardXSize = settings.BoardXSize,
                BoardYSize = settings.BoardYSize,
                SavedAt = settings.SavedAt
            };
        }
    }
}