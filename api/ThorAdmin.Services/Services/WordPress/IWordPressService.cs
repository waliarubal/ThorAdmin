using ThorAdmin.Services.Models;

namespace ThorAdmin.Services
{
    public interface IWordPressService
    {
        Task<WordPressInstance> GetInstance(string id, string rootDirectory, bool isDetailRequired = true);

        Task<bool> CreateInstance(string instanceName, string rootDirectory, string dbServer, string dbUser, string dbPassword, string wpArchive);

        Task<bool> DeleteInstance(string id, string rootDirectory);

        Task<IEnumerable<WordPressInstance>> GetInstances(string rootDirectory);
    }
}
