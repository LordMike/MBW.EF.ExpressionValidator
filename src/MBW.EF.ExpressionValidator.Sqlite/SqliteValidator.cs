using MBW.EF.ExpressionValidator.Validatiom;
using Microsoft.EntityFrameworkCore;

namespace MBW.EF.ExpressionValidator.Sqlite
{
    internal class SqliteValidator<TContext> : CommonExpressionValidator<TContext> where TContext : DbContext
    {
        public SqliteValidator() : base("Sqlite")
        {
        }

        protected override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            const string dummyConnectionString = "Data Source=:memory:;Version=3";
            optionsBuilder.UseSqlite(dummyConnectionString);
        }
    }
}