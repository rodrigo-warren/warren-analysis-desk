using Microsoft.EntityFrameworkCore;
using warren_analysis_desk;

public class RobotKeysRepository: IRobotKeysRepository
{
    private readonly DatabaseContext _context;

    public RobotKeysRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<RobotKeys> GetByIdAsync(int Id)
    {
        try 
        {
            return await _context.RobotKeys.FirstOrDefaultAsync(u => u.Id == Id) 
                ?? throw new InvalidOperationException($"RobotKeys not found with ID {Id}");
        }
        catch(InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Error fetching robot keys: {ex.Message}", ex);
        }

    }

    public async Task<IEnumerable<RobotKeys>> GetAllAsync()
    {
        try 
        {
            return await _context.RobotKeys.ToListAsync();
        } catch (Exception ex)
        {
            throw new Exception($"Error fetching robot keys: {ex.Message}", ex);
        }
    }

    public async Task<RobotKeys> AddAsync(RobotKeys robotKeys)
    {
        try
        {
            await _context.RobotKeys.AddAsync(robotKeys);
            await _context.SaveChangesAsync();
            return robotKeys;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding robot keys: {ex.Message}", ex);
        }
    }

    public async Task UpdateAsync(RobotKeys robotKeys)
    {
        try 
        {
            if(!await _context.RobotKeys.AnyAsync(r => r.Id == robotKeys.Id))
            {
                throw new InvalidOperationException($"Robot Key not found with ID {robotKeys.Id}");
            }

            _context.Entry(robotKeys).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new Exception($"Error updating robot keys: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var robotKeys = await _context.RobotKeys.FindAsync(id)
                ?? throw new InvalidOperationException($"Robot Keys not found with ID {id}");;

            _context.RobotKeys.Remove(robotKeys);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting robot keys: {ex.Message}", ex);
        }
    }
}