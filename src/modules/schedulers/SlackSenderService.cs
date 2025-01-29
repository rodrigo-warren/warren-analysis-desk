using Microsoft.AspNetCore.Mvc;
using warren_analysis_desk;


public class SlackSchedulerService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public SlackSchedulerService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var robotKeysService = scope.ServiceProvider.GetRequiredService<IRobotKeysService>();

                await Task.Delay(60000, stoppingToken);

                // var robotKeyController = (RobotKeysController)scope
                //     .ServiceProvider.GetRequiredService(typeof(RobotKeysController));

                Console.WriteLine("Disparando de 60...60 segundos para o Slack.");
            }
        }
    }
}
