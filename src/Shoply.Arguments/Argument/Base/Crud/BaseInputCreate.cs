using Shoply.Arguments.Utils;

namespace Shoply.Arguments.Argument.Base;

public class BaseInputCreate<TInputCreate> : BaseSetProperty<TInputCreate> where TInputCreate : BaseInputCreate<TInputCreate> { }

public class BaseInputCreate_0 : BaseInputCreate<BaseInputCreate_0> { }