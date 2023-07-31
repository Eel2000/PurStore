using PureStore.App.Models;
using System.Threading.Tasks;

namespace PureStore.App.Services.Interfaces;

public interface IDownloadService
{
    ValueTask<IEnumerable<ItemApp>> GetDownloadedOrInstalledAsync();
}
