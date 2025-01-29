namespace warren_analysis_desk;

public interface INewsService
{
    Task<News> GetByIdAsync(int id);
    Task<IEnumerable<News>> GetByTitleAsync(string title);
    Task<IEnumerable<News>> GetAllAsync();
    Task<News> AddAsync(NewsDto newsDto);
    Task UpdateAsync(int id, NewsDto newsDto);
    Task DeleteAsync(int id);
}