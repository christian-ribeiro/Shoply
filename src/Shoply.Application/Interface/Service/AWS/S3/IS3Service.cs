using Shoply.Application.Argument.AWS.S3;

namespace Shoply.Application.Interface.Service.AWS.S3;

public interface IS3Service
{
    Task<List<OutputUploadFileS3>> UploadFiles(List<InputUploadFileS3> listInputUploadFileS3);
}