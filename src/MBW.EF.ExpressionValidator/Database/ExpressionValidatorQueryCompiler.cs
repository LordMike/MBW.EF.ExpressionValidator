using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using MBW.EF.ExpressionValidator.Validatiom;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MBW.EF.ExpressionValidator.Database
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "Adding query translation validation")]
    internal class ExpressionValidatorQueryCompiler : IQueryCompiler
    {
        private readonly IQueryCompiler _inner;
        private readonly ExpressionValidatorExtension _extension;

        public ExpressionValidatorQueryCompiler(IQueryCompiler inner, IDbContextOptions options)
        {
            _inner = inner;
            _extension = options.FindExtension<ExpressionValidatorExtension>();
        }

        private void ValidateExpression<TResult>(Expression query)
        {
            List<(ExpressionValidatorBase, Exception)> exceptions = null;

            IReadOnlyList<ExpressionValidatorBase> validators = _extension.GetValidators();
            foreach (ExpressionValidatorBase validator in validators)
            {
                try
                {
                    validator.CompileQuery<TResult>(query);
                }
                catch (Exception e)
                {
                    exceptions ??= new List<(ExpressionValidatorBase, Exception)>(validators.Count);
                    exceptions.Add((validator, e));
                }
            }

            if (exceptions != null)
            {
                string[] targets = new string[exceptions.Count];
                for (int i = 0; i < exceptions.Count; i++)
                    targets[i] = exceptions[i].Item1.DatabaseKind;

                throw new ExpressionValidationException(targets, "Unable to translate query for " + string.Join(", ", targets), query, exceptions.Select(s => s.Item2));
            }
        }

        public TResult Execute<TResult>(Expression query)
        {
            ValidateExpression<TResult>(query);

            return _inner.Execute<TResult>(query);
        }

        public TResult ExecuteAsync<TResult>(Expression query, CancellationToken cancellationToken)
        {
            ValidateExpression<TResult>(query);

            return _inner.ExecuteAsync<TResult>(query, cancellationToken);
        }

        public Func<QueryContext, TResult> CreateCompiledQuery<TResult>(Expression query)
        {
            ValidateExpression<TResult>(query);

            return _inner.CreateCompiledQuery<TResult>(query);
        }

        public Func<QueryContext, TResult> CreateCompiledAsyncQuery<TResult>(Expression query)
        {
            ValidateExpression<TResult>(query);

            return _inner.CreateCompiledAsyncQuery<TResult>(query);
        }
    }
}