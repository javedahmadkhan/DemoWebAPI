//
// Copyright:   Copyright (c) 
//
// Description: Mapping Extension Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace Demo.BusinessLogic.Extensions
{
    /// <summary>
    /// This class is used for Mapping Extension
    /// </summary>
    public static class MappingExtension
    {
        /// <summary>
        /// Has Precision Method
        /// </summary>
        /// <typeparam name="TProperty">Property</typeparam>
        /// <param name="source">Source</param>
        /// <param name="precision">Precision</param>
        /// <param name="scale">Scale</param>
        /// <returns>Property Builder</returns>
        public static PropertyBuilder<TProperty> HasPrecision<TProperty>(this PropertyBuilder<TProperty> source, byte precision, byte scale)
        {
            string decimalStr = $"decimal({precision},{scale})";
            return source.HasColumnType(decimalStr);
        }

        /// <summary>
        /// With Optional Method
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <typeparam name="TRelatedEntity">Related Entity</typeparam>
        /// <param name="source">Source</param>
        /// <param name="navExpression">Navigation Expression</param>
        /// <returns>Reference Collection Builder</returns>
        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithOptional<TEntity, TRelatedEntity>(this CollectionNavigationBuilder<TEntity, TRelatedEntity> source, Expression<Func<TRelatedEntity, TEntity>> navExpression)
        where TEntity : class
        where TRelatedEntity : class
        {
            return source.WithOne(navExpression).IsRequired(false);
        }

        /// <summary>
        /// With Required Method
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <typeparam name="TRelatedEntity">Related Entity</typeparam>
        /// <param name="source">Source</param>
        /// <param name="navExpression">Navigation Expression</param>
        /// <returns>Reference Collection Builder</returns>
        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithRequired<TEntity, TRelatedEntity>(this CollectionNavigationBuilder<TEntity, TRelatedEntity> source, Expression<Func<TRelatedEntity, TEntity>> navExpression)
        where TEntity : class
        where TRelatedEntity : class
        {
            return source.WithOne(navExpression).IsRequired(true);
        }

        /// <summary>
        /// Will Cascade On Delete Method
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <typeparam name="TRelatedEntity">Related Entity</typeparam>
        /// <param name="source">Source</param>
        /// <param name="isCascade">Is Cascade</param>
        /// <returns>Reference Collection Builder</returns>
        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WillCascadeOnDelete<TEntity, TRelatedEntity>(this ReferenceCollectionBuilder<TEntity, TRelatedEntity> source, bool isCascade = true)
        where TEntity : class
        where TRelatedEntity : class
        {
            return isCascade == true ? source.OnDelete(DeleteBehavior.Cascade) : source.OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// Map Only If Changed Method
        /// </summary>
        /// <typeparam name="TSource">Source</typeparam>
        /// <typeparam name="TDestination">Destination</typeparam>
        /// <param name="map">Mapping Expression</param>
        /// <returns>Mapping Expression</returns>
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
