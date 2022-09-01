using System.ComponentModel;


namespace BLL.Battleship.Enums
{
    
    /// <summary>
    /// Enumeration of Possible AI Difficulty Levels.
    /// </summary>
    public enum EAILevel
    {
        [Description("Easy AI")]
        Easy,
        
        [Description("Mediun AI")]
        Medium,
        
        [Description("Hard AI")]
        Hard
    }
}