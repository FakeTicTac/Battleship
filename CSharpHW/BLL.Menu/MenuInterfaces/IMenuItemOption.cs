

namespace BLL.Menu.MenuInterfaces
{
    
    /// <summary>
    /// Basic Implementation Interface for Menu Item Option.
    /// Used Mainly for List with Different Generics Creation.
    /// </summary>
    public interface IMenuItemOption
    {
        
        /// <summary>
        /// String contains Title of the Menu Item Option.
        /// </summary>
        string Title { get; }
        
        
        /// <summary>
        /// Func Executes method, which Should to be Executed when Menu Item Option is triggered.
        /// </summary>
        /// <returns>Execute method result. Can be any type base on Function output.</returns>
        dynamic? ExecuteFunction();
        
        
        /// <summary>
        /// Return the deep Copy of Menu Item Option.
        /// </summary>
        /// <returns>Return the Deep Copy of Menu Item Option.</returns>
        IMenuItemOption Clone();
    }
}