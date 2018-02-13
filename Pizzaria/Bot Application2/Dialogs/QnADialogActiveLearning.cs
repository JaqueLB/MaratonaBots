using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace Pizzaria.Dialogs
{
    [Serializable]
    public class QnADialogActiveLearning : QnAMakerDialog
    {
        public QnADialogActiveLearning() : base(new QnAMakerService(new QnAMakerAttribute(ConfigurationManager.AppSettings["QnASubscriptionKey"], ConfigurationManager.AppSettings["QnAKBId"], "Não entendi, por favor pergunte novamente.", 0.5, 3)))
        {

        }
        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            var firstAnswer = result.Answers.First().Answer;

            Activity resposta = ((Activity)context.Activity).CreateReply();

            var dadosResposta = firstAnswer.Split(';');

            if (dadosResposta.Length == 1)
            {
                await context.PostAsync(firstAnswer);
                return;
            }

            var j = 0;
            for (var i = 0; i < 4; i++)
            {
                
                var titulo = dadosResposta[j];
                var imgUrl = dadosResposta[j + 1];
                var url = dadosResposta[j + 2];
                j += 3;

                HeroCard card = new HeroCard
                {
                    Title = titulo,
                };
                card.Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, "Compre Agora", null, url)
                };
                card.Images = new List<CardImage>
                {
                    new CardImage(imgUrl)
                };

                resposta.Attachments.Add(card.ToAttachment());
            }

            await context.PostAsync(resposta);
        }
    }
}