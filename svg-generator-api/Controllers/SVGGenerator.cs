using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace svg_generator_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SVGGenerator : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        public SVGGenerator(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name ="GetFigureDimensions")]
        public async Task<IActionResult> GetFigureDimension()
        {
            try
            {
                var dimensions = await GetDimensions();
                return Ok(dimensions);

            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
        }
        private async Task<SVGFigureDimensions> GetDimensions()
        {
            using (StreamReader reader = new StreamReader(SVGFigureDimensions.GetFilePath))
            {
                var dimensions = JsonConvert.DeserializeObject<SVGFigureDimensions>(await reader.ReadToEndAsync());
                return dimensions;
            }
        }
        [HttpPost(Name = "SaveFigureDimensions")]
        public async void GetFigureDimension([FromBody] SVGFigureDimensions body)
        {
            try
            {
                string json = JsonConvert.SerializeObject(body);
                byte[] data = Encoding.UTF8.GetBytes(json);

                using (FileStream fileStream = System.IO.File.Open(SVGFigureDimensions.GetFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fileStream.Write(data, 0, data.Length);
                }


            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
