
namespace System.Linq.Expressions;

public static class ExpressionExtensions
{
    public static Expression<TDelegate> Substitute<TDelegate, TValue>(this LambdaExpression lambdaSource, int paramIndex, Expression<Func<TValue>> substituteValue)
        => lambdaSource.Substitute<TDelegate>(paramIndex, substituteValue.Body);

    public static Expression<TDelegate> Substitute<TDelegate>(this LambdaExpression lambdaSource, int paramIndex, Expression substituteValue)
    {
        var visitor = new LambdaParameterSubtitution(lambdaSource.Parameters[paramIndex], substituteValue);
        var newBody = visitor.Visit(lambdaSource.Body);
        var newParameters = lambdaSource.Parameters.Where((_, i) => i != paramIndex);
        var result = Expression.Lambda<TDelegate>(newBody, newParameters);

        return result;
    }

    private class LambdaParameterSubtitution : ExpressionVisitor
    {
        private readonly ParameterExpression _substitutedParameter;
        private readonly Expression _substituteValue;

        public LambdaParameterSubtitution(ParameterExpression substitutedParameter, Expression substituteValue)
        {
            this._substitutedParameter = substitutedParameter;
            this._substituteValue = substituteValue;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _substitutedParameter ? _substituteValue : node;
    }
}
