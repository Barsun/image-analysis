using image_analysis.Model;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace image_analysis.Services
{
    public class ImageAnalyzerService : IComputerVisionService
    {
        private const string SubscriptionKey = "";
        private const string Endpoint = "";

        public ComputerVisionClient Authenticate()
        {
            var client =
              new ComputerVisionClient(new ApiKeyServiceClientCredentials(SubscriptionKey))
              { Endpoint = Endpoint };
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
