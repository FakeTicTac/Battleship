using System;
using BLL.Battleship.Enums;
using BLL.Battleship.Extensions;


namespace BLL.Battleship
{
    
    /// <summary>
    /// Represents BattleShip Game Settings' State.
    /// </summary>
    public class Settings
    {
        
        /// <summary>
        /// Entity ID from Database.
        /// </summary>
        public Guid SettingsId { get; init; }
        
        
        /// <summary>
        /// Name of the Setting Save.
        /// </summary>
        public string? Name { get; init; }
        
        
        /// <summary>
        /// Represents if Game Mode of the BattleShip.
        /// </summary>
        public EGameMode GameMode { get; set; }
        
        
        /// <summary>
        /// Represents The Level of AI to Play With.
        /// </summary>
        public EAILevel? AILevel { get; set; }
        
        
        /// <summary>
        /// Represents the X Coordinate Lenght of the Board.
        /// </summary>
        public int XBoardSize { get; set; }
        
        
        /// <summary>
        /// Represents the Y Coordinate Lenght of the Board.
        /// </summary>
        public int YBoardSize { get; set; }
        
        
        /// <summary>
        /// Indicator of Setting Manual Creation Instance.
        /// </summary>
        public bool IsManual { get; set; }


        /// <summary>
        /// Represents The Rule of Ship Touching.
        /// </summary>
        public EShipTouchRule ShipTouchRule { get; set; }
        
        
        /// <summary>
        /// Represent The Rule of Ship Usage.
        /// </summary>
        public EShipUsageRule ShipUsageRule { get; set; }

        
        /// <summary>
        /// Basic Parameterless Settings Constructor.
        /// </summary>
        public Settings() {}


        /// <summary>
        /// Basic Constructor for Settings. Some Parameters can be Omitted, in this Case Standard Rules are Used.
        /// That means 10x10 Board, Same Ships and Ships Cannot Touch at All.
        /// </summary>
        /// <param name="gameMode">Indicates Mode of the Game to be Played.</param>
        /// <param name="aiLevel">Indicates AI Difficulty Level to Play With.</param>
        /// <param name="xBoardSize">Represents the X Coordinate Lenght of the Board.</param>
        /// <param name="yBoardSize">Represents the Y Coordinate Lenght of the Board.</param>
        /// <param name="shipUsageRule">Indicates the Rules for Ship Choose.</param>
        /// <param name="shipTouchRule">Indicates the Rule for Ship Touching.</param>
        /// <param name="isManual">Indicator of Setting Manual Creation Instance.</param>
        public Settings(EGameMode gameMode, EAILevel? aiLevel, int xBoardSize = 10, int yBoardSize = 10, 
            EShipUsageRule shipUsageRule = EShipUsageRule.SameShips, EShipTouchRule shipTouchRule = EShipTouchRule.NoTouching, bool isManual = false)
        {
            if (!BoardSizeValidator(xBoardSize) || !BoardSizeValidator(yBoardSize))
                throw new ArgumentException("Given Board Sizes are not Supported.");
            
            AIInitialization(gameMode, aiLevel);
            
            GameMode = gameMode;
            ShipUsageRule = shipUsageRule;
            ShipTouchRule = shipTouchRule;

            XBoardSize = xBoardSize;
            YBoardSize = yBoardSize;
            IsManual = isManual;
        }


        /// <summary>
        /// Validates AI Choose and Initialize It.
        /// </summary>
        /// <param name="gameMode">Indicates Mode of the Game to be Played.</param>
        /// <param name="aiLevel">Indicates AI Difficulty Level to Play With.</param>
        private void AIInitialization(EGameMode gameMode, EAILevel? aiLevel)
        {
            AILevel = gameMode switch
            {
                EGameMode.SinglePlayer when aiLevel == null => throw new ArgumentException(
                    "AI Difficulty Should Be Chosen, When Playing Single Player Mode."),
                EGameMode.SinglePlayer => aiLevel,
                _ => null
            };
        }
        
        
        /// <summary>
        /// Validates Board sizes based on Maximum and Minimum supported Sizes.
        /// </summary>
        /// <param name="boardSize">Size of the Board.</param>
        /// <returns>Boolean indicator of Support. If Supported then True.</returns>
        private static bool BoardSizeValidator(int boardSize)
        {
            const int minBoardSize = 4;
            const int maxBoardSize = 50;
                
            return boardSize is >= minBoardSize and <= maxBoardSize;
        } 
        
        
        /// <summary>
        /// String  Implementation of Settings.
        /// </summary>
        /// <returns>Returns all Settings String.</returns>
        public override string ToString() => 
            $"Game Mode: {GameMode.GetDescription()}\n" +
            $"Board Sizes: {XBoardSize}x{YBoardSize}\n" +
            $"Ship Usage Rule: {ShipUsageRule.GetDescription()}\n" +
            $"Ship Touching Rule: {ShipTouchRule.GetDescription()}\n" +
            $"AI Level: {AILevel?.GetDescription()}";
    }
}