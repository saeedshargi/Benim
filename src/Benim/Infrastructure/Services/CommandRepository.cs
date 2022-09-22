﻿using Benim.Infrastructure.Interfaces;

namespace Benim.Infrastructure.Services;

public class CommandRepository<T>:ICommandRepository<T> where T: class
{
    public Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }
}