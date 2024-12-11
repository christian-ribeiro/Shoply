namespace Shoply.Arguments.DataAnnotation;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class HeaderIgnoreAttribute : Attribute { }