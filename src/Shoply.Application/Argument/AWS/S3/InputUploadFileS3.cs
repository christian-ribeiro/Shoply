namespace Shoply.Application.Argument.AWS.S3;

public class InputUploadFileS3(string fileName, string contentType, byte[] file)
{
    public string FileName { get; private set; } = fileName;
    public string ContentType { get; private set; } = contentType;
    public byte[] File { get; private set; } = file;
}