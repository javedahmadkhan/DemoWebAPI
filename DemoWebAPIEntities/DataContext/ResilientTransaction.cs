//
// Copyright:   Copyright (c) 
//
// Description: Resilient Transaction Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//

using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Demo.Entities.DataContext
{
    /// <summary>
    /// This class id used for performing Resilient Transaction
    /// </summary>
    public class ResilientTransaction
    {
        private readonly DbContext _context;

        /// <summary>
        /// Constructor for Resilient Transaction class
        /// </summary>
        /// <param name="context">Db Context</param>
        private ResilientTransaction(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// New Method
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <returns>Resilient Transaction</returns>
        public static ResilientTransaction New(DbContext context)
        {
            return new ResilientTransaction(context);
        }

        /// <summary>
        /// Execute Method
        /// </summary>
        /// <param name="action">Action</param>
        public async Task ExecuteAsync(Func<Task> action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                await action();
                await transaction.CommitAsync();
            });
        }
    }
}
