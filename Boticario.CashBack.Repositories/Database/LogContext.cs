// ----------------------------------------
// <copyright file=LogContext.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Models;
using Microsoft.EntityFrameworkCore;

namespace Boticario.CashBack.Repositories.Database
{
    public class LogContext : DbContext
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="LogContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
            CreateDataBase();
        } 

        #endregion [ Constructors ]

        #region [ Dbset ]

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public DbSet<Log> Logs { get; set; }


        #endregion [ Dbset ]

        #region [ Protected methods ]

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasKey(m => m.Id);
            base.OnModelCreating(modelBuilder);
        }


        #endregion [ Protected methods ]

        #region [ Private methods ]

        /// <summary>
        /// Creates the data base.
        /// </summary>
        public void CreateDataBase()
        {
            Database.EnsureCreated();
        } 

        #endregion [ MyRegion ]
    }
}
