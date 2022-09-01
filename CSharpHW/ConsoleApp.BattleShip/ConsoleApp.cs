using DAL.Battleship;
using Controller.Battleship;


namespace ConsoleApp.BattleShip
{
    
    /// <summary>
    /// Console Battleship Application Runtime.
    /// </summary>
    internal static class ConsoleApp
    {
        
        /// <summary>
        /// Battleship Game Start Up.
        /// </summary>
        private static void Main() => new BattleShipController(new AppDbContext()).RunController();
    }
}
