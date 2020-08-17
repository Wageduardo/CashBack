// ----------------------------------------
// <copyright file=BoticarioContext.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Api.Extensions.SecurityRoles;
using Boticario.CashBack.Models;
using Boticario.CashBack.Models.Interfaces.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boticario.CashBack.Repositories.Database
{
    public class BoticarioContext : DbContext
    {
        #region [ DbSet ]

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PreApproved> PreApproveds { get; set; }
        public DbSet<PercentageValuePromotion> PercentageValuePromotions { get; set; }

        #endregion [ DbSet ]

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="BoticarioContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BoticarioContext(DbContextOptions<BoticarioContext> options) : base(options)
        {
        }

        #endregion [ Constructors ]

        #region [ Protected Methods ]

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
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Email)
                .IsRequired();
                entity.Property(a => a.Cpf)
                .IsRequired();
                entity.HasIndex(a => a.Code)
                .IsUnique();
                entity.Property(a => a.Code)
                .IsRequired();
                entity.Property(a => a.Status)
                .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
               entity.HasKey(a => a.Id);
               entity.HasIndex(a => a.Email)
                .IsUnique();
                entity.HasIndex(a => a.Cpf)
                  .IsUnique();
               entity.Property(a => a.Password)
                .IsRequired();
               entity.Property(a => a.Name)
                .IsRequired();
               entity.Property(a => a.Role)
                .IsRequired();
            });

            modelBuilder.Entity<PreApproved>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.Cpf)
                .IsUnique();
                entity.Property(a => a.Cpf)
                .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        #endregion [ Protected Methods ]

        #region [ Public methods ]


        /// <summary>
        /// <para>
        /// Saves all changes made in this context to the database.
        /// </para>
        /// <para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// <para>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </para>
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        /// been sent successfully to the database.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task result contains the
        /// number of state entries written to the database.
        /// </returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            // set the ModifiedTime automatic
            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedTime").CurrentValue = DateTime.Now;
            });
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// <para>
        /// Saves all changes made in this context to the database.
        /// </para>
        /// <para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        /// been sent successfully to the database.</param>
        /// <returns>
        /// The number of state entries written to the database.
        /// </returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            // set the ModifiedTime automatic
            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            EditedEntities.ForEach(E =>
            {
                E.Property("ModifiedTime").CurrentValue = DateTime.Now;
            });
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        #endregion [ Public methods ]


        /// <summary>
        /// Creates the data base.
        /// </summary>
        public void InsertDataTest()
        {
            //include data seed
            if (Users.Count() == 0)
            {
                User admin = new User { Id = Guid.NewGuid(), Email = "admin@boticario.com.br", Password = "password".ToHashPassword(), Role = Roles.SolutionAdministrator, Name = "Jon doe", Cpf = "82548718093" };
                User admin1 = new User { Id = Guid.NewGuid(), Email = "admin1@boticario.com.br", Password = "password".ToHashPassword(), Role = Roles.SolutionAdministrator, Name = "Mary Jane", Cpf = "35116417050" };
                User admin2 = new User { Id = Guid.NewGuid(), Email = "admin2@boticario.com.br", Password = "password".ToHashPassword(), Role = Roles.SolutionAdministrator, Name = "Foo bar", Cpf = "68662320074" };

                User preApporved = new User { Id = Guid.NewGuid(), Email = "preapproved@boticario.com.br", Password = "password".ToHashPassword(), Role = Roles.OperationalUser, Name = "Boticario", Cpf = "15350946056" };
                User user = new User { Id = Guid.NewGuid(), Email = "user@boticario.com.br", Password = "password".ToHashPassword(), Role = Roles.OperationalUser, Name = "Maria joana", Cpf = "82177322057" };
                User user1 = new User { Id = Guid.NewGuid(), Email = "user1@boticario.com.br", Password = "password".ToHashPassword(), Role = Roles.OperationalUser, Name = "Mario brother", Cpf = "07406359055" };
                User user2 = new User { Id = Guid.NewGuid(), Email = "user2@boticario.com.br", Password = "password".ToHashPassword(), Role = Roles.OperationalUser, Name = "Jin Sakay", Cpf = "08498925037" };


                Users.Add(admin);
                Users.Add(admin1);
                Users.Add(admin2);

                Users.Add(preApporved);
                Users.Add(user);
                Users.Add(user1);
                Users.Add(user2);
                SaveChanges();
            }

            if (PercentageValuePromotions.Count() == 0)
            {
                PercentageValuePromotions.Add(new PercentageValuePromotion() { Value = 0, Percentage = 10 });
                PercentageValuePromotions.Add(new PercentageValuePromotion() { Value = 1000, Percentage = 15 });
                PercentageValuePromotions.Add(new PercentageValuePromotion() { Value = 1500, Percentage = 20 });
                SaveChanges();
            }

            if (PreApproveds.Count() == 0)
            {
                PreApproveds.Add(new PreApproved() { Id = Guid.NewGuid(), Cpf = "15350946056" });
                SaveChanges();
            }

            if (Purchases.Count() == 0)
            {
                var listUser = Users.Where(a => a.Role == Roles.OperationalUser);
                foreach (var u in listUser)
                {
                    for (int i = 0; i < new Random().Next(10, 30); i++)
                    {
                        var x = new Random().Next(500, 2000);
                        var y = new Random().NextDouble();
                        var value = Math.Round(x + y, 2);
                        var cashbackPercent = GetCashbackPercent(value);
                        var aux = Math.Round(cashbackPercent / 100.0f, 2);
                        var purchase = new Purchase()
                        {
                            Id = Guid.NewGuid(),
                            Email = u.Email,
                            Cpf = u.Cpf,
                            Code = Guid.NewGuid().ToString().Split("-").First(),
                            Status = (ECashbackStatus)new Random().Next(0, 3),
                            Value = value,
                            CashbackPercent = cashbackPercent,
                            CashbackValue = Math.Round(value * aux, 2)
                        };
                        Purchases.Add(purchase);
                    }
                }
                SaveChanges();
            }
        }

        /// <summary>
        /// This methods is just to create data for challenge
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private double GetCashbackPercent(double value)
        {
            foreach (var a in PercentageValuePromotions.OrderByDescending(t => t.Value))
            {
                if (value > a.Value)
                    return a.Percentage;
            }
            return 0;
        }
    }
}