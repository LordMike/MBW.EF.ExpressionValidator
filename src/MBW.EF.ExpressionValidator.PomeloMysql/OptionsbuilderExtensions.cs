using MBW.EF.ExpressionValidator.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MBW.EF.ExpressionValidator.PomeloMysql
{
    public static class OptionsbuilderExtensions
    {
        public static DbContextOptionsBuilder AddMysqlExpressionValidation<TContext>(this DbContextOptionsBuilder builder) where TContext : DbContext
        {
            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new MysqlValidator<TContext>()));
        }

        public static DbContextOptionsBuilder<TContext> AddMysqlExpressionValidation<TContext>(this DbContextOptionsBuilder<TContext> builder) where TContext : DbContext
        {
            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new MysqlValidator<TContext>()));
        }
    }
}
