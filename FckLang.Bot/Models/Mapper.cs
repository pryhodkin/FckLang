using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;
using System.Text;

namespace FckLang.Bot.Models
{
    public class Mapper
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<char, char> Dictionary { get; set; }


        public Mapper(Dictionary<char, char> dict, string from, string to)
        {
            Dictionary = dict;
            From = from;
            To = to;
        }

        public string Map(string text)
        {
            var sb = new StringBuilder();

            foreach (char c in text)
            {
                if (Dictionary.TryGetValue(c, out char result))
                    sb.Append(result);
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
