using System.ComponentModel;


namespace BLL.Battleship.Enums
{
    
    /// <summary>
    /// Enumeration of Possible Player Types.
    /// </summary>
    public enum EPlayerType
    {
        [Description("Human Player")]
        Human,
        
        [Description("AI Player")]
        AI
    }
}