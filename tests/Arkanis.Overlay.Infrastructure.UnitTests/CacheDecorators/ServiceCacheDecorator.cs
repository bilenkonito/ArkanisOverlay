namespace Arkanis.Overlay.Infrastructure.UnitTests.CacheDecorators;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

public abstract class ServiceCacheDecorator(ILogger logger)
{
    [SuppressMessage("Security", "CA5351:Do Not Use Broken Cryptographic Algorithms")]
    protected async Task<TResult> CacheAsync<TParams, TResult>(
        TParams methodParams,
        string methodName,
        Func<TParams, Task<TResult>> runAsync,
        [CallerFilePath] string callerFilePath = ""
    )
    {
        var serializedParams = JsonSerializer.Serialize(methodParams);
        logger.LogDebug("Proxying call to {MethodName} with params: {MethodParams}", methodName, serializedParams);

        var paramsId = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(serializedParams)));
        TResult? result = default;

        var callerDirPath = Path.GetDirectoryName(callerFilePath);
        var cacheFileName = $"{methodName}-{paramsId}.json";
        var cacheFileDir = Path.Join(callerDirPath, "data");
        var cacheFilePath = Path.Join(cacheFileDir, cacheFileName);
        if (File.Exists(cacheFilePath))
        {
            logger.LogDebug("Loading cached result from {FilePath} for a call to {MethodName}", cacheFilePath, methodName);
            result = await JsonSerializer.DeserializeAsync<TResult>(File.OpenRead(cacheFilePath));
        }

        if (result is null)
        {
            result = await runAsync(methodParams);
            logger.LogDebug("Writing cached result to {FilePath} from a call to {MethodName}", cacheFilePath, methodName);

            Directory.CreateDirectory(cacheFileDir);
            await using var file = File.OpenWrite(cacheFilePath);
            await JsonSerializer.SerializeAsync(file, result);
        }

        return result;
    }
}
