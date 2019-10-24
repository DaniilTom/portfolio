using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SightMap.BLL.Filters
{
    public abstract class BaseFilter<T> : IFilter<T> where T : BaseEntity
    {
        public int Id { get; }
        public int Offset { get; }
        public int Size { get; }

        protected BaseFilter(BaseFilterDTO filterDto)
        {
            Id = filterDto.Id;
            Offset = filterDto.Offset;
            Size = filterDto.Size;
        }

        public virtual Expression<Func<T, bool>> GetExpression()
        {
            Expression<Func<T, bool>> resultExp = s => true;
            return resultExp;
        }

        /*-----------------------------------------------------------------*/
        // Runtime Exception: 
        //private Expression<Func<Sight, bool>> AndExp(Expression<Func<Sight, bool>> left, Expression<Func<Sight, bool>> right)
        //{
        //    return Expression.Lambda<Func<Sight, bool>>(Expression.And(left, right), left.Parameters);
        //}
        /*-----------------------------------------------------------------*/

        /*-----------------------------------------------------------------*/
        // Runtime Exception: Can't Invoke
        //public Expression<Func<Sight, bool>> AndExp(Expression<Func<Sight, bool>> expr1,
        //                                             Expression<Func<Sight, bool>> expr2)
        //{
        //    var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        //    return Expression.Lambda<Func<Sight, bool>>
        //          (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        //}
        /*-----------------------------------------------------------------*/

        /*-----------------------------------------------------------------*/
        // Works fine with GetExpression()
        protected Expression<Func<T, bool>> AndExp(Expression<Func<T, bool>> func1, Expression<Func<T, bool>> func2)
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    func1.Body, new ExpressionParameterReplacer(func2.Parameters, func1.Parameters).Visit(func2.Body)),
                func1.Parameters);
        }

        protected class ExpressionParameterReplacer : ExpressionVisitor
        {
            public ExpressionParameterReplacer(IList<ParameterExpression> fromParameters, IList<ParameterExpression> toParameters)
            {
                ParameterReplacements = new Dictionary<ParameterExpression, ParameterExpression>();
                for (int i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
                    ParameterReplacements.Add(fromParameters[i], toParameters[i]);
            }

            private IDictionary<ParameterExpression, ParameterExpression> ParameterReplacements { get; set; }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression replacement;
                if (ParameterReplacements.TryGetValue(node, out replacement))
                    node = replacement;
                return base.VisitParameter(node);
            }
        }
        /*-----------------------------------------------------------------*/
    }
}
