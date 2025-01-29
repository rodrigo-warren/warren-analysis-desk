using Microsoft.EntityFrameworkCore;
using warren_analysis_desk;

public class NewsRepository : INewsRepository
{
    private readonly DatabaseContext _context;

    public NewsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<News> GetByIdAsync(int id)
    {
        try
        {
            return await _context.News.FirstOrDefaultAsync(u => u.Id == id) 
                ?? throw new InvalidOperationException($"News not found with ID {id}");
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Error fetching news: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<News>> GetAllAsync()
    {
        try
        {
            return await _context.News.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching news: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<News>> GetByTitleAsync(string title)
    {
        try
        {
            var newsList = await _context.News
            .Where(u => u.Title == title)
            .ToListAsync() ?? 
                throw new InvalidOperationException($"No news found withtTitle {title}");
        
            return newsList;
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception($"Error fetching news: {ex.Message}", ex);
        }
    }

    public async Task<News> AddAsync(News news)
    {
        try
        {
            if (!await _context.RobotKeys.AnyAsync(r => r.Id == news.RobotKeysId))
            {
                throw new InvalidOperationException($"Robot Key not found with ID {news.RobotKeysId}");
            }

            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
            return news;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding news: {ex.Message}", ex);
        }
    }

    public async Task UpdateAsync(News news)
    {
        try
        {
            if (!await _context.RobotKeys.AnyAsync(r => r.Id == news.RobotKeysId))
            {
                throw new InvalidOperationException($"Robot Key not found with ID {news.RobotKeysId}");
            }

            if (!await _context.News.AnyAsync(u => u.Id == news.Id))
            {
                throw new InvalidOperationException($"News not found with ID {news.Id}");
            }

            _context.Entry(news).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating news: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var news = await _context.News.FindAsync(id) 
                ?? throw new InvalidOperationException($"News not found with ID {id}");

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting news: {ex.Message}", ex);
        }
    }
}
