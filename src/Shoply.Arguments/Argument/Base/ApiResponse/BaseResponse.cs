namespace Shoply.Arguments.Argument.Base;

public class BaseResponse<TTypeResult>
{
    public TTypeResult? Result { get; set; }
    public List<DetailedError>? ListError { get; set; }
    public List<DetailedSuccess>? ListSuccess { get; set; }
}