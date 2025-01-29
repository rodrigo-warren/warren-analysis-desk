namespace warren_analysis_desk;

public interface INewsRepository 
{
    Task<News> GetByIdAsync(int id);
    Task<IEnumerable<News>> GetByTitleAsync(string title);
    Task<IEnumerable<News>> GetAllAsync();
    Task<News> AddAsync(News news);
    Task UpdateAsync(News news);
    Task DeleteAsync(int id);
}