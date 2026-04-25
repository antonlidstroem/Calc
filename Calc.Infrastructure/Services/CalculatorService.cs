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
            // Hantera % genom att ersätta det med /100
            string sanitizedInput = input.Replace("%", "/100");

            // Vi använder DataTable.Compute som en säker och inbyggd .NET-motor 
            // för enkla matematiska uttryck.
            var dt = new DataTable();
            var result = dt.Compute(sanitizedInput, "");

            return Convert.ToDouble(result, CultureInfo.InvariantCulture);
        }
        catch
        {
            return 0; // Returnerar 0 vid felaktig syntax
        }
    }
}