using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace Demo.BusinessLogic.Extensions
{
    public static class MappingExtension
    {
        public static PropertyBuilder<TProperty> HasPrecision<TProperty>(this PropertyBuilder<TProperty> source, byte precision, byte scale)
        {
            string decimalStr = $"decimal({precision},{scale})";
            return source.HasColumnType(decimalStr);
        }

        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithOptional<TEntity, TRelatedEntity>(this CollectionNavigationBuilder<TEntity, TRelatedEntity> source, Expression<Func<TRelatedEntity, TEntity>> navExpression)
        where TEntity : class
        where TRelatedEntity : class
        {
            return source.WithOne(navExpression).IsRequired(false);
        }

        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithRequired<TEntity, TRelatedEntity>(this CollectionNavigationBuilder<TEntity, TRelatedEntity> source, Expression<Func<TRelatedEntity, TEntity>> navExpression)
        where TEntity : class
        where TRelatedEntity : class
        {
            return source.WithOne(navExpression).IsRequired(true);
        }

        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WillCascadeOnDelete<TEntity, TRelatedEntity>(this ReferenceCollectionBuilder<TEntity, TRelatedEntity> source, bool isCascade = true)
        where TEntity : class
        where TRelatedEntity : class
        {
            return isCascade == true ? source.OnDelete(DeleteBehavior.Cascade) : source.OnDelete(DeleteBehavior.Restrict);
        }

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
