using construction.Dtos;

namespace construction.Seeds;

public class ImagesData
{
    public Images[] GetImagesData()
    {
        return new Images[]
        {
            new Images
            {
                Image_Id = 1,
                Job_Id = 1,
                Image = "test"
            },

            new Images
            {
                Image_Id = 2,
                Job_Id = 1,
                Image = "test2"
            }
        };
    }
}
