using Microsoft.AspNetCore.Mvc;
using construction.Dtos;
using construction.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace construction.Controllers;



[ApiController]
public class ImageController(ImageRepository imageRepository) : ControllerBase
{


    [HttpPost("construction/api/image")]
    [Authorize]
    public async Task<ActionResult<string>> AddImage(IFormFile file)
    {
        try
        {

            // add image
            var imageLink = await imageRepository.AddImage(file);

            // return image link
            return Ok(new { imageLink });
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpDelete("construction/api/image")]
    [Authorize]
    public async Task<ActionResult> DeleteImage(string imageLink)
    {
        try
        {

            // delete image
            await imageRepository.DeleteImage(imageLink);

            // return no content
            return NoContent();
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
};
