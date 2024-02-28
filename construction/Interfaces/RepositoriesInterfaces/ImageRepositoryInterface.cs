namespace construction.Interfaces;



public interface IImageRepository
{
    // add image
    Task<string?> AddImage(IFormFile file);

    // delete image
    Task DeleteImage(string imageLink);
}
