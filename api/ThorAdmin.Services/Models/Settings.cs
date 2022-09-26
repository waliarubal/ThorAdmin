namespace ThorAdmin.Services.Models
{
    public sealed class Settings
    {
        public string RootDirectory { get; set; } = "/";

        public string  DbServer { get; set; } = "localhost";

        public string DbUser { get; set; } = "root";

        public string DbPassword { get; set; } = string.Empty;

        public string WordPressArchive { get; set; }
    }
}
