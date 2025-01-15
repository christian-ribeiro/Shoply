namespace Shoply.Application.Argument.AWS.S3;

public class OutputUploadFileS3(int index, string key, string url)
{
    public int Index { get; private set; } = index;
    public string Key { get; private set; } = key;
    public string Url { get; private set; } = url;
}