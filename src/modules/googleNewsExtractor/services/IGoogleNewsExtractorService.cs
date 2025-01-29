namespace warren_analysis_desk;

public interface IGoogleNewsExtractorService
{
    Task<List<News>> GetGoogleNews();
}