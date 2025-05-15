using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Configuration;
public sealed class CacheSettings
{
    public string? CacheKeyPrefix { get; set; }
    public string CacheType { get; set; } = "InMemory";
    public RedisSettings? Redis { get; set; }
    public AzureCacheSettings? Azure { get; set; }
    public ElastiCacheSettings? Aws { get; set; }
}
