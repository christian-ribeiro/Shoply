using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Shoply.Application.Argument.AWS.S3;
using Shoply.Application.Interface.Service.AWS.S3;

namespace Shoply.Application.Service.AWS.S3;

public class S3Service(IOptions<S3Configuration> options) : IS3Service
{
    private readonly AmazonS3Client s3Client = new(options.Value.AccessKeyId, options.Value.SecretAccessKey, RegionEndpoint.GetBySystemName(options.Value.Region));

    public async Task<List<OutputUploadFileS3>> UploadFiles(List<InputUploadFileS3> listInputUploadFileS3)
    {
        var listPutObjectRequest = listInputUploadFileS3.Select(x => new PutObjectRequest
        {
            BucketName = options.Value.BucketName,
            Key = x.FileName,
            InputStream = new MemoryStream(x.File),
            ContentType = x.ContentType
        }).ToList();

        var tasks = listPutObjectRequest.Select(async i =>
        {
            await s3Client.PutObjectAsync(i);
            return new OutputUploadFileS3(listPutObjectRequest.IndexOf(i), i.Key, $"https://{options.Value.BucketName}.s3.amazonaws.com/{i.Key}");
        }).ToList();

        OutputUploadFileS3[] listResponse = await Task.WhenAll(tasks);
        return listResponse.ToList();
    }
}