using DP.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Persistence.SQLServer.Extensions;

internal static class FilterExtensions
{
    public static string BuildFilterString(this List<IFilterCriteria> filters)
    {
        if (filters == null || !filters.Any())
            return string.Empty;

        var filterParts = filters.Select(f =>
            $"{f.FieldName} {f.Operator} {FormatFilterValue(f.Value)}");

        return string.Join(" and ", filterParts);
    }

    private static string FormatFilterValue(object value)
    {
        if (value == null)
            return "null";

        return value switch
        {
            string s => $"'{s}'",
            DateTime dt => $"'{dt:yyyy-MM-ddTHH:mm:ss}'",
            DateTimeOffset dto => $"'{dto:yyyy-MM-ddTHH:mm:sszzz}'",
            _ => value.ToString() ?? string.Empty
        };
    }

    
}
