namespace image_analysis.Model
{
    public interface IComputerVisionService
    {
        Task<ImageAnalysisViewModel> AnalyzeImage(string imageUrl);
    }
}
