using System.Net;
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
        // extract the path from the URL
        Uri fileUri = new Uri(fileLink);
        string filePath = WebUtility.UrlDecode(fileUri.AbsolutePath);

        // extract the file path after '/images/'
        const string prefixToRemove = "/images/";
        int prefixPosition = filePath.IndexOf(prefixToRemove);
        if (prefixPosition >= 0)
        {
            // +1 to include the trailing slash in removal
            filePath = filePath.Substring(prefixPosition + prefixToRemove.Length);
        }

        // remove any leading or trailing slashes
        filePath = filePath.Trim('/');

        // initialize fb storage with firebase bucket name and options
        var storage = new FirebaseStorage(_bucket, new FirebaseStorageOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(_apiKey),
            ThrowOnCancel = true
        });

        // delete the file
        await storage.Child("images").Child(filePath).DeleteAsync();
    }

}
