using System.ComponentModel;


namespace BLL.Battleship.Enums
{
    
    /// <summary>
    /// Enumeration of Possible Ship Usage Rules.
    /// </summary>
    public enum EShipUsageRule
    {
        [Description("Players Use Same Ships")]
        SameShips,
        
        [Description("Players Use Different Ships")]
        DifferentShips
    }
}