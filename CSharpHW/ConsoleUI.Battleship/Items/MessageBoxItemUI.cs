
using SF  = ConsoleUI.Battleship.SharedUI;


namespace ConsoleUI.Battleship.Items
{
    
    /// <summary>
    /// Describes BattleShip Game Message Box Item UI Solution.
    /// </summary>
    public class MessageBoxItemUI
    {
        
        /// <summary>
        /// Max Size of the Message Box on X Coordinate.
        /// </summary>
        private int BoxXLimit { get; }
        
        
        /// <summary>
        /// Max Size of the Message Box on Y Coordinate.
        /// </summary>
        private int BoxYLimit { get; }


        /// <summary>
        /// Describes X Coordinate Cursor Position For Item to be Placed.
        /// </summary>
        private int ItemXPosition { get; }
            
            
        /// <summary>
        /// Describes Y Coordinate Cursor Position For Item to be Placed.
        /// </summary>
        private int ItemYPosition { get; }


        /// <summary>
        /// Message Box Item Basic Constructor. Defines It's Position and Sizes.
        /// </summary>
        /// <param name="xPos">Message Box X Coordinate Position.</param>
        /// <param name="yPos">Message Box Y Coordinate Position.</param>
        /// <param name="xSize">Message Box X Coordinate Size.</param>
        /// <param name="ySize">Message Box YCoordinate Size.</param>
        public MessageBoxItemUI(int xPos, int yPos, int xSize, int ySize)
        {
            ItemXPosition = xPos;
            ItemYPosition = yPos;

            BoxXLimit = xSize;
            BoxYLimit = ySize;
        }

        
        /// <summary>
        /// Display Message Box in The Console with Specified Message.
        /// </summary>
        /// <param name="message">Message to be Displayed in the Box.</param>
        /// <param name="messageYPos">Message Y Coordinate Position in the Box.</param>
        public (int, int) DisplayMessageBox(string message, int messageYPos = 0)
        {
            var boarder = SF.StringRepeater("~", BoxXLimit);
            
            SF.SetCursorAndDrawText(boarder, ItemXPosition, ItemYPosition);

            var currentXPos = ItemXPosition + 1;
            var currentYPos = messageYPos == 0 ? ItemYPosition + 2 : messageYPos;
            
            foreach (var letter in message)
            {
                if (currentXPos == ItemXPosition + BoxXLimit - 2)
                {
                    currentXPos = ItemXPosition + 1;
                    currentYPos += 2;
                }
                
                SF.SetCursorAndDrawText($"{letter}", currentXPos, currentYPos);
                currentXPos += 1;
            }
            
            SF.SetCursorAndDrawText(boarder, ItemXPosition, 
                currentYPos >= ItemYPosition + BoxYLimit - 1 ? currentYPos + 2 : ItemYPosition + BoxYLimit);

            return (currentXPos, currentYPos);
        }
    }
}