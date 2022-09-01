using System.Collections.Generic;


namespace BLL.Battleship
{
    
    /// <summary>
    /// Represents Players' Shot State.
    /// </summary>
    public class PlayerShot
    {
        
        /// <summary>
        /// List Stores Player Sots During The Game.
        /// </summary>
        public List<(int, int)>? PlayerShots { get; init; }


        /// <summary>
        /// Basic Constructor For PlayerShot.
        /// </summary>
        public PlayerShot() => PlayerShots = new List<(int, int)>();
    }
}