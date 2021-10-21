using GO.Integration.TelegramBot.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace GO.Core.Api.Controllers
{
    [Route("api/webhooks")]
    [ApiController]
    public sealed class WebhooksController : ControllerBase
    {
        private readonly ITelegramBotListenerService _telegramBotListenerService;

        public WebhooksController(
            ITelegramBotListenerService telegramBotListenerService)
        {
            _telegramBotListenerService = telegramBotListenerService;
        }

        [HttpPost("telegram")]
        public async Task<IActionResult> TelegramHandler(
            [FromBody] Update update,
            CancellationToken cancellationToken)
        {
            await _telegramBotListenerService.EchoUpdateAsync(update, cancellationToken);

            return Ok();
        }
    }
}
