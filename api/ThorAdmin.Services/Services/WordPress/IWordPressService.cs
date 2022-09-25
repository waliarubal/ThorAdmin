using ThorAdmin.Services.Models;

namespace ThorAdmin.Services
{
    public interface IWordPressService
    {
        Task<WordPressInstance> GetInstance(string instanceName, string rootDirectory);

        bool CreateInstance(string instanceName, string rootDirectory);

        bool DeleteInstance(string instanceName, string rootDirectory);

        Task<IEnumerable<WordPressInstance>> GetInstances(string rootDirectory);
    }
}
