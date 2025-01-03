using System.Linq.Expressions;
using System.Reflection;

namespace Shoply.Arguments.Utils;

public class BaseSetProperty<TClass> where TClass : BaseSetProperty<TClass>
{
    public TClass SetProperty<TValue>(Expression<Func<TClass, TValue>> property, TValue? propertyValue)
    {
        if (property.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
            propertyInfo.SetValue(this, propertyValue);

        return (TClass)this;
    }
}