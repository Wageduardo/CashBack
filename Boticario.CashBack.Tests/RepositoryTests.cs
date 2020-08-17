// ----------------------------------------
// <copyright file=RepositoryTests.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Api.Extensions.SecurityRoles;
using Boticario.CashBack.Models;
using Boticario.CashBack.Repositories.Database;
using Boticario.CashBack.Tests.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boticario.CashBack.Repositories.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class RepositoryTests
    {
        #region [ Fields ]

        private BoticarioContext context;
        private Repository<User> userRepository;

        #endregion [ Fields ]

        #region [ Arrange ]

        [SetUp()]
        public void Initiliaze()
        {
            var options = new DbContextOptionsBuilder<BoticarioContext>()
                .UseSqlite($"Data Source = {Guid.NewGuid().ToString()}.db")
                .Options;



            context = new BoticarioContext(options);
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DELETE FROM Users");
            userRepository = new Repository<User>(context);

            var entity = new EntityBase();
            entity.SetAllProperties();
            entity.GetPropertyValues();
            entity.ToString();
        }

        [TearDown()]
        public void TearDown()
        {
            var db = context.Database;
            context.Dispose();
            db.EnsureDeleted();
        }

        #endregion [ Arrange ]

        #region [ Tests ]

        [Test()]
        public void InsertdataTest()
        {
            context.InsertDataTest();
            Assert.That(context.Users.Count(), Is.GreaterThan(0));
            Assert.That(context.Purchases.Count(), Is.GreaterThan(0));
        }

        [Test()]
        public void GetAllTest()
        {
            var empty = userRepository.GetAll();
            Assert.That(empty, Is.Empty);
            var user1 = new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" };
            userRepository.Add(user1);
            var has1 = userRepository.GetAll();
            Assert.That(has1, Has.Count.EqualTo(1));
            var user2 = new User() { Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" };
            userRepository.Add(user2);
            var has2 = userRepository.GetAll();
            Assert.That(has2, Has.Count.EqualTo(2));
        }

        [Test()]
        public async Task GetAllAsyncTest()
        {
            var empty = await userRepository.GetAllAsync();
            Assert.That(empty, Is.Empty);
            var user1 = new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" };
            await userRepository.AddAsync(user1);
            var has1 = await userRepository.GetAllAsync();
            Assert.That(has1, Has.Count.EqualTo(1));
            var user2 = new User() { Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" };
            await userRepository.AddAsync(user2);
            var has2 = await userRepository.GetAllAsync();
            Assert.That(has2, Has.Count.EqualTo(2));
        }

        [Test()]
        public async Task GetByIdTest()
        {
            Assert.That(userRepository.GetById(Guid.NewGuid()), Is.Null);
            var user1 = await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });
            var user = userRepository.GetById(user1.Id);
            Assert.That(user, Is.SameAs(user1));
        }

        [Test()]
        public async Task GetByIdAsyncTest()
        {
            Assert.That(await userRepository.GetByIdAsync(Guid.NewGuid()), Is.Null);
            var user1 = await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });
            var user = await userRepository.GetByIdAsync(user1.Id);
            Assert.That(user, Is.SameAs(user1));
        }

        [Test()]
        public async Task FindTest()
        {
            var id = Guid.NewGuid();
            Assert.That(userRepository.Find(t => t.Id == id), Is.Null);
            var user1 = await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });
            var user = userRepository.Find(t => t.Id == user1.Id);
            Assert.That(user, Is.SameAs(user1));
        }

        [Test()]
        public async Task FindAsyncTest()
        {
            var id = Guid.NewGuid();
            Assert.That(await userRepository.FindAsync(t => t.Id == id), Is.Null);
            var user1 = await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });
            var user = await userRepository.FindAsync(t => t.Id == user1.Id);
            Assert.That(user, Is.SameAs(user1));
        }

        [Test()]
        public async Task FindAllTest()
        {
            Assert.That(userRepository.FindAll(t => t.Email == "2"), Is.Empty);

            await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" });

            Assert.That(userRepository.FindAll(t => t.Email == "2"), Is.Empty);

            await userRepository.AddAsync(new User() { Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123457" });

            Assert.That(userRepository.FindAll(t => t.Email == "test@test.com"), Has.Count.EqualTo(1));

            await userRepository.AddAsync(new User() { Email = "test2@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123458" });

            Assert.That(userRepository.FindAll(t => t.Password == "password"), Has.Count.EqualTo(3));
        }

        [Test()]
        public async Task FindAllAsyncTest()
        {
            Assert.That(await userRepository.FindAllAsync(t => t.Email == "2"), Is.Empty);

            await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });

            Assert.That(await userRepository.FindAllAsync(t => t.Email == "2"), Is.Empty);

            await userRepository.AddAsync(new User() { Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" });

            Assert.That(await userRepository.FindAllAsync(t => t.Email == "test@test.com"), Has.Count.EqualTo(1));

            await userRepository.AddAsync(new User() { Email = "test2@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "1234567" });

            Assert.That(await userRepository.FindAllAsync(t => t.Password == "password"), Has.Count.EqualTo(3));
        }

        [Test()]
        public async Task UpdateTest()
        {
            var user = await userRepository.AddAsync(new User() { Email = "test2@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });
            user.Role = Roles.ReadOnly;
            userRepository.Update(user, user.Id);
            Assert.That(userRepository.GetById(user.Id), Has.Property("Role").EqualTo(Roles.ReadOnly));
        }

        [Test()]
        public async Task UpdateAsyncTest()
        {
            var user = await userRepository.AddAsync(new User() { Email = "test2@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });
            user.Role = Roles.ReadOnly;
            await userRepository.UpdateAsync(user, user.Id);
            Assert.That(await userRepository.GetByIdAsync(user.Id), Has.Property("Role").EqualTo(Roles.ReadOnly));
        }

        [Test()]
        public async Task DeleteTest()
        {
            var user = await userRepository.AddAsync(new User() { Email = "test2@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });
            userRepository.Delete(user);
            Assert.That(userRepository.GetById(user.Id), Is.Null);
        }

        [Test()]
        public async Task DeleteAsyncTest()
        {
            var user = await userRepository.AddAsync(new User() { Email = "test2@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" });
            await userRepository.DeleteAsync(user);
            Assert.That(await userRepository.GetByIdAsync(user.Id), Is.Null);
        }

        [Test()]
        public async Task CountTest()
        {
            Assert.That(userRepository.Count(), Is.EqualTo(0));

            await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });

            Assert.That(userRepository.Any(a => a.Email == "test@test.com"), Is.True);
            Assert.That(await userRepository.AnyAsync(a => a.Email == "test@test.com"), Is.True);

            Assert.That(userRepository.GetFirst(), Is.Not.Null);
            Assert.That(await userRepository.GetFirstAsync(), Is.Not.Null);

            Assert.That(userRepository.Count(), Is.EqualTo(1));

            await userRepository.AddAsync(new User() { Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" });

            Assert.That(userRepository.Count(), Is.EqualTo(2));

            Assert.That(userRepository.Count(t => t.Email == "test1@test.com"), Is.EqualTo(1));
        }

        [Test()]
        public async Task CountAsyncTest()
        {
            Assert.That(await userRepository.CountAsync(), Is.EqualTo(0));

            await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });

            Assert.That(await userRepository.CountAsync(), Is.EqualTo(1));

            await userRepository.AddAsync(new User() { Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" });

            Assert.That(await userRepository.CountAsync(), Is.EqualTo(2));

            Assert.That(await userRepository.CountAsync(t => t.Email == "test1@test.com"), Is.EqualTo(1));
        }

        [Test()]
        public async Task AddRangeTest()
        {
            userRepository.AddRange(new List<User> { new User() { Id = Guid.NewGuid(), Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" },
                                                     new User() { Id = Guid.NewGuid(), Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" }
                                                    });
            Assert.That(await userRepository.CountAsync(), Is.EqualTo(2));
            Assert.That(() =>
            {
                userRepository.AddRange(null);
            }, Throws.ArgumentNullException);
        }

        [Test()]
        public async Task AddRangeAsyncTest()
        {
            await userRepository.AddRangeAsync(new List<User> { new User() { Id = Guid.NewGuid(), Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" },
                                               new User() { Id = Guid.NewGuid(), Email = "test1@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" } });
            Assert.That(await userRepository.CountAsync(), Is.EqualTo(2));
            Assert.That(async () =>
            {
                await userRepository.AddRangeAsync(null);
            }, Throws.ArgumentNullException);
        }

        [Test()]
        public async Task ExistsTest()
        {
            Assert.That(userRepository.Exists(Guid.NewGuid()), Is.False);

            var user = await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "123456" });

            Assert.That(userRepository.Exists(user.Id), Is.True);
        }

        [Test()]
        public async Task ExistsAsyncTest()
        {
            Assert.That(await userRepository.ExistsAsync(Guid.NewGuid()), Is.False);

            var user = await userRepository.AddAsync(new User() { Email = "test@test.com", Password = "password", Name = "test", Role = Roles.OperationalUser, Cpf = "12345" });

            Assert.That(await userRepository.ExistsAsync(user.Id), Is.True);
        }

        [Test()]
        public void ConstructorsTest()
        {
            var userRepo = new UserRepository(context);
            Assert.That(userRepo, Is.Not.Null);
            var purchaseRepo = new PurchaseRepository(context);
            Assert.That(purchaseRepo, Is.Not.Null);
        } 

        #endregion [ Tests ]
    }
}