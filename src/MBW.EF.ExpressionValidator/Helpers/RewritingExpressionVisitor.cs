using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace MBW.EF.ExpressionValidator.Helpers
{
    /// <summary>
    /// Rewrite the expression tree so that it only contains expressions that use entity types etc. of the actual
    /// provider context, instead of the original in-memory provider context.
    ///
    /// Author: Laurents Meyer (https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/1514#issuecomment-925851476)
    /// </summary>
    internal class RewritingExpressionVisitor<TContext> : ExpressionVisitor where TContext : DbContext
    {
        private readonly IModel _model;
        private readonly IAsyncQueryProvider _asyncQueryProvider;

        public RewritingExpressionVisitor(TContext mySqlContext)
        {
            _model = mySqlContext.Model;
            _asyncQueryProvider = mySqlContext.GetInfrastructure().GetService<IAsyncQueryProvider>();
        }

        protected override Expression VisitExtension(Expression node)
            => node switch
            {
                QueryRootExpression { QueryProvider: null } rootExpression =>
                    new QueryRootExpression(_model.FindEntityType(rootExpression.EntityType.Name)),
                QueryRootExpression { QueryProvider: { } } rootExpression =>
                    new QueryRootExpression(_asyncQueryProvider, _model.FindEntityType(rootExpression.EntityType.Name)),
                _ => base.VisitExtension(node),
            };
    }
}