using System;
using MBW.EF.ExpressionValidator.Extensions;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MBW.EF.ExpressionValidator.PomeloMysql
{
    public static class OptionsbuilderExtensions
    {
        private static readonly ServerVersion DefaultMySql = new MySqlServerVersion(new Version(8, 0, 25));
        private static readonly ServerVersion DefaultMariaDb = new MariaDbServerVersion(new Version(10, 6, 0));

        public static DbContextOptionsBuilder AddMysqlExpressionValidation<TContext>(this DbContextOptionsBuilder builder, string serverVersion = null) where TContext : DbContext
        {
            ServerVersion version = DefaultMySql;
            if (serverVersion != null)
                version = ServerVersion.Parse(serverVersion, ServerType.MySql);

            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new MysqlValidator<TContext>(version)));
        }

        public static DbContextOptionsBuilder<TContext> AddMysqlExpressionValidation<TContext>(this DbContextOptionsBuilder<TContext> builder, string serverVersion = null) where TContext : DbContext
        {
            ServerVersion version = DefaultMySql;
            if (serverVersion != null)
                version = ServerVersion.Parse(serverVersion, ServerType.MySql);

            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new MysqlValidator<TContext>(version)));
        }

        public static DbContextOptionsBuilder AddMariaDbExpressionValidation<TContext>(this DbContextOptionsBuilder builder, string serverVersion = null) where TContext : DbContext
        {
            ServerVersion version = DefaultMariaDb;
            if (serverVersion != null)
                version = ServerVersion.Parse(serverVersion, ServerType.MariaDb);

            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new MysqlValidator<TContext>(version)));
        }

        public static DbContextOptionsBuilder<TContext> AddMariaDbExpressionValidation<TContext>(this DbContextOptionsBuilder<TContext> builder, string serverVersion = null) where TContext : DbContext
        {
            ServerVersion version = DefaultMariaDb;
            if (serverVersion != null)
                version = ServerVersion.Parse(serverVersion, ServerType.MariaDb);

            return builder.AddExpressionValidationCore(x => x.AddQueryValidator(new MysqlValidator<TContext>(version)));
        }
    }
}
