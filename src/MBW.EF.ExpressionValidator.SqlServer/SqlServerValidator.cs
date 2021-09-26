using MBW.EF.ExpressionValidator.Validatiom;
using Microsoft.EntityFrameworkCore;

namespace MBW.EF.ExpressionValidator.SqlServer
{
    internal class SqlServerValidator<TContext> : CommonExpressionValidator<TContext> where TContext : DbContext
    {
        public SqlServerValidator() : base("SqlServer")
        {
        }

        protected override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            const string dummyConnectionString = "Server=localhost";
            optionsBuilder.UseSqlServer(dummyConnectionString);
        }
    }
}