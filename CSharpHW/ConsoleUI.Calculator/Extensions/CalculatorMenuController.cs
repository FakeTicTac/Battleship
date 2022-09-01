using MenuSystem;
using ConsoleUI.Menu;
using BLL.Menu.MenuEnums;


namespace ConsoleUI.Calculator.Extensions
{
    
    /// <summary>
    /// Class Describes Calculator Menu Controller -> Connection Between UI and Logic.
    /// </summary>
    public class CalculatorMenuController : MenuController
    {

        /// <summary>
        /// Calculator Reference.
        /// </summary>
        private BLL.Calculator.Calculator Calculator { get; }


        /// <summary>
        /// Menu Controller Basic Constructor.
        /// </summary>
        /// <param name="menu">Reference to Menu to be Displayed.</param>
        /// <param name="calculator">Reference to Calculator to be Displayed.</param>
        public CalculatorMenuController(BLL.Menu.Menu menu, BLL.Calculator.Calculator calculator) : base(menu)
                                                                                            => Calculator = calculator;


        /// <summary>
        /// Chooses Appropriate UI Solution for Menu Drawing Based on Menu Type.
        /// </summary>
        protected override void MenuDisplayChooser()
        {
            switch (Menu.MenuType)
            {
                case EMenuType.Selection:
                    CalculatorView.DisplayMenu(Menu, Calculator.CalculatorCurrentValue, 5);
                    break;
                case EMenuType.Option:
                    OptionMenuUI.DisplayMenu(Menu, 5);
                    break;
                default:
                    return;
            }
        }
    }
}