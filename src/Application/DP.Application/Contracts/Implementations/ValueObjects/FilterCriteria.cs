using DP.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Implementations.ValueObjects;
internal sealed class FilterCriteria : IFilterCriteria
{
    public FilterCriteria()
    {

    }

    public string FieldName { get; set; } = string.Empty;
    public string Operator { get; set; } = "eq";
    public object Value { get; set; } = new();
    public string LogicalConnector { get; set; } = "and";
}
