namespace warren_analysis_desk;

public interface IRobotKeysService
{
    Task<RobotKeys> GetByIdAsync(int id);
    Task<IEnumerable<RobotKeys>> GetAllAsync();
    Task<RobotKeys> AddAsync(RobotKeysDto robotKeysDto);
    Task UpdateAsync(int id, RobotKeysDto robotKeysDto);
    Task DeleteAsync(int id);
}