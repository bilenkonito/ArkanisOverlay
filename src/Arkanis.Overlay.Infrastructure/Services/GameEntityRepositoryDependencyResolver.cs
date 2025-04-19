namespace Arkanis.Overlay.Infrastructure.Services;

using Domain.Abstractions;
using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Exceptions;

internal sealed class GameEntityRepositoryDependencyResolver(IServiceProvider serviceProvider)
{
    public Context DependsOn<T>() where T : class, IGameEntity
        => CreateDependencyContextOn<T>();

    public Context DependsOn(Type gameEntityType)
        => CreateDependencyContextOn(gameEntityType);

    private Context CreateDependencyContextOn<T>() where T : class, IGameEntity
        => CreateDependencyContext(CreateDependencyOn<T>());

    private Context CreateDependencyContextOn(Type gameEntityType)
        => CreateDependencyContext(CreateDependencyOn(gameEntityType));

    private IDependable CreateDependencyOn<T>() where T : class, IGameEntity
        => serviceProvider.GetRequiredService<IGameEntityRepository<T>>();

    private IDependable CreateDependencyOn(Type gameEntityType)
    {
        var repositoryType = typeof(IGameEntityRepository<>).MakeGenericType(gameEntityType);
        var service = serviceProvider.GetRequiredService(repositoryType);
        var repository = service as IDependable ?? throw new InvalidOperationException($"failed to resolve {repositoryType} for {gameEntityType}.");
        return repository;
    }

    private Context CreateDependencyContext(IDependable dependable)
        => new(this, dependable);

    public sealed class Context(GameEntityRepositoryDependencyResolver resolver, params IEnumerable<IDependable> dependencies) : IDependable
    {
        private readonly List<IDependable> _dependencies = dependencies.ToList();

        public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        {
            var dependencies = _dependencies
                .Select(dependency => (Instance: dependency, Task: dependency.WaitUntilReadyAsync(cancellationToken)))
                .ToList();

            try
            {
                var tasks = dependencies.Select(x => x.Task).ToList();
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (OperationCanceledException e)
            {
                var failedDependencies = dependencies
                    .Where(dependency => !dependency.Task.IsCompletedSuccessfully)
                    .Select(dependency => dependency.Instance)
                    .ToArray();

                throw DependencyWaitCancelledException.Create(failedDependencies, e);
            }
        }

        public Context AlsoDependencyOn<T>() where T : class, IGameEntity
        {
            _dependencies.Add(resolver.CreateDependencyOn<T>());
            return this;
        }

        public Context AlsoDependencyOn(Type gameEntityType)
        {
            _dependencies.Add(resolver.CreateDependencyOn(gameEntityType));
            return this;
        }
    }
}
