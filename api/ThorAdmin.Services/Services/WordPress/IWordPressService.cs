using ThorAdmin.Services.Models;

namespace ThorAdmin.Services
{
    public interface IWordPressService
    {
        Task<WordPressInstance> GetInstance(string id, Settings settings, bool isDetailRequired = true);

        Task<bool> CreateInstance(string instanceName, Settings settings);

        Task<bool> DeleteInstance(string id, Settings settings);

        Task<IEnumerable<WordPressInstance>> GetInstances(Settings settings);
    }
}
