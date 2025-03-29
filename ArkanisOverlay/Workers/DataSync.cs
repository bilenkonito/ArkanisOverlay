using ArkanisOverlay.Data.API;
using ArkanisOverlay.Data.Entities.UEX;
using ArkanisOverlay.Data.Storage;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Workers;

public class DataSync(ILogger<DataSync> logger, DataClient dataClient, UEXContext dbContext)
{
    private Thread? _thread;

    public void Start()
    {
        // _thread = new Thread(Run);
        // _thread.Start();
    }

    private async void Run()
    {
        await Update<CommodityEntity>("/commodities").ConfigureAwait(false);
        await Update<VehicleEntity>("/vehicles").ConfigureAwait(false);
    }

    public async Task Update<T>(string endpoint) where T : BaseEntity, new()
    {
        var typeName = typeof(T).Name;

        logger.LogInformation("Updating {type} from {endpoint}", typeName, endpoint);
        try
        {
            var result = await dataClient.Get<List<T>>(endpoint).ConfigureAwait(false);

            if (result == null) return;

            logger.LogDebug(result.ToString());

            var debug = result.GroupBy(x => x.Id).Where(x => x.Count() > 1).ToList();

            
            dbContext.UpdateRange(result);
            // since we are doing a synchronization update, the remote state defines our local state
            // so we can delete all existing entries and add new ones
            // var dbSet = dbContext.Set<T>();
            // delete all existing entries
            // foreach (var id in dbSet.Select(e => e.Id))
            // {
            //     var entity = new T { Id = id };
            //     dbSet.Entry(entity).State = EntityState.Deleted;
            // }

            // add new entries
            // dbSet.UpdateRange(result);
            // await dbSet.AddRangeAsync(result).ConfigureAwait(false);

            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            // fix EF Core tracking issues
            dbContext.ChangeTracker.Clear();
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to update {type} from {endpoint}: {message}", typeName, endpoint, ex.Message);
        }

        logger.LogInformation("Finished updating {type} from {endpoint}", typeName, endpoint);
    }
}