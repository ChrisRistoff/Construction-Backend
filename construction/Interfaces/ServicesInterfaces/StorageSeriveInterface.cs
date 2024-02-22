namespace construction.Interfaces;



interface IStorageService
{
    // Upload file to storage
    Task<string> UploadFileAsync(Stream fileStream, string fileName);

    // Delete file from storage
    Task DeleteFileAsync(string fileLink);
}
