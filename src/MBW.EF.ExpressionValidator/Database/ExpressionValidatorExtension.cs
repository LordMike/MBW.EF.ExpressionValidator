using System.Collections.Generic;
using MBW.EF.ExpressionValidator.Extensions;
using MBW.EF.ExpressionValidator.Validatiom;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MBW.EF.ExpressionValidator.Database
{
    public class ExpressionValidatorExtension : IDbContextOptionsExtension
    {
        private readonly List<ExpressionValidatorBase> _validators = new List<ExpressionValidatorBase>();
        private DbContextOptionsExtensionInfo _info;

        public DbContextOptionsExtensionInfo Info => _info ??= new ExpressionValidatorExtensionInfo(this);

        public void ApplyServices(IServiceCollection services)
        {
            services.AddExpressionValidator();
        }

        public void AddQueryValidator<TValidator>(TValidator validator) where TValidator : ExpressionValidatorBase
        {
            _validators.Add(validator);
        }

        internal IReadOnlyList<ExpressionValidatorBase> GetValidators()
        {
            return _validators;
        }

        public void Validate(IDbContextOptions options)
        {
        }

        internal class ExpressionValidatorExtensionInfo : DbContextOptionsExtensionInfo
        {
            public ExpressionValidatorExtensionInfo(ExpressionValidatorExtension extension) : base(extension)
            {
            }

            public override int GetServiceProviderHashCode()
            {
                return 0;
            }

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            {
                return false;
            }

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
            }

            public override bool IsDatabaseProvider => false;
            public override string LogFragment => string.Empty;
        }
    }
}