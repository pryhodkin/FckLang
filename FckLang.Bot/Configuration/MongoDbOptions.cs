namespace FckLang.Bot.Configuration
{
    public class MongoDbOptions
    {
        public const string Section = "MongoDb";

        public string Connection { get; set; }
        public string Db { get; set; }
        public string MapperCollection { get; set; }
    }
}
