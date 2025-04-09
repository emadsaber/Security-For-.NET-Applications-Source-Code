using OpenIddict.Abstractions;
using OpenIdDict_ClientCredentials.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpenIdDict_ClientCredentials.Services
{
    //https://learn.microsoft.com/en-us/dotnet/core/extensions/timer-service
    public class WorkerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public WorkerService(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("service-worker") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "service-worker",
                    ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                    Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials
                }
                });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
