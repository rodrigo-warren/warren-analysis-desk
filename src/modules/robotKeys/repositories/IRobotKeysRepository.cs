namespace warren_analysis_desk;

public interface IRobotKeysRepository 
{
    Task<RobotKeys> GetByIdAsync(int id);
    Task<IEnumerable<RobotKeys>> GetAllAsync();
    Task<RobotKeys> AddAsync(RobotKeys robotKeys);
    Task UpdateAsync(RobotKeys robotKeys);
    Task DeleteAsync(int id);
}