using image_analysis.Model;
using image_analysis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace image_analysis.Controllers
{
    [ApiController]
    [Route("image")]
    public class ImageAnalyzerController : ControllerBase
    {

        private readonly ILogger<ImageAnalyzerController> _logger;
        private readonly IComputerVisionService _computerVisionService;

        public ImageAnalyzerController(ILogger<ImageAnalyzerController> logger)
        {
            _logger = logger;
            _computerVisionService = new ImageAnalyzerService();
        }

        [HttpGet("analyzer")]
        public async Task<ImageAnalysisViewModel> GetImageAnalyzerResultAsync([FromBody]string imageStream)
        {
            return await _computerVisionService.AnalyzeImage(imageStream);
        }
    }
}