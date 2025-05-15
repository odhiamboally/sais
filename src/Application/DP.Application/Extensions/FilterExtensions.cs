using DP.Application.Contracts.Implementations.ValueObjects;
using DP.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Extensions;


internal static class FilterExtensions
{
    public static List<IFilterCriteria> ToFilterCriteriaList(this Dictionary<string, string?> parameters)
    {
        var result = new List<IFilterCriteria>();

        foreach (var (key, rawValue) in parameters)
        {
            if (string.IsNullOrWhiteSpace(rawValue)) continue;

            var (fieldName, op) = ParseKeyOperator(key);
            result.Add(new FilterCriteria
            {
                FieldName = fieldName,
                Operator = op,
                Value = rawValue,
                LogicalConnector = "and" // default; can be extended
            });
        }

        return result;
    }

    /// <summary>
    /// Supports parsing keys like "StartDate__gt" into ("StartDate", "gt")
    /// If no operator suffix, defaults to "eq"
    /// </summary>
    private static (string fieldName, string op) ParseKeyOperator(string key)
    {
        if (key.Contains("__"))
        {
            var parts = key.Split("__", 2);
            return (parts[0], parts[1]);
        }

        return (key, "eq");
    }


}
