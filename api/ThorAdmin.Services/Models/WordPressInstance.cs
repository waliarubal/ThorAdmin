namespace ThorAdmin.Services.Models
{
    public class WordPressInstance
    {
        public string Id { get; set; }

        public bool IsConfigured { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string Directory { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
