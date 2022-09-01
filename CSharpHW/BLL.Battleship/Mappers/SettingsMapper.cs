using BLL.Battleship.Enums;

using DL = DAL.Battleship.DTO;


namespace BLL.Battleship.Mappers
{
    
    /// <summary>
    /// Class Defines Settings Business Logic Layer Mapper.
    /// </summary>
    public static class SettingsMapper
    {
        
        /// <summary>
        /// Map Data Access Layer Settings Object To The Business Logic Layer Settings Object.
        /// </summary>
        /// <returns>Business Logic Layer Settings Object.</returns>
        public static Settings? MapToBll(DL.Settings? settings)
        {
            if (settings == null) return null;

            return new Settings
            {
                SettingsId = settings.SettingsId,
                Name = settings.Name,
                GameMode = (EGameMode)settings.GameMode,
                AILevel = settings.AiLevel != null ? (EAILevel) settings.AiLevel : null,
                ShipTouchRule = (EShipTouchRule)settings.ShipTouchRule,
                ShipUsageRule = (EShipUsageRule)settings.ShipUsageRule,
                XBoardSize = settings.BoardXSize,
                YBoardSize = settings.BoardYSize
            };
        }


        /// <summary>
        /// Map Business Logic Layer BattleShip Object To Data Access Layer BattleShip Object.
        /// </summary>
        /// <returns>Data Access Layer BattleShip Object.</returns>
        public static DL.Settings? MapToDal(Settings? settings, string? saveName = null)
        {
            if (settings == null) return null;

            return new DL.Settings
            {
                Name = saveName,
                IsManualCreation = settings.IsManual,
                GameMode = (int) settings.GameMode,
                AiLevel = settings.AILevel != null ? (int) settings.AILevel : null,
                ShipTouchRule = (int) settings.ShipTouchRule,
                ShipUsageRule = (int) settings.ShipUsageRule,
                BoardXSize = settings.XBoardSize,
                BoardYSize = settings.YBoardSize
            };
        }
    }
}