namespace warren_analysis_desk;
public class NewsService(INewsRepository newsRepository) : INewsService
{
    private readonly INewsRepository _newsRepository = newsRepository;

    public async Task<News> GetByIdAsync(int id)
    {
        return await _newsRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<News>> GetByTitleAsync(string title)
    {
        return await _newsRepository.GetByTitleAsync(title);
    }

    public async Task<IEnumerable<News>> GetAllAsync()
    {
        return await _newsRepository.GetAllAsync();
    }

    public async Task<News> AddAsync(NewsDto newsDto)
    {
        var news = new News(newsDto);
        return await _newsRepository.AddAsync(news);
    }

    public async Task UpdateAsync(int id, NewsDto newsDto)
    {
        newsDto.Id = id;
        var news = new News(newsDto);
        await _newsRepository.UpdateAsync(news);
    }

    public async Task DeleteAsync(int id)
    {
        await _newsRepository.DeleteAsync(id);
    }
}