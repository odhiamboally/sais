﻿using DP.Application.Contracts.Abstractions.Caching;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Implementations.Caching;
internal sealed class AzureCacheService : ICacheService
{
    public AzureCacheService()
    {
            
    }

    public T Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetAsync<T>(string key)
    {
        throw new NotImplementedException();
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string key)
    {
        throw new NotImplementedException();
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration)
    {
        throw new NotImplementedException();
    }
}
