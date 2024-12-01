using System.Reflection;

namespace Shoply.Arguments.Argument.General.Session;

public class SessionHelper
{
    public static void SetGuidSessionDataRequest<TClass>(TClass @class, Guid guidSessionDataRequest)
    {
        List<FieldInfo> listRuntimeFields = (from i in @class?.GetType().GetRuntimeFields() where i.FieldType.IsInterface select i).ToList();

        _ = (from i in listRuntimeFields
             let setGuidMethod = i.FieldType.GetMethod("SetGuid")
             let listInterface = setGuidMethod == null ? [.. i.FieldType.GetInterfaces()] : new List<Type>()
             where (from j in listInterface where j != null select j).Any() | setGuidMethod != null
             let interfaceToInvoke = setGuidMethod == null ? (from j in listInterface
                                                              where j != null
                                                              let setGuidMethod = j.GetMethod("SetGuid")
                                                              where setGuidMethod != null
                                                              select new { Interface = i, SetGuidMethod = setGuidMethod }).FirstOrDefault() : new { Interface = i, SetGuidMethod = setGuidMethod }
             where interfaceToInvoke != null
             select InvokeInterfaceSetGuid(@class, guidSessionDataRequest, (FieldInfo: interfaceToInvoke.Interface, MethodInfo: interfaceToInvoke.SetGuidMethod))).ToList();
    }

    public static bool InvokeInterfaceSetGuid<TClass>(TClass @class, Guid guidSessionDataRequest, (FieldInfo FieldInfo, MethodInfo MethodInfo) interfaceToInvoke)
    {
        var value = interfaceToInvoke.FieldInfo.GetValue(@class);
        if (value != null)
            interfaceToInvoke.MethodInfo.Invoke(value, new object[] { guidSessionDataRequest });
        return true;
    }
}