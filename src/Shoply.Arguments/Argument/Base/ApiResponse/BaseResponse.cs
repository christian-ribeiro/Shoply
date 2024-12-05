namespace Shoply.Arguments.Argument.Base;

public class BaseResponse<TTypeResult>
{
    public TTypeResult? Result { get; set; }
    public List<DetailedNotification>? ListNotification { get; set; }
}