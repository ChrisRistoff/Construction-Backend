using Firebase.Storage;
using construction.Interfaces;

namespace construction.Services;

public class StorageService(IConfiguration configuration) : IStorageService
{
    private readonly string? _bucket = configuration["Firebase:StorageBucket"];
    private readonly string? _apiKey = configuration["Firebase:ApiKey"];

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var task = new FirebaseStorage(_bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(_apiKey),
                ThrowOnCancel = true
            })
            .Child("images")
            .Child(fileName)
            .PutAsync(fileStream);

        var downloadUrl = await task;
        return downloadUrl;
    }



    public async Task DeleteFileAsync(string fileLink)
    {
        var task = new FirebaseStorage(_bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(_apiKey),
                ThrowOnCancel = true
            })
            .Child("images")
            .Child(fileLink)
            .DeleteAsync();

        await task;
    }
}
