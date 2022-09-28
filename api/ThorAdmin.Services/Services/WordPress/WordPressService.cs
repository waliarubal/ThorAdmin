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

        public async Task<bool> CreateInstance(string instanceName, Settings settings)
        {
            var id = instanceName
                .Trim()
                .ToLowerInvariant()
                .Replace(" ", "_");

            var instance = await GetInstance(id, settings);
            if (instance != null)
                throw new Exception($"Instance with ID '{id}' already exists.");

            var isCreated = await _mySqlService.CreateDatabase(id, settings.DbServer, settings.DbUser, settings.DbPassword);
            if (!isCreated)
                throw new Exception($"Failed to create database '{id}'.");

            if (!File.Exists(settings.WordPressArchive))
                throw new Exception($"WordPress archive does not exist.");

            var wpDirectory = Path.Combine(settings.RootDirectory, id);
            var wpConfig = Path.Combine(wpDirectory, "wp-config.php");

            ZipFile.ExtractToDirectory(settings.WordPressArchive, settings.RootDirectory);
            Directory.Move(Path.Combine(settings.RootDirectory, "wordpress"), wpDirectory);
            File.Move(Path.Combine(wpDirectory, "wp-config-sample.php"), wpConfig);

            if (!File.Exists(wpConfig))
                throw new Exception("Failed to create WordPress configuration file.");

            var wpConfigData = (await File.ReadAllTextAsync(wpConfig))
                .Replace("define( 'DB_NAME', 'database_name_here' );", $"define( 'DB_NAME', '{id}' );")
                .Replace("define( 'DB_USER', 'username_here' );", $"define('DB_USER', '{settings.DbUser}' );")
                .Replace("define( 'DB_PASSWORD', 'password_here' );", $"define( 'DB_PASSWORD', '{settings.DbPassword}' );")
                .Replace("define( 'DB_HOST', 'localhost' );", $"define( 'DB_HOST', '{settings.DbServer}' );");
            await File.WriteAllTextAsync(wpConfig, wpConfigData);

            return true;
        }

        public async Task<bool> DeleteInstance(string id, Settings settings)
        {
            var instance = await GetInstance(id, settings, false);
            if (instance == null)
                return false;

            var (DbServer, DbUser, DbPassword, DbName) = await GetConfig(instance.Directory);

            var isDeleted = await _mySqlService.DeleteDatabase(DbName, DbServer, DbUser, DbPassword);
            if (!isDeleted)
                throw new Exception($"Failed to delete database '{DbName}'.");

            Directory.Delete(instance.Directory, true);

            return true;
        }

        public async Task<WordPressInstance> GetInstance(string id, Settings settings, bool isDetailRequired = true)
        {
            var wpDirectory = Path.Combine(settings.RootDirectory, id);
            var wpConfig = new FileInfo(Path.Combine(wpDirectory, "wp-config.php"));
            if (wpConfig.Exists)
            {
                var directoryInfo = new DirectoryInfo(wpDirectory);
                var instance = new WordPressInstance
                {
                    Id = directoryInfo.Name,
                    Name = directoryInfo.Name,
                    Directory = directoryInfo.FullName,
                    Created = directoryInfo.CreationTimeUtc,
                    Modified = directoryInfo.LastWriteTimeUtc,
                    IsConfigured = false,
                    Url = $"{settings.BaseUrl}/{directoryInfo.Name}",
                };

                if (isDetailRequired)
                {
                    var (DbServer, DbUser, DbPassword, DbName) = await GetConfig(directoryInfo.FullName);

                    instance.IsConfigured =  await _mySqlService.TableExists("wp_options", DbName, DbServer, DbUser, DbPassword);
                    if (instance.IsConfigured)
                    {
                        instance.Name = await _mySqlService.ExecuteSaclar<string>("SELECT option_value FROM wp_options WHERE option_name = 'blogname'", DbServer, DbUser, DbPassword, DbName) ?? directoryInfo.Name;
                        instance.Description = await _mySqlService.ExecuteSaclar<string>("SELECT option_value FROM wp_options WHERE option_name = 'blogdescription'", DbServer, DbUser, DbPassword, DbName);
                        instance.Url = await _mySqlService.ExecuteSaclar<string>("SELECT option_value FROM wp_options WHERE option_name = 'siteurl'", DbServer, DbUser, DbPassword, DbName);
                    }  
                }

                return instance;
            }

            return null;
        }

        public async Task<IEnumerable<WordPressInstance>> GetInstances(Settings settings)
        {
            var instances = new List<WordPressInstance>();
            foreach (var directory in Directory.GetDirectories(settings.RootDirectory))
            {
                var instance = await GetInstance(directory, settings);
                if (instance != null)
                    instances.Add(instance);
            }

            return instances;
        }
    }
}
