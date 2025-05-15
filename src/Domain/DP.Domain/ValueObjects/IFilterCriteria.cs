using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.ValueObjects;
public interface IFilterCriteria
{
    string FieldName { get; }
    string Operator { get; }
    object Value { get; }
    string LogicalConnector { get; }
}
