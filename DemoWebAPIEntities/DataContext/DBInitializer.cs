//
// Copyright:   Copyright (c) 
//
// Description: DB Initializer Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Microsoft.EntityFrameworkCore;

namespace Demo.Entities.DataContext
{
    /// <summary>
    /// This class is used for performaing Database Initialization methods
    /// </summary>
    public static class DBInitializer
    {
        /// <summary>
        /// Initialize Method
        /// </summary>
        /// <param name="context">Database context</param>
        public static void Initialize(DemoDBContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Seed Method
        /// </summary>
        /// <param name="context">Database context</param>
        private static void Seed(DemoDBContext context)
        {
            // Add seed initialize method
        }
    }
}
