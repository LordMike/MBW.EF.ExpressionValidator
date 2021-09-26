using System;
using MBW.EF.ExpressionValidator.Database;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace MBW.EF.ExpressionValidator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "Adding query translation validation")]
        public static IServiceCollection AddExpressionValidator(this IServiceCollection services)
        {
            return services.Decorate((IQueryCompiler existing, IServiceProvider serviceProvider) =>
            {
                return ActivatorUtilities.CreateInstance<ExpressionValidatorQueryCompiler>(serviceProvider, existing);
            });
        }
    }
}