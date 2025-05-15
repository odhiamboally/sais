using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.Caching;
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    T? Get<T>(string key);

    Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration);
    void Set<T>(string key, T value, TimeSpan expiration);


    Task RemoveAsync(string key);
    void Remove(string key);
}
