using System;
using System.Threading;
using System.Threading.Tasks;
using BackEndHagan.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class BidUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public BidUpdateService ( IServiceProvider serviceProvider )
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync ( CancellationToken stoppingToken )
    {
        while ( !stoppingToken.IsCancellationRequested )
        {
            using ( var scope = _serviceProvider.CreateScope () )
            {
                var haganContext = scope.ServiceProvider.GetRequiredService<HaganContext>();
            var currentTime = DateTime.Now;

                // Check if it's the specific time you want to update the property
                if ( currentTime.Hour == 23 && currentTime.Minute == 59 && currentTime.Second == 59 )
            {
                    await haganContext.Database.ExecuteSqlRawAsync ("EXEC UpdateBidState");
            }
            }

            // Delay for 1 second before checking again
            await Task.Delay (1000, stoppingToken);
        }
    }

    private async Task UpdateProperty ( HaganContext haganContext )
    {
         await haganContext.Database.ExecuteSqlRawAsync ("EXEC UpdateBidState");
    }
}