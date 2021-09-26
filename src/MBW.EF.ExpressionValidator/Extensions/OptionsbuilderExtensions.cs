using System;
using MBW.EF.ExpressionValidator.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MBW.EF.ExpressionValidator.Extensions
{
    public static class OptionsbuilderExtensions
    {
        public static DbContextOptionsBuilder AddExpressionValidationCore(this DbContextOptionsBuilder builder, Action<ExpressionValidatorExtension> configure)
        {
            ExpressionValidatorExtension extension = builder.Options.FindExtension<ExpressionValidatorExtension>() ?? new ExpressionValidatorExtension();

            ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(extension);
            configure(extension);

            return builder;
        }

        public static DbContextOptionsBuilder<TContext> AddExpressionValidationCore<TContext>(this DbContextOptionsBuilder<TContext> builder, Action<ExpressionValidatorExtension> configure) where TContext : DbContext
        {
            ExpressionValidatorExtension extension = builder.Options.FindExtension<ExpressionValidatorExtension>() ?? new ExpressionValidatorExtension();

            ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(extension);
            configure(extension);

            return builder;
        }
    }
}
