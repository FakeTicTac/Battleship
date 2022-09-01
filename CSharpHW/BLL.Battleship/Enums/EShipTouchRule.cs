using System.ComponentModel;


namespace BLL.Battleship.Enums
{
    
    /// <summary>
    /// Enumeration of Possible Ship Touch Rules.
    /// </summary>
    public enum EShipTouchRule
    {
        [Description("Ships Can't Touch")]
        NoTouching,
        
        [Description("Ship Corners Can Touch")]
        CornersCanTouch,
        
        [Description("Ship All Sides Can Touch")]
        SidesCanTouch
    }
}