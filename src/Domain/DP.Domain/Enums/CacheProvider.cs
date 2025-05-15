using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Enums;
public enum CacheProvider
{
    Redis,
    InMemory,
    SQLServer,
    Azure,
    Aws
}
