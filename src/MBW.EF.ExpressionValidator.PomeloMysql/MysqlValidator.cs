using System;
using MBW.EF.ExpressionValidator.Validatiom;
using Microsoft.EntityFrameworkCore;

namespace MBW.EF.ExpressionValidator.PomeloMysql
{
    internal class MysqlValidator<TContext> : CommonExpressionValidator<TContext> where TContext : DbContext
    {
        public MysqlValidator() : base("MySql")
        {
        }

        protected override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            const string dummyConnectionString = "database=Issue1514";
            optionsBuilder.UseMySql(dummyConnectionString, new MySqlServerVersion(new Version(8, 0, 25)));
        }
    }
}