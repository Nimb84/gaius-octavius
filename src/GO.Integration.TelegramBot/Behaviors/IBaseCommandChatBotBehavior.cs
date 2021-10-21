﻿using GO.Service.Users.Models;
using Telegram.Bot.Types;

namespace GO.Integration.TelegramBot.Behaviors
{
    internal interface IBaseCommandChatBotBehavior
    {
        Task HandleCommandAsync(
            UserResponse? currentUser,
            Update model,
            CancellationToken cancellationToken = default);
    }
}
