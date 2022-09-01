using System.ComponentModel;


namespace BLL.Battleship.Enums
{
    
    /// <summary>
    /// Enumeration of Possible Game Modes.
    /// </summary>
    public enum EGameMode
    {
        [Description("Single Player")]
        SinglePlayer,
        
        [Description("Multi Player")]
        MultiPlayer
    }
}