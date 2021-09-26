using System.Linq.Expressions;
using MBW.EF.ExpressionValidator.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace MBW.EF.ExpressionValidator.Validatiom
{
    public abstract class CommonExpressionValidator<TContext> : ExpressionValidatorBase where TContext : DbContext
    {
        private readonly IDatabase _db;
        private readonly RewritingExpressionVisitor<TContext> _expressionVisitor;

        public CommonExpressionValidator(string databaseKind) : base(databaseKind)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddDbContext<TContext>(Configure)
                .BuildServiceProvider();

            TContext dbContext = serviceProvider.GetRequiredService<TContext>();
            _expressionVisitor = new RewritingExpressionVisitor<TContext>(dbContext);
            _db = dbContext
                .GetInfrastructure()
                .GetRequiredService<IDatabase>();
        }

        protected abstract void Configure(DbContextOptionsBuilder dbContextOptionsBuilder);

        public sealed override void CompileQuery<TResult>(Expression query)
        {
            query = _expressionVisitor.Visit(query);
            _db.CompileQuery<TResult>(query, false);
        }
    }
}