using BLL.Menu;
using BLL.Menu.MenuEnums;
using BLL.Menu.MenuInterfaces;
using System.Collections.Generic;
using ConsoleUI.Calculator.Extensions;


namespace Controller.Calculator
{
    
    /// <summary>
    /// Class Describes Controller between UI and Logic.
    /// </summary>
    public class CalculatorController
    {

        /// <summary>
        /// Calculator Logic Connection Definition.
        /// </summary>
        private BLL.Calculator.Calculator Calculator { get; } = new();
        
        
        /// <summary>
        /// Calculator Start Up Menu.
        /// </summary>
        public void MainMenu()
        {
            var mainMenu = new Menu("Calculator Main Menu", EMenuType.Selection, EMenuLevel.Root);
            
            mainMenu.AddMenuItems(new List<IMenuItem>()
            {
                new MenuItem<Menu?>( "Binary Operations", SubmenuBinary),
                new MenuItem<Menu?>( "Unary Operations", SubmenuUnary),
            });
            mainMenu.AddMenuItems(new List<IMenuItem>()
            {
                new MenuItem<string>("Initialize Value", InitialValue),
                new MenuItem<string>("Erase Value", Erase)
            });

            new CalculatorMenuController(mainMenu, Calculator).Run();
        }
        
        
        /// <summary>
        /// Menu of Unary Mathematical Operations.
        /// </summary>
        /// <returns>String of User Choice.</returns>
        private Menu? SubmenuUnary()
        {
            var unaryMenu = new Menu("Unary Operations", EMenuType.Selection, EMenuLevel.First);
            
            unaryMenu.AddMenuItems(new List<IMenuItem>
            {
                new MenuItem<string>( "Negate", Negate),
                new MenuItem<string>( "Root", Root),
                new MenuItem<string>( "Square", Square),
                new MenuItem<string>("Abs", Abs),
                new MenuItem<string>("Erase Value", Erase)
            });

            return new CalculatorMenuController(unaryMenu, Calculator).Run();
        }
        
        
        /// <summary>
        /// Menu of Binary Mathematical Operations.
        /// </summary>
        /// <returns>String of User Choice.</returns>
        private Menu? SubmenuBinary()
        {
            var binaryMenu = new Menu("Binary Operations", EMenuType.Selection, EMenuLevel.First);
            
            binaryMenu.AddMenuItems(new List<IMenuItem>
            {
                new MenuItem<string>( "Plus", Add),
                new MenuItem<string>( "Minus", Subtract),
                new MenuItem<string>("Divide", Divide),
                new MenuItem<string>( "Multiply", Multiply),
                new MenuItem<string>( "Elevate", Elevate),
                new MenuItem<string>("Erase Value", Erase)
            });

            return new CalculatorMenuController(binaryMenu, Calculator).Run();
        }
        
        
        /// <summary>
        /// Adds Number to Current Calculation.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Add()
        {
            Calculator.Add(ConsoleUI.Calculator.CalculatorView.CalculationDisplayProcess("+", Calculator.CalculatorCurrentValue));
            return "";
        }
        

        /// <summary>
        /// Subtract Number From Current Calculation.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Subtract()
        {
            Calculator.Subtract(ConsoleUI.Calculator.CalculatorView.CalculationDisplayProcess("-", Calculator.CalculatorCurrentValue));
            return "";
        }

        
        /// <summary>
        /// Divide Current Calculation on Number.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Divide()
        {
            var userInput = ConsoleUI.Calculator.CalculatorView.CalculationDisplayProcess("/", Calculator.CalculatorCurrentValue);

            if (userInput == 0) 
                ConsoleUI.Calculator.CalculatorView.ErrorMessage("Cannot divide on 0.");
            else 
                Calculator.Divide(userInput);

            return "";
        }
        
        
        /// <summary>
        /// Multiply Current Calculation on Number.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Multiply()
        {
            Calculator.Multiply(ConsoleUI.Calculator.CalculatorView.CalculationDisplayProcess("*", Calculator.CalculatorCurrentValue));
            return "";
        }
        
        
        /// <summary>
        /// Elevate Current Calculation on Number.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Elevate()
        {
            Calculator.Elevate(ConsoleUI.Calculator.CalculatorView.CalculationDisplayProcess("^", Calculator.CalculatorCurrentValue));
            return "";
        }
        
        
        /// <summary>
        /// Negate Number Current Calculation.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Negate()
        {
            Calculator.Negate();
            return "";
        }
        
        
        /// <summary>
        /// Square Root of the Current Calculation.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Root()
        {
            if (Calculator.CalculatorCurrentValue < 0) ConsoleUI.Calculator.CalculatorView.ErrorMessage("Cannot Find Root of a Negative Number.");
            else Calculator.Root();

            return "";
        }


        /// <summary>
        /// Square of Current Calculation.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Square()
        {
            Calculator.Square();
            return "";
        }
        
        
        /// <summary>
        /// Abs of Current Calculation.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Abs()
        {
           Calculator.Abs();
           return "";
        }

        
        /// <summary>
        /// Set New Initial Value for Calculator.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string InitialValue()
        {
            Calculator.InitialValue(ConsoleUI.Calculator.CalculatorView.CalculationDisplayProcess("new", Calculator.CalculatorCurrentValue));
            return "";
        }
        
        
        /// <summary>
        /// Remove the Value of Calculator to Zero.
        /// </summary>
        /// <returns>Empty String if Calculation Succeeded.</returns>
        private string Erase()
        {
            Calculator.Erase();
            return "";
        }
    }
}