
namespace Lab.MVC.AppSemTemplate.Services
{
    public class HostedExampleService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Loop infinito que só para quando um token
            // é enviado para parar a execução
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
