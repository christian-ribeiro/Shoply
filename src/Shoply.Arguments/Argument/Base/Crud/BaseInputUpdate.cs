using Shoply.Arguments.Utils;

namespace Shoply.Arguments.Argument.Base;

public class BaseInputUpdate<TInputUpdate> : BaseSetProperty<TInputUpdate> where TInputUpdate : BaseInputUpdate<TInputUpdate> { }

public class BaseInputUpdate_0 : BaseInputUpdate<BaseInputUpdate_0> { }