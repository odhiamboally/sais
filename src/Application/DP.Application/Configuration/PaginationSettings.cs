using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Configuration;
public record PaginationSettings
{
    public int DefaultPageSize { get; init; } = 2;
    public int MaxPageSize { get; init; } = 100; 
    public bool IsLastPage { get; init; } = false;
    public bool IsFirstPage { get; init; } = false;

    public int? PreviousPage { get; init; } = 0;
    public int? CurrentPage { get; init; } = 0;
    public int? NextPage { get; init; } = 0;

    public int? Cursor { get; init; } = 0;
    public int? PreviousCursor { get; init; }
    public int? NextCursor { get; init; }
    public int? LastCursor { get; init; }
}
