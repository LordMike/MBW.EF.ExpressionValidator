using MBW.EF.ExpressionValidator.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MBW.EF.ExpressionValidator.SqlServer
{
    public static class OptionsbuilderExtensions
    {
        public static DbContextOptionsBuilder AddSqlServerExpressionValidation<TContext>(this DbContextOptionsBuilder builder) where TContext : DbContext
        {
            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new SqlServerValidator<TContext>()));
        }

        public static DbContextOptionsBuilder<TContext> AddSqlServerExpressionValidation<TContext>(this DbContextOptionsBuilder<TContext> builder) where TContext : DbContext
        {
            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new SqlServerValidator<TContext>()));
        }
    }
}
