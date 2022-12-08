using System.Collections.Generic;

namespace FckLang.Bot.Logic
{
    public class Answers
    {
        Dictionary<string, string> Phrases { get; set; }

        public Answers(Dictionary<string, string> phrases)
        {
            Phrases = phrases;
        }

        public string Get(string key)
        {
            return Phrases.TryGetValue(key, out string answer) ? answer : key;
        }

        public static Answers Default = new Answers(
            new Dictionary<string, string>
            {
                { "PrivateTextMessage", "This bot now supports only inline using," +
                    "please type @{botname} before your bad message to get tips with variants of transliteration." }
            });
    }
}
