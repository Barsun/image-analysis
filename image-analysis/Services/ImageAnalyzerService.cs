using image_analysis.Config;
using image_analysis.Model;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Options;

namespace image_analysis.Services
{
    public class ImageAnalyzerService : IComputerVisionService
    {
        private static string _subscriptionKey = "";
        private static string _endpoint = "";

        public ImageAnalyzerService(IOptions<ComputerVisionConfig> config)
        {
            _subscriptionKey = config.Value.SubscriptionKey;
            _endpoint = config.Value.Endpoint;
        }

        public ComputerVisionClient Authenticate()
        {
            var client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptionKey))
              { Endpoint = _endpoint };
            return client;
        }

        public async Task<ImageAnalysisViewModel> AnalyzeImage(string imageUrl)
        {
            try
            {
                var client = Authenticate();
                var features = new List<VisualFeatureTypes?>()
                {
                    VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                    VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                    VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                    VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                    VisualFeatureTypes.Objects
                };
                ImageAnalysis results;
                await using (Stream imageStream = File.OpenRead(imageUrl))
                {
                    results = await client.AnalyzeImageInStreamAsync(imageStream, visualFeatures: features);
                    imageStream.Close();
                }

                var imageAnalysis = new ImageAnalysisViewModel
                {
                    ImageAnalysisResult = results
                };
                return imageAnalysis;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
