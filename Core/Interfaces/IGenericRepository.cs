﻿using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<T>
{
    Task<T?> GetByIdAsync(int? id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsyncWithSpec(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}