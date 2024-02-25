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
            },

            new Images
            {
                Image_Id = 3,
                Job_Id = 2,
                Image = "test3"
            },

            new Images
            {
                Image_Id = 4,
                Job_Id = 1,
                Image = "test4"
            },

            new Images
            {
                Image_Id = 5,
                Job_Id = 1,
                Image = "test5"
            },
        };
    }
}
