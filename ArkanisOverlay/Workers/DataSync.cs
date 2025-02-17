using ArkanisOverlay.Data.UEX.API;
using ArkanisOverlay.Data.UEX.DTO;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Workers;

public class DataSync(ILogger<DataSync> logger, Client client)
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
        }
        catch
        {
            // ignored
        }
    }
}