using System;
using BLL.Menu.MenuEnums;
using BLL.Menu.MenuInterfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace BLL.Menu
{
    
    /// <summary>
    /// Implementation of Menu with all necessary Data.
    /// </summary>
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    public class Menu
    {
        
        /// <summary>
        /// String contains Title of the Menu.
        /// </summary>
        private string Title { get; }

        
        /// <summary>
        /// Defines Type of Current Menu.
        /// </summary>
        public EMenuType MenuType { get; }
        
        
        /// <summary>
        /// Defines Level of Current Menu.
        /// </summary>
        public EMenuLevel MenuLevel { get; }
        
        
        /// <summary>
        /// Show Currently Selected Menu Item in Menu Item List.
        /// </summary>
        public int XCursorPosition { get; private set; }
        
        
        /// <summary>
        /// List stores Menu Items that belong to this Menu in order of addition.
        /// </summary>
        public readonly List<IMenuItem> MenuItems = new();

        
        /// <summary>
        /// Indicator of Quantity of Reserved Menu Items in List.
        /// </summary>
        public int ReservedMenuItems { get; private set; }


        /// <summary>
        /// Menu basic constructor.
        /// </summary>
        /// <param name="title">Title of the Menu.</param>
        /// <param name="menuType">Type of the current Menu.</param>
        /// <param name="menuLevel">Level of the current Menu.</param>
        public Menu(string title, EMenuType menuType, EMenuLevel menuLevel)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title of Menu cannot be Empty.");

            Title = title;
            MenuType = menuType;
            MenuLevel = menuLevel;

            if (MenuType != EMenuType.Option) ReservedMenuItemFillerLevelBased();
            
            else ReservedMenuItemFillerTypeBased();
        }

        
        /// <summary>
        /// Fill Menu Items List with Reserved Menu Items based on Menu Level.
        /// </summary>
        private void ReservedMenuItemFillerLevelBased()
        {
            var mainMenuItem = new MenuItem<bool?>("Main Menu", () => null, EMenuItemAction.ToMainMenu);
            var returnMenuItem = new MenuItem<bool?>("Return", () => null, EMenuItemAction.Return);

            switch (MenuLevel)
            {
                case EMenuLevel.First:
                    MenuItems.Add(returnMenuItem);
                    break;
                case EMenuLevel.SecondOrMore:
                    MenuItems.Add(returnMenuItem);
                    MenuItems.Add(mainMenuItem);
                    break;
            }
            
            MenuItems.Add(new MenuItem<bool?>("Exit", () => null, EMenuItemAction.ToExit));
            ReservedMenuItems = MenuItems.Count;
        }
        
        
        /// <summary>
        /// Fill Menu Items List with Reserved Menu Items based on Menu Level.
        /// </summary>
        private void ReservedMenuItemFillerTypeBased()
        {
            AddMenuItems(new List<IMenuItem>
            {
                new MenuItem<bool?>("Continue", () => null, EMenuItemAction.Continue),
                new MenuItem<bool?>("Return", () => null, EMenuItemAction.Return)
            });
            ReservedMenuItems = MenuItems.Count;
        }
        
        
        /// <summary>
        /// Add Menu Item to certain position in Menu List.
        /// </summary>
        /// <param name="item">Menu Item to be added.</param>
        /// <param name="position">Position in Menu List for addition.</param>
        public void AddMenuItem(IMenuItem item, int? position = null)
        {
           //if (MenuItems.Any(x => x.Title == item.Title)) throw new ApplicationException("Menu Item Already Exist in the list.");

            MenuItems.Insert(position ?? MenuItems.Count - ReservedMenuItems , item);
        }
        
        
        /// <summary>
        /// Add Menu Items from list to tne Menu list.
        /// </summary>
        /// <param name="items">List of Menu Items to be added.</param>
        public void AddMenuItems(List<IMenuItem> items) => items.ForEach(x => AddMenuItem(x));

        
        /// <summary>
        /// Move Cursor Position from Current Position. Between Menu Items and Menu Items Options.
        /// </summary>
        /// <param name="cursorPosition">Position to move Cursor.</param>
        public void MoveCursorPosition((int, int) cursorPosition)
        {
            var (xPos, yPos) = cursorPosition;

            if (xPos < 0 || xPos > MenuItems.Count - 1)
                        throw new ArgumentException("Can't Move Outside the Menu Items!");

            MenuItems[xPos].MoveCursorPosition(yPos);
            XCursorPosition = xPos;
        }
        
        
        /// <summary>
        /// Execute All Menu Item Options to Get All values back.
        /// </summary>
        /// <returns>All Menu Item Options values.</returns>
        public List<dynamic?> TriggerChosenMenuItemOptions()
        {
            if (MenuType != EMenuType.Option) throw new ArgumentException("Cannot Trigger Options for non-Option Menu.");

            return (from item in MenuItems where item.MenuItemOptions.Count != 0 select item.IsHidden ? null : item.MenuItemOptions[item.YCursorPosition].ExecuteFunction()).ToList();
        }
        

        /// <summary>
        /// Return the deep Copy of Menu Items in Menu Item List.
        /// </summary>
        /// <returns>Return the Deep Copy of Menu Items in Menu Item List.</returns>
        public List<IMenuItem> CopyOfMenuItems() => MenuItems.ConvertAll(x => x.Clone());
        
        
        /// <summary>
        /// String implementation of Menu.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Title;
    }
}
