using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace Demo.BusinessLogic.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class MappingExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static PropertyBuilder<TProperty> HasPrecision<TProperty>(this PropertyBuilder<TProperty> source, byte precision, byte scale)
        {
            string decimalStr = $"decimal({precision},{scale})";
            return source.HasColumnType(decimalStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TRelatedEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="navExpression"></param>
        /// <returns></returns>
        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithOptional<TEntity, TRelatedEntity>(this CollectionNavigationBuilder<TEntity, TRelatedEntity> source, Expression<Func<TRelatedEntity, TEntity>> navExpression)
        where TEntity : class
        where TRelatedEntity : class
        {
            return source.WithOne(navExpression).IsRequired(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TRelatedEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="navExpression"></param>
        /// <returns></returns>
        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithRequired<TEntity, TRelatedEntity>(this CollectionNavigationBuilder<TEntity, TRelatedEntity> source, Expression<Func<TRelatedEntity, TEntity>> navExpression)
        where TEntity : class
        where TRelatedEntity : class
        {
            return source.WithOne(navExpression).IsRequired(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TRelatedEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="isCascade"></param>
        /// <returns></returns>
        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WillCascadeOnDelete<TEntity, TRelatedEntity>(this ReferenceCollectionBuilder<TEntity, TRelatedEntity> source, bool isCascade = true)
        where TEntity : class
        where TRelatedEntity : class
        {
            return isCascade == true ? source.OnDelete(DeleteBehavior.Cascade) : source.OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> MapOnlyIfChanged<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map)
        {
            map.ForAllMembers(source =>
            {
                source.Condition((sourceObject, destObject, sourceProperty, destProperty) =>
                {
                    if (sourceProperty == null)
                    {
                        bool flag1 = destProperty != null;
                        return flag1;
                    }

                    bool flag = !sourceProperty.Equals(destProperty);
                    return flag;
                });
            });
            return map;
        }
    }
}
