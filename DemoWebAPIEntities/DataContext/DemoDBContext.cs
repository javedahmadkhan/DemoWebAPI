//
// Copyright:   Copyright (c) 
//
// Description: Demo DB Context Class
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
    /// This class is used for performing Database context methods
    /// </summary>
    public class DemoDBContext : DbContext
    {
        /// <summary>
        /// Constructor for DemoDBContext class
        /// </summary>
        public DemoDBContext()
        {
        }

        /// <summary>
        /// Constructor for DemoDBContext class
        /// </summary>
        /// <param name="options">DbContext Options</param>
        public DemoDBContext(DbContextOptions<DemoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItem { get; set; }

        /// <summary>
        /// On Model Creating Method
        /// </summary>
        /// <param name="modelBuilder">Model Builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
