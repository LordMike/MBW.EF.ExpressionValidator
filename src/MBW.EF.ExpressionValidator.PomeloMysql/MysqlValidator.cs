using MBW.EF.ExpressionValidator.Validatiom;
using Microsoft.EntityFrameworkCore;

namespace MBW.EF.ExpressionValidator.PomeloMysql
{
    internal class MysqlValidator<TContext> : CommonExpressionValidator<TContext> where TContext : DbContext
    {
        private readonly ServerVersion _serverVersion;

        public MysqlValidator(ServerVersion serverVersion) : base("MySql")
        {
            _serverVersion = serverVersion;
        }

        protected override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            const string dummyConnectionString = "server=irrelevant";
            optionsBuilder.UseMySql(dummyConnectionString, _serverVersion);
        }
    }
}