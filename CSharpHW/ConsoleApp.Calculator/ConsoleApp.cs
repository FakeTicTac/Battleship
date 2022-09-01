using Controller.Calculator;


namespace ConsoleApp.Calculator
{
    
    /// <summary>
    /// Calculator Console App.
    /// </summary>
    internal static class ConsoleApp
    {

        /// <summary>
        /// Calculator Console Startup.
        /// </summary>
        private static void Main() => new CalculatorController().MainMenu();
    }
}