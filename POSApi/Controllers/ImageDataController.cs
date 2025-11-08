using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Domain.Model;

namespace POSApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ImageDataController : ControllerBase
    {
        private readonly IImageDataRepository imageRepository;
        private readonly IGenericRepository<ImageData> genImage;
        public ImageDataController(IImageDataRepository imageRepository, IGenericRepository<ImageData> genImage)
        {
            this.imageRepository = imageRepository;
            this.genImage = genImage;
        }

        [HttpPost("upload")]
        public IActionResult CreateImage(IFormFile data)
        {
            try
            {
                if (data == null)
                {
                    throw new InvalidOperationException("data is null");
                }
                else
                {
                    using var memoryStream = new MemoryStream();
                     data.CopyToAsync(memoryStream);
                    var fileByteArray = memoryStream.ToArray();

                    var prod = new ImageData();
                    prod.FileName = data.FileName;
                    prod.ContentType = data.ContentType;
                    prod.Data = fileByteArray;
                     genImage.AddAsync(prod);
                     genImage.SaveAsync();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
