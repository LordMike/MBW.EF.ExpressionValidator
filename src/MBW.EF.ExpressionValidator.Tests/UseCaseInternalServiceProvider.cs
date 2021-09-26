using System;
using System.Collections.Generic;
using System.Linq;
using MBW.EF.ExpressionValidator.Extensions;
using MBW.EF.ExpressionValidator.PomeloMysql;
using MBW.EF.ExpressionValidator.Sqlite;
using MBW.EF.ExpressionValidator.SqlServer;
using MBW.EF.ExpressionValidator.Tests.TestContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MBW.EF.ExpressionValidator.Tests
{
    public class UseCaseInternalServiceProvider : IDisposable
    {
        private readonly Context _db;
        private readonly ServiceProvider _sp;

        public UseCaseInternalServiceProvider()
        {
            // Use case: Using a custom service provider, and not letting EF create it
            _sp = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddExpressionValidator()
                .BuildServiceProvider();

            Context ctx = new Context(new DbContextOptionsBuilder<Context>()
                .UseInternalServiceProvider(_sp)
                .UseInMemoryDatabase(nameof(UseCaseInternalServiceProvider))
                .AddSqliteExpressionValidation()
                .AddSqlServerExpressionValidation()
                .AddMysqlExpressionValidation()
                .Options);

            _db = ctx;
            _db.Database.EnsureCreated();
        }

        /// <summary>
        /// Tests a valid expression that will work in all databases
        /// </summary>
        [Fact]
        public void TestValidQuery()
        {
            List<BlogPost> items = _db.BlogPosts.Where(s => s.Id == 1).ToList();
            Assert.Single(items);
        }

        private bool LocalFunction(BlogPost post) => true;

        /// <summary>
        /// Tests a query that will not work in any database, as it uses a local C# function which cannot be translated
        /// </summary>
        [Fact]
        public void TestInvalidQuery()
        {
            ExpressionValidationException exception = Assert.Throws<ExpressionValidationException>(() => _db.BlogPosts.Where(x => LocalFunction(x)).ToList());
            Assert.Equal(3, exception.DatabaseTargets.Length);
            Assert.All(exception.InnerExceptions, e => Assert.IsType<InvalidOperationException>(e));
        }

        public void Dispose()
        {
            _sp?.Dispose();
        }
    }
}