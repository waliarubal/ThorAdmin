using System.IO.Compression;
using ThorAdmin.Services.Models;

namespace ThorAdmin.Services
{
    public class WordPressService : IWordPressService
    {
        readonly IMySqlService _mySqlService;

        public WordPressService(IMySqlService mySqlService) => _mySqlService = mySqlService;

        async Task<(string DbServer, string DbUser, string DbPassword, string DbName)> GetConfig(string wpDirectory)
        {
            string dbName = null,
                dbUser = null,
                dbPassword = null,
                dbServer = null;

            var wpConfig = Path.Combine(wpDirectory, "wp-config.php");
            var defines = (await File.ReadAllLinesAsync(wpConfig)).Where(line => line.StartsWith("define("));
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

        public async Task<bool> CreateInstance(string instanceName, string rootDirectory, string dbServer, string dbUser, string dbPassword, string wpArchive)
        {
            var id = instanceName
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "_");

            var instance = await GetInstance(id, rootDirectory);
            if (instance != null)
                throw new Exception($"Instance with ID '{id}' already exists.");

            var isCreated = await _mySqlService.CreateDatabase(id, dbServer, dbUser, dbPassword);
            if (!isCreated)
                throw new Exception($"Failed to create database '{id}'.");

            if (!File.Exists(wpArchive))
                throw new Exception($"WordPress archive does not exist.");

            var wpDirectory = Path.Combine(rootDirectory, id);
            var wpConfig = Path.Combine(wpDirectory, "wp-config.php");

            ZipFile.ExtractToDirectory(wpArchive, rootDirectory);
            Directory.Move(Path.Combine(rootDirectory, "wordpress"), wpDirectory);
            File.Move(Path.Combine(wpDirectory, "wp-config-sample.php"), wpConfig);

            if (!File.Exists(wpConfig))
                throw new Exception("Failed to create WordPress configuration file.");

            var wpConfigData = (await File.ReadAllTextAsync(wpConfig))
                .Replace("define( 'DB_NAME', 'database_name_here' );", $"define( 'DB_NAME', '{id}' );")
                .Replace("define( 'DB_USER', 'username_here' );", $"define('DB_USER', '{dbUser}' );")
                .Replace("define( 'DB_PASSWORD', 'password_here' );", $"define( 'DB_PASSWORD', '{dbPassword}' );")
                .Replace("define( 'DB_HOST', 'localhost' );", $"define( 'DB_HOST', '{dbServer}' );");
            await File.WriteAllTextAsync(wpConfig, wpConfigData);

            return true;
        }

        public async Task<bool> DeleteInstance(string id, string rootDirectory)
        {
            var instance = await GetInstance(id, rootDirectory);
            if (instance == null)
                return false;

            var (DbServer, DbUser, DbPassword, DbName) = await GetConfig(instance.Directory);

            var isDeleted = await _mySqlService.DeleteDatabase(DbName, DbServer, DbUser, DbPassword);
            if (!isDeleted)
                throw new Exception($"Failed to delete database '{DbName}'.");

            Directory.Delete(instance.Directory, true);

            return true;
        }

        public async Task<WordPressInstance> GetInstance(string id, string rootDirectory)
        {
            var wpDirectory = Path.Combine(rootDirectory, id);
            var wpConfig = new FileInfo(Path.Combine(wpDirectory, "wp-config.php"));
            if (wpConfig.Exists)
            {
                var directoryInfo = new DirectoryInfo(wpDirectory);
                var instance = new WordPressInstance
                {
                    Id = directoryInfo.Name,
                    Directory = directoryInfo.FullName,
                    Created = directoryInfo.CreationTimeUtc,
                    Modified = directoryInfo.LastWriteTimeUtc
                };

                var (DbServer, DbUser, DbPassword, DbName) = await GetConfig(directoryInfo.FullName);

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
