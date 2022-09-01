using BLL.Menu.MenuEnums;
using System.Collections.Generic;


namespace BLL.Menu.MenuInterfaces
{
    
    /// <summary>
    /// Basic Implementation Interface for Menu Item.
    /// Used Mainly for List with Different Generics Creation.
    /// </summary>
    public interface IMenuItem
    {
        
        /// <summary>
        /// String contains Title of the Menu Item.
        /// </summary>
        string Title { get; }
        
        
        /// <summary>
        /// Indicator of Item Visibility.
        /// </summary>
        bool IsHidden { get; set; }
        
        
        /// <summary>
        /// Show Selected Menu Item Option in Menu Item Option List.
        /// </summary>
        int YCursorPosition { get; }
        
        
        /// <summary>
        /// Defines Type of Action Menu Item Holds.
        /// </summary>
        EMenuItemAction MenuItemAction { get; }
        
        
        /// <summary>
        /// List of Menu Item Options belonging to Current Menu Item.
        /// </summary>
        List<IMenuItemOption> MenuItemOptions { get; }


        /// <summary>
        /// Change Current Item Visibility.
        /// </summary>
        void Hide();


        /// <summary>
        /// Move Cursor Position from current Position. Between Menu Items and Menu Items Options.
        /// </summary>
        /// <param name="yCursorPosition">Y CoordinatePosition to move cursor.</param>
        void MoveCursorPosition(int yCursorPosition);
        
        
        /// <summary>
        /// Func Executes method. which should to be Executed when Menu Item Option is triggered.
        /// </summary>
        /// <returns>Execute method result. Can be any type base on Function.</returns>
        dynamic? ExecuteFunction();


        /// <summary>
        /// Triggers Chosen Menu Item Option Function.
        /// </summary>
        void triggerOnChose();

        
        /// <summary>
        /// Return the deep Copy of Menu Item.
        /// </summary>
        /// <returns>Return the Deep Copy of Menu Item.</returns>
        IMenuItem Clone();
    }
}