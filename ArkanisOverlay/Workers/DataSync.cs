using ArkanisOverlay.Data.Storage;
using ArkanisOverlay.Data.UEX.API;
using ArkanisOverlay.Data.UEX.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Workers;

public class DataSync(ILogger<DataSync> logger, Client client, UEXContext dbContext)
{
    private Thread? _thread;

    public void Start()
    {
        _thread = new Thread(Run);
        _thread.Start();
    }

    private async void Run()
    {
        await Update<CommodityDto>("/commodities").ConfigureAwait(false);
        await Update<VehicleDto>("/vehicles").ConfigureAwait(false);
    }

    private async Task Update<T>(string endpoint) where T : BaseDto, new()
    {
        try
        {
            var result = await client.Get<List<T>>(endpoint).ConfigureAwait(false);

            if (result == null) return;

            logger.LogDebug(result.ToString());
            
            var debug = result.GroupBy(x => x.Id).Where(x => x.Count() > 1).ToList();

            // since we are doing a synchronization update, the remote state defines our local state
            // so we can delete all existing entries and add new ones
            var dbSet = dbContext.Set<T>();
            // delete all existing entries
            foreach (var id in dbSet.Select(e => e.Id))
            {
                var entity = new T { Id = id };
                dbSet.Entry(entity).State = EntityState.Deleted;
            }
            // add new entries
            await dbSet.AddRangeAsync(result).ConfigureAwait(false);

            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to update {endpoint}: {message}", endpoint, ex.Message);
        }
        
        logger.LogInformation("Finished updating {endpoint}", endpoint);
    }
}