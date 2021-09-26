using MBW.EF.ExpressionValidator.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MBW.EF.ExpressionValidator.Sqlite
{
    public static class OptionsbuilderExtensions
    {
        public static DbContextOptionsBuilder AddSqliteExpressionValidation<TContext>(this DbContextOptionsBuilder builder) where TContext : DbContext
        {
            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new SqliteValidator<TContext>()));
        }

        public static DbContextOptionsBuilder<TContext> AddSqliteExpressionValidation<TContext>(this DbContextOptionsBuilder<TContext> builder) where TContext : DbContext
        {
            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new SqliteValidator<TContext>()));
        }
    }
}
