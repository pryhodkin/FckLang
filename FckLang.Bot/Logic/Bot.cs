using FckLang.Bot.Models;
using FckLang.Bot.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace FckLang.Bot.Logic
{
    public class Bot
    {
        private string token_;
        private IStorage<Mapper> mappers_;
        private Answers answers_;
        public Bot(string token, IStorage<Mapper> mappers, Answers answers = null)
        {
            token_ = token;
            mappers_ = mappers;
            answers_ = Answers.Default;
        }

        public async Task AnswerInlineQueryAsync(InlineQuery query)
        {
            var results = new List<InlineQueryResult>();
            var input = query.Query;

            foreach (var mapper in mappers_)
            {
                var id = $"{query.Id}_{mapper.Id}";
                var title = $"{mapper.From} to {mapper.To}";

                var output = mapper.Map(input);

                var result = new InlineQueryResultArticle(id, title, new InputTextMessageContent(output));
                results.Add(result);
            }

            var client = new TelegramBotClient(token_);

            await client.AnswerInlineQueryAsync(query.Id, results);
        }

        public async Task AnswerTextMessage(Message message)
        {
            var client = new TelegramBotClient(token_);

            await client.SendTextMessageAsync(message.Chat.Id, answers_.Get("PrivateTextMessage"));
        }
    }
}
