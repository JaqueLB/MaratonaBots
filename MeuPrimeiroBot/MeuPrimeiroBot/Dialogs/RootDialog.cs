using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MeuPrimeiroBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            await context.PostAsync("**Oi!!**");

            var message = activity.CreateReply();

            if (activity.Text.Equals("herocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateHeroCard();
                message.Attachments.Add(attachment);
            } else if (activity.Text.Equals("videocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateVideoCard();
                message.Attachments.Add(attachment);
            } else if (activity.Text.Equals("audiocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAudioCard();
                message.Attachments.Add(attachment);
            } else if (activity.Text.Equals("animationcard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAnimationCard();
                message.Attachments.Add(attachment);
            } else if (activity.Text.Equals("carrossel", StringComparison.InvariantCultureIgnoreCase))
            {
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                var hero = CreateHeroCard();
                var animation = CreateAnimationCard();
                message.Attachments.Add(hero);
                message.Attachments.Add(animation);
            }
            
            await context.PostAsync(message);

            // calculate something for us to return
            // int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            // await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }

        private Attachment CreateAnimationCard()
        {
            var animationCard = new AnimationCard();
            animationCard.Title = "Um gif";
            animationCard.Subtitle = "gif 1";
            animationCard.Autoloop = false;
            animationCard.Autostart = true;
            animationCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("https://media.giphy.com/media/gZEBpuOkPuydi/giphy.gif")
                };
            return animationCard.ToAttachment();
        }

        private Attachment CreateAudioCard()
        {
            var audioCard = new AudioCard();
            audioCard.Title = "Um áudio";
            audioCard.Subtitle = "audio 1";
            audioCard.Autoloop = false;
            audioCard.Autostart = true;
            audioCard.Image = new ThumbnailUrl("http://tudosobrecachorros.com.br/wp-content/uploads/cachorro-independente.jpg", "thumb");
            audioCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("http://www.wavlist.com/movies/234/sw5-wontfail.wav")
                };
            return audioCard.ToAttachment();
        }

        private Attachment CreateVideoCard()
        {
            var videoCard = new VideoCard();
            videoCard.Title = "Um video";
            videoCard.Subtitle = "video 1";
            videoCard.Autoloop = false;
            videoCard.Autostart = true;
            videoCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("http://techslides.com/demos/sample-videos/small.mp4")
                };
            return videoCard.ToAttachment();
        }

        private Attachment CreateHeroCard()
        {
            var heroCard = new HeroCard();
            heroCard.Title = "Animal";
            heroCard.Subtitle = "Cachorro";
            heroCard.Images = new List<CardImage>
            {
                new CardImage("http://tudosobrecachorros.com.br/wp-content/uploads/cachorro-independente.jpg",
                "Cachorro", new CardAction(ActionTypes.OpenUrl, "title", value:"https://www.google.com"))
            };
            heroCard.Buttons = new List<CardAction>
            {
                new CardAction
                {
                    Text = "Texto",
                    DisplayText = "Display",
                    Title = "Title",
                    Type = ActionTypes.PostBack,
                    Value = "animationcard"
                }
            };
            return heroCard.ToAttachment();
        }
    }
}