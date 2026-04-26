using Calc.Core.Interfaces;
using System.Data;
using System.Globalization;

namespace Calc.Infrastructure.Services;

public class CalculatorService : ICalculatorService
{
    public double EvaluateExpression(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return 0;

        // Spara undan ursprunglig kultur för att återställa den senare
        var originalCulture = Thread.CurrentThread.CurrentCulture;

        try
        {
            // Sanera input: Ersätt % med /100 och se till att kommatecken blir punkter
            string sanitizedInput = input.Replace("%", "/100").Replace(",", ".");

            var dt = new DataTable();

            // Tvinga InvariantCulture under själva beräkningen
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var result = dt.Compute(sanitizedInput, "");

            return Convert.ToDouble(result, CultureInfo.InvariantCulture);
        }
        catch
        {
            // Vid fel i uträkningen (t.ex. division med noll), returnera 0
            return 0;
        }
        finally
        {
            // Återställ alltid kulturen så att resten av appen inte påverkas
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }
}