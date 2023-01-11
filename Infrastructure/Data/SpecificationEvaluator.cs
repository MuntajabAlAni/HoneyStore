﻿using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity: class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;
        if (spec.Criteria is Expression<Func<TEntity, bool>>)
        {
            query = query.Where(spec.Criteria);
        }
            
        if (spec.OrderBy is Expression<Func<TEntity, object>>)
        {
            query = query.OrderBy(spec.OrderBy);
        }
            
        if (spec.OrderByDescending is Expression<Func<TEntity, object>>)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
            
        if(spec.IsPagingEnabled){
            query = query.Skip(spec.Skip).Take(spec.Take);
        }
        query = spec.Includes!.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }
}