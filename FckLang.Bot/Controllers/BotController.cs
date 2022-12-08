using FckLang.Bot.Configuration;
using FckLang.Bot.Models;
using FckLang.Bot.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FckLang.Bot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private IStorage<Mapper> mapperStorage_;
        private BotOptions botOptions_;

        public BotController(IStorage<Mapper> storage, IOptions<BotOptions> options)
            : base()
        {
            mapperStorage_ = storage;
            botOptions_ = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            var bot = new Logic.Bot(botOptions_.Token, mapperStorage_);

            switch (update.Type)
            {
                case UpdateType.Message:
                    await bot.AnswerTextMessage(update.Message);
                    break;
                case UpdateType.InlineQuery:
                    await bot.AnswerInlineQueryAsync(update.InlineQuery);
                    break;
                default:
                    break;
            }

            return Ok();
        }
    }
}
