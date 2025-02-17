using ArkanisOverlay.Data.Storage;
using ArkanisOverlay.Data.UEX.API;
using ArkanisOverlay.Data.UEX.DTO;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Workers;

public class DataSync(ILogger<DataSync> logger, Client client, UEXContext dbContext)
{
    private Thread? _thread;
    
    public void Start() {
        _thread = new Thread(Run);
        _thread.Start();
    }

    private async void Run()
    {
        try
        {
            var result = await client.Get<List<CommodityDto>>("/commodities").ConfigureAwait(false);

            if (result == null) return;
            
            logger.LogDebug(result.ToString());

            await dbContext.Commodities.AddRangeAsync(result).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
        catch
        {
            // ignored
        }
    }
}