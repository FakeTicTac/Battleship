

namespace BLL.Battleship.Strategies
{
    
    /// <summary>
    /// Implementation of Easy AI and It's Decisions During the Game Process.
    /// </summary>
    public class AIEasyStrategy : AIBaseStrategy
    {

        public AIEasyStrategy()
        {
            
        }
        
        /// <summary>
        /// Basic Easy AI Constructor. Defines Available Cells for Hitting.
        /// </summary>
        /// <param name="xSize">X Coordinate Board Size.</param>
        /// <param name="ySize">Y Coordinate Board Size.</param>
        public AIEasyStrategy(int xSize, int ySize) : base(xSize, ySize) { }
    }
}