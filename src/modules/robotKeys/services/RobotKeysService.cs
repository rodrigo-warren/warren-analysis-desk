namespace warren_analysis_desk;
public class RobotKeysService(IRobotKeysRepository robotKeysRepository) : IRobotKeysService
{
    private readonly IRobotKeysRepository _robotKeysRepository = robotKeysRepository;

    public async Task<RobotKeys> GetByIdAsync(int id)
    {
        return await _robotKeysRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<RobotKeys>> GetAllAsync()
    {
        return await _robotKeysRepository.GetAllAsync();
    }

    public async Task<RobotKeys> AddAsync(RobotKeysDto robotKeysDto)
    {
        var robotKeys = new RobotKeys(robotKeysDto);
        return await _robotKeysRepository.AddAsync(robotKeys);
    }

    public async Task UpdateAsync(int id, RobotKeysDto robotKeysDto)
    {
        robotKeysDto.Id = id;
        var robotKeys = new RobotKeys(robotKeysDto);
        await _robotKeysRepository.UpdateAsync(robotKeys);
    }

    public async Task DeleteAsync(int id)
    {
        await _robotKeysRepository.DeleteAsync(id);
    }
}