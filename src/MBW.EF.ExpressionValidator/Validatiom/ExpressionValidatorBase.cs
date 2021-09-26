using System.Linq.Expressions;

namespace MBW.EF.ExpressionValidator.Validatiom
{
    public abstract class ExpressionValidatorBase
    {
        protected ExpressionValidatorBase(string databaseKind)
        {
            DatabaseKind = databaseKind;
        }

        public string DatabaseKind { get; }

        public abstract void CompileQuery<TResult>(Expression query);
    }
}