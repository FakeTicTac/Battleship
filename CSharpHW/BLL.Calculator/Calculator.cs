using System;


namespace BLL.Calculator
{
    
    /// <summary>
    /// Implementation of Calculator with all necessary Data.
    /// </summary>
    public class Calculator
    {

        /// <summary>
        /// Calculation value.
        /// </summary>
        public double CalculatorCurrentValue;
        
        
        /// <summary>
        /// Adds number to Current Calculation.
        /// </summary>
        /// <param name="value">Value To Be Added to Calculation.</param>
        public void Add(double value) => CalculatorCurrentValue += value;
            
        
        /// <summary>
        /// Subtract number to Current Calculation.
        /// </summary>
        /// <param name="value">Value To Be Subtracted to Calculation.</param>
        public void Subtract(double value) => CalculatorCurrentValue -= value;

        
        /// <summary>
        /// Divide number to Current Calculation.
        /// </summary>
        /// <param name="value">Value To Be Divided to Calculation.</param>
        /// <exception cref="ArgumentException">Division on Zero is Impossible.</exception>
        public void Divide(double value)
        {
            if (value == 0) throw new ArgumentException("Division on Zero is Impossible.");
            
            CalculatorCurrentValue /= value;
        }
        
        
        /// <summary>
        /// Multiply Current Calculation on Number.
        /// </summary>
        /// <param name="value">Value To Be Multiplied to Calculation.</param>
        public void Multiply(double value) => CalculatorCurrentValue *= value;


        /// <summary>
        /// Elevate Current Calculation on Number.
        /// </summary>
        /// <param name="value">Value as Indicator of Power.</param>
        public void Elevate(double value) => CalculatorCurrentValue = Math.Pow(CalculatorCurrentValue, value);


        /// <summary>
        /// Negate Number Current Calculation.
        /// </summary>
        public void Negate() => CalculatorCurrentValue = -CalculatorCurrentValue;


        /// <summary>
        /// Square Root of the Current Calculation.
        /// </summary>
        /// <exception cref="ArgumentException">Cannot Find Root of a Negative Number.</exception>
        public void Root()
        {
            if (CalculatorCurrentValue < 0) throw new ArgumentException("Cannot Find Root of a Negative Number.");
            
            CalculatorCurrentValue = Math.Sqrt(CalculatorCurrentValue);
        }


        /// <summary>
        /// Square of Current Calculation.
        /// </summary>
        public void Square() => CalculatorCurrentValue = Math.Pow(CalculatorCurrentValue, 2);


        /// <summary>
        /// Abs of Current Calculation.
        /// </summary>
        public void Abs() => CalculatorCurrentValue = Math.Abs(CalculatorCurrentValue);


        /// <summary>
        /// Set New Initial Value for Calculator.
        /// </summary>
        /// <param name="value">Value to be Set as Initial.</param>
        public void InitialValue(double value) => CalculatorCurrentValue = value;
        
        
        /// <summary>
        /// Remove the Value of Calculator to Zero.
        /// </summary>
        public void Erase() => CalculatorCurrentValue = 0;
    }
}