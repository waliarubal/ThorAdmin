using ThorAdmin.Services.Models;

namespace ThorAdmin.Services
{
    public class WordPressService : IWordPressService
    {
        readonly IMySqlService _mySqlService;

        public WordPressService(IMySqlService mySqlService) => _mySqlService = mySqlService;

        (string DbServer, string DbUser, string DbPassword, string DbName) GetConfig(string configFile)
        {
            string dbName = null,
                dbUser = null,
                dbPassword = null,
                dbServer = null;

            var defines = File.ReadAllLines(configFile).Where(line => line.StartsWith("define("));
            foreach (var define in defines)
            {
                var config = define.Replace("define(", string.Empty).Replace(");", string.Empty).Split(",");
                if (config.Length != 2)
                    continue;

                var key = config[0].Trim().Replace("'", string.Empty);
                var value = config[1].Trim().Replace("'", string.Empty);
                switch (key)
                {
                    case "DB_HOST":
                        dbServer = value;
                        break;

                    case "DB_NAME":
                        dbName = value;
                        break;

                    case "DB_USER":
                        dbUser = value;
                        break;

                    case "DB_PASSWORD":
                        dbPassword = value;
                        break;
                }
            }

            return (dbServer, dbUser, dbPassword, dbName);
        }

        public bool CreateInstance(string instanceName, string rootDirectory)
        {
            var instance = GetInstance(instanceName, rootDirectory);
            if (instance != null)
                return false;

            return true;
        }

        public bool DeleteInstance(string instanceName, string rootDirectory)
        {
            throw new NotImplementedException();
        }

        

        public async Task<WordPressInstance> GetInstance(string instanceName, string rootDirectory)
        {
            var wpDirectory = Path.Combine(rootDirectory, instanceName);
            var wpConfig = new FileInfo(Path.Combine(wpDirectory, "wp-config.php"));
            if (wpConfig.Exists)
            {
                var directoryInfo = new DirectoryInfo(wpDirectory);
                var instance = new WordPressInstance
                {
                    Directory = directoryInfo.FullName,
                    Created = directoryInfo.CreationTimeUtc,
                    Modified = directoryInfo.LastWriteTimeUtc
                };

                var (DbServer, DbUser, DbPassword, DbName) = GetConfig(wpConfig.FullName);

                instance.Name = await _mySqlService.ExecuteSaclar<string>("SELECT option_value FROM wp_options WHERE option_name = 'blogname'", DbServer, DbUser, DbPassword, DbName) ?? directoryInfo.Name;
                instance.Description = await _mySqlService.ExecuteSaclar<string>("SELECT option_value FROM wp_options WHERE option_name = 'blogdescription'", DbServer, DbUser, DbPassword, DbName);
                instance.Url = await _mySqlService.ExecuteSaclar<string>("SELECT option_value FROM wp_options WHERE option_name = 'siteurl'", DbServer, DbUser, DbPassword, DbName);

                return instance;
            }

            return null;
        }

        public async Task<IEnumerable<WordPressInstance>> GetInstances(string rootDirectory)
        {
            var instances = new List<WordPressInstance>();
            foreach (var directory in Directory.GetDirectories(rootDirectory))
            {
                var instance = await GetInstance(directory, rootDirectory);
                if (instance != null)
                    instances.Add(instance);

            }

            return instances;
        }
    }
}
