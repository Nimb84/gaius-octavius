using System.Text;
using GO.HostBuilder.Exceptions;
using GO.HostBuilder.Extensions;
using GO.Integration.TelegramBot.Abstractions;
using GO.Integration.TelegramBot.Behaviors.Movie.Enums;
using GO.Integration.TelegramBot.Behaviors.Movie.Helpers;
using GO.Integration.TelegramBot.Extensions;
using GO.Service.Movies.Commands.DeleteWatchItem;
using GO.Service.Movies.Commands.SaveAsWatched;
using GO.Service.Movies.Commands.SaveToWatchLater;
using GO.Service.Movies.Models;
using GO.Service.Movies.Queries.GetMovie;
using GO.Service.Movies.Queries.SearchMovie;
using GO.Service.Users.Models;
using MediatR;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace GO.Integration.TelegramBot.Behaviors.Movie
{
    internal sealed class MovieBotBehavior
        : IMovieBotBehavior
    {
        private readonly IMediator _mediator;
        private readonly ITelegramBotClientService _telegramBotClientService;

        public MovieBotBehavior(
            IMediator mediator,
            ITelegramBotClientService telegramBotClientService)
        {
            _mediator = mediator;
            _telegramBotClientService = telegramBotClientService;
        }

        public async Task HandleMessageAsync(
            UserResponse currentUser,
            Update model,
            CancellationToken cancellationToken = default)
        {
            var query = new SearchMovieQuery(currentUser.Id, model.Message?.Text ?? string.Empty);
            var movieList = await _mediator.Send(query, cancellationToken);

            foreach (var movie in movieList.Take(5))
            {
                var uri = string.IsNullOrWhiteSpace(movie.ImageUrl)
                    ? null
                    : new Uri(movie.ImageUrl);

                await _telegramBotClientService.SendPhotoAsync(
                    model.GetChatId(),
                    GetMessage(movie),
                    uri,
                    GetKeyboard(movie),
                    cancellationToken);
            }
        }

        public Task HandleCommandAsync(
            UserResponse? currentUser,
            Update model,
            CancellationToken cancellationToken = default)
        {
            if (currentUser == null)
                throw new GoForbiddenException();

            var command = model.ToCommandRequest();
            var action = EnumExtensions.Parse<MovieActionType>(command.Action);

            return action switch
            {
                MovieActionType.MarkAsWatched => MarkAsWatchedAsync(
                    currentUser.Id,
                    Guid.Parse(command.Arguments.First()),
                    model,
                    cancellationToken),
                MovieActionType.WatchLater => WatchLaterAsync(
                    currentUser.Id,
                    Guid.Parse(command.Arguments.First()),
                    model,
                    cancellationToken),
                MovieActionType.Delete => DeleteAsync(
                    currentUser.Id,
                    Guid.Parse(command.Arguments.First()),
                    model,
                    cancellationToken),
                _ => throw new ArgumentOutOfRangeException(nameof(MovieActionType), action, null)
            };
        }

        private async Task MarkAsWatchedAsync(
            Guid userId,
            Guid movieId,
            Update model,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new SaveAsWatchedCommand(userId, movieId), cancellationToken);

            var movie = await _mediator.Send(new GetMovieQuery(userId, movieId), cancellationToken);

            await _telegramBotClientService.UpdateCaptionAsync(
                model.GetChatId(),
                model.GetMessageId(),
                GetMessage(movie),
                GetKeyboard(movie),
                cancellationToken);
        }

        private async Task WatchLaterAsync(
            Guid userId,
            Guid movieId,
            Update model,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new SaveToWatchLaterCommand(userId, movieId), cancellationToken);

            var movie = await _mediator.Send(new GetMovieQuery(userId, movieId), cancellationToken);

            await _telegramBotClientService.UpdateCaptionAsync(
                model.GetChatId(),
                model.GetMessageId(),
                GetMessage(movie),
                GetKeyboard(movie),
                cancellationToken);
        }

        private async Task DeleteAsync(
            Guid userId,
            Guid movieId,
            Update model,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteWatchItemCommand(userId, movieId), cancellationToken);

            var movie = await _mediator.Send(new GetMovieQuery(userId, movieId), cancellationToken);

            await _telegramBotClientService.UpdateCaptionAsync(
                model.GetChatId(),
                model.GetMessageId(),
                GetMessage(movie),
                GetKeyboard(movie),
                cancellationToken);
        }

        private static IReplyMarkup GetKeyboard(MovieResponse movie)
        {
            var actions = new List<MovieActionType>
            {
                MovieActionType.MarkAsWatched
            };

            if (!movie.IsWatchLater)
                actions.Add(MovieActionType.WatchLater);

            if (movie.IsWatchLater || movie.LastWatchedAt.HasValue)
                actions.Add(MovieActionType.Delete);

            return MovieInlineKeyboardHelper.GetKeyboard(movie.Id, actions);
        }

        private static string GetMessage(MovieResponse movie)
        {
            var icon = movie.IsWatchLater
                ? "👀"
                : movie.LastWatchedAt != null
                    ? "✔"
                    : "🎬";

            var message = new StringBuilder();

            message.AppendLine($"{icon} {movie.Title} ({movie.Source})");
            message.AppendLine($"{Environment.NewLine}{movie.Type}");
            message.AppendLine($"{Environment.NewLine}{movie.Description}");

            if (string.IsNullOrWhiteSpace(movie.Notes))
                message.AppendLine($"{Environment.NewLine}{movie.Notes}");

            if (movie.LastWatchedAt != null)
                message.AppendLine($"{Environment.NewLine}Last watch: {movie.LastWatchedAt:d}");

            return message.ToString();
        }
    }
}
