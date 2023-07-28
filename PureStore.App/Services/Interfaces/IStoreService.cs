using PureStore.App.Models;

namespace PureStore.App.Services.Interfaces;

public interface IStoreService
{
    ValueTask<IEnumerable<ItemApp>> GetAsync(string key);

    ValueTask<IEnumerable<ItemApp>> GetAsync();
    ValueTask<IEnumerable<ItemApp>> GetItemApps(int number);
}
