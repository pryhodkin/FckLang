using FckLang.Bot.Configuration;
using FckLang.Bot.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FckLang.Bot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private MongoDbOptions dbOptions_;
        private AdminOptions adminOptions_;

        public AdminController(IOptions<MongoDbOptions> dbOptions, IOptions<AdminOptions> adminOptions)
        {
            dbOptions_ = dbOptions.Value;
            adminOptions_ = adminOptions.Value;
        }

        [HttpPost]
        [Route("resetdb")]
        public async Task<IActionResult> ResetDB([FromBody] string adminToken)
        {
            if (adminToken != adminOptions_.Token)
                return Unauthorized("No or bad admin token provided!");

            var initializer = new DbInitializer();

            await initializer.Initialize(dbOptions_.Connection, dbOptions_.Db, dbOptions_.MapperCollection);

            return Ok();
        }
    }
}
