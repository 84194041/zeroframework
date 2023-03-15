﻿using Microsoft.EntityFrameworkCore;
using ZeroFramework.DeviceCenter.Domain.Specifications;
using ZeroFramework.DeviceCenter.Domain.Specifications.Evaluators;
using ZeroFramework.DeviceCenter.Infrastructure.Specifications.Evaluators;
using ZeroFramework.DeviceCenter.Infrastructure.Specifications.Extensions;

namespace ZeroFramework.DeviceCenter.Infrastructure.Specifications.Extensions
{
    public static class DbSetExtensions
    {
        public static async Task<List<TSource>> ToListAsync<TSource>(this DbSet<TSource> source, ISpecification<TSource> specification, CancellationToken cancellationToken = default) where TSource : class
        {
            var result = await SpecificationEvaluator.Default.GetQuery(source, specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null
                ? result
                : specification.PostProcessingAction(result).ToList();
        }

        public static async Task<IEnumerable<TSource>> ToEnumerableAsync<TSource>(this DbSet<TSource> source, ISpecification<TSource> specification, CancellationToken cancellationToken = default) where TSource : class
        {
            var result = await SpecificationEvaluator.Default.GetQuery(source, specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null
                ? result
                : specification.PostProcessingAction(result);
        }

        public static IQueryable<TSource> WithSpecification<TSource>(this IQueryable<TSource> source, ISpecification<TSource> specification, ISpecificationEvaluator? evaluator = null) where TSource : class
        {
            evaluator ??= SpecificationEvaluator.Default;
            return evaluator.GetQuery(source, specification);
        }
    }
}
