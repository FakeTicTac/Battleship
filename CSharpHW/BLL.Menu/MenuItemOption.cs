using System;
using BLL.Menu.MenuInterfaces;


namespace BLL.Menu
{
    
    /// <summary>
    /// Implementation of Menu Item Option with all necessary Data using Generics.
    /// </summary>
    /// <typeparam name="T">Generics which Describe Execution Method return Value.</typeparam>
    public class MenuItemOption<T> : IMenuItemOption
    {
        
        /// <summary>
        /// String contains Title of the Menu Item Option.
        /// </summary>
        public string Title { get; }
        

        /// <summary>
        /// Func contains method to be Executed when Menu Item Option is triggered.
        /// </summary>
        private Func<T> ExecuteMethod { get; }


        /// <summary>
        /// Basic Constructor for Menu Item Option.
        /// </summary>
        /// <param name="title">Title of the Menu Item Option.</param>
        /// <param name="executeMethod">Method that Should be Executed when Menu Item Option is triggered.</param>
        public MenuItemOption(string title, Func<T> executeMethod)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title of Menu Item Option cannot be Empty.");
            
            Title = title;
            ExecuteMethod = executeMethod;
        }
        
        
        /// <summary>
        /// Return the deep Copy of Menu Item Option.
        /// </summary>
        /// <returns>Return the Deep Copy of Menu Item Option.</returns>
        public IMenuItemOption Clone() => new MenuItemOption<T>(Title, ExecuteMethod);
     
        
        /// <summary>
        /// Func Executes method. which should to be Executed when Menu Item Option is triggered.
        /// </summary>
        /// <returns>Execute method result. Dynamic => T Generics.</returns>
        public dynamic? ExecuteFunction() => ExecuteMethod.Invoke();
        
        
        /// <summary>
        /// String implementation of Menu Item Option.
        /// </summary>
        /// <returns>Menu Item Option Title.</returns>
        public override string ToString() => Title;
    }
}