using System;

namespace BudgetManagerReportApp.Exceptions
{
    public class ParameterException(string parameterName)
        : Exception($"Need properly passed parameter '{parameterName}'.");
}
