using Calc.Core.Interfaces;
using System.Data;
using System.Globalization;

namespace Calc.Infrastructure.Services;

public class CalculatorService : ICalculatorService
{
    public double EvaluateExpression(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return 0;

        try
        {
            // Replace % with /100. 
            // Note: This performs simple division (e.g., 50% -> 0.5)
            string sanitizedInput = input.Replace("%", "/100");

            var dt = new DataTable();
            // Force InvariantCulture to handle "." correctly in all regions
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            
            var result = dt.Compute(sanitizedInput, "");

            return Convert.ToDouble(result, CultureInfo.InvariantCulture);
        }
        catch
        {
            return 0; 
        }
    }
}
