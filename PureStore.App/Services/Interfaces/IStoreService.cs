using PureStore.App.Models;
using PureStore.Domain.Common;

namespace PureStore.App.Services.Interfaces;

public interface IStoreService
{
    ValueTask<IEnumerable<ItemApp>> GetAsync(string key);

    ValueTask<IEnumerable<ItemApp>> GetAsync();
    ValueTask<IEnumerable<ItemApp>> GetItemApps(int number);
    ValueTask<Response<IEnumerable<Upload>>> GetUploads();
}
