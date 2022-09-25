namespace ThorAdmin.Services.Models
{
    public class WordPressInstance
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string Directory { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
