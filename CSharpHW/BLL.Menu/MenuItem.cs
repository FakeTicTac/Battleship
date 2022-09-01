using System;
using System.Linq;
using BLL.Menu.MenuEnums;
using BLL.Menu.MenuInterfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace BLL.Menu
{
    
    /// <summary>
    /// Implementation of Menu Item with all necessary Data using Generics.
    /// </summary>
    /// <typeparam name="T">Generics Describe Execution Method return Value.</typeparam>
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    public class MenuItem<T> : IMenuItem
    {
        
        /// <summary>
        /// String contains Title of the Menu Item.
        /// </summary>
        public string Title { get; }
        
        
        /// <summary>
        /// Indicator of Item Visibility.
        /// </summary>
        public bool IsHidden { get; set; }
        
        
        /// <summary>
        /// Func contains method to be Executed when Menu Item is triggered.
        /// </summary>
        private Func<T> ExecuteMethod { get; }
        
        
        /// <summary>
        /// Defines Type of Action Menu Item Holds.
        /// </summary>
        public EMenuItemAction MenuItemAction { get; }
        
        
        /// <summary>
        /// Show Selected Menu Item Option in Menu Item Option List.
        /// </summary>
        public int YCursorPosition { get; private set; }
        
        
        /// <summary>
        /// List of Menu Item Options belonging to Current Menu Item.
        /// </summary>
        public List<IMenuItemOption> MenuItemOptions { get; }


        /// <summary>
        /// Basic constructor for Menu Item.
        /// </summary>
        /// <param name="title">Title of the Menu Item.</param>
        /// <param name="executeMethod">Method that should be executed when Menu Item is triggered.</param>
        /// <param name="menuItemAction">Type of action menu Item holds.</param>
        public MenuItem(string title, Func<T> executeMethod, EMenuItemAction menuItemAction = EMenuItemAction.Accept)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title cannot be Empty.");
            
            Title = title;
            IsHidden = false;
            ExecuteMethod = executeMethod;
            MenuItemAction = menuItemAction;
            MenuItemOptions = new List<IMenuItemOption>();
        }
        
        
        /// <summary>
        /// Add Menu Item Option to certain position in Menu Item Option List.
        /// </summary>
        /// <param name="item">Menu Item Option to be Added.</param>
        /// <param name="position">Position in Menu List for addition.</param>
        public void AddMenuItemOption(IMenuItemOption item, int? position = null)
        {
            if (MenuItemOptions.Any(x => x.Title == item.Title))
                throw new ApplicationException("Menu Item Option Already Exist in the List.");
            
            MenuItemOptions.Insert(position ?? MenuItemOptions.Count, item);
        }
        
        
        /// <summary>
        /// Add Menu Item Options from List to tne Menu Item Option List.
        /// </summary>
        /// <param name="items">List of Menu Item Options to be added.</param>
        public void AddMenuItemOptions(List<IMenuItemOption> items) 
                                        => items.ForEach(x => AddMenuItemOption(x));

        
        /// <summary>
        /// Func Executes method. which should to be Executed when Menu Item is triggered.
        /// </summary>
        /// <returns>Execute method result. Dynamic => T Generics.</returns>
        public dynamic? ExecuteFunction() => ExecuteMethod.Invoke();
        
        
        /// <summary>
        /// Move Cursor Position from current Position. Between Menu Items and Menu Items Options.
        /// </summary>
        /// <param name="yCursorPosition">Y CoordinatePosition to move cursor.</param>
        public void MoveCursorPosition(int yCursorPosition)
        {
            if (yCursorPosition < 0 || yCursorPosition > MenuItemOptions.Count)
                throw new ArgumentException("Can't Move Outside the Menu Item Options!");
            
            YCursorPosition = yCursorPosition;
        }


        /// <summary>
        /// Return the deep Copy of Menu Item.
        /// </summary>
        /// <returns>Return the Deep Copy of Menu Item.</returns>
        public IMenuItem Clone()
        {
            var menuItemClone = new MenuItem<T>(Title, ExecuteMethod, MenuItemAction)
            {
                YCursorPosition = YCursorPosition,
                IsHidden = IsHidden
            };
            MenuItemOptions.ForEach(x => menuItemClone.AddMenuItemOption(x.Clone()));

            return menuItemClone;
        }
        
        
        /// <summary>
        /// Change Current Item Visibility.
        /// </summary>
        public void Hide() => IsHidden = !IsHidden;
        
        
        /// <summary>
        /// Triggers Chosen Menu Item Option Function.
        /// </summary>
        public void triggerOnChose() => MenuItemOptions[YCursorPosition].ExecuteFunction();
        

        /// <summary>
        /// String implementation of Menu Item.
        /// </summary>
        /// <returns>Menu Item Title.</returns>
        public override string ToString() => Title;
    }
}