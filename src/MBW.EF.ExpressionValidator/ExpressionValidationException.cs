using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MBW.EF.ExpressionValidator
{
    public class ExpressionValidationException : AggregateException
    {
        public string[] DatabaseTargets { get; }
        public string DatabaseTarget => DatabaseTargets[0];
        public Expression Expression { get; }

        public ExpressionValidationException(string[] databaseTargets, string message, Expression expression, IEnumerable<Exception> innerExceptions) : base(message, innerExceptions)
        {
            Debug.Assert(databaseTargets.Length > 0);

            DatabaseTargets = databaseTargets;
            Expression = expression;
        }
    }
}