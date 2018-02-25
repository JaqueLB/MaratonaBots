using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace ManicureBot.Dialogs
{
    [Serializable]
    [LuisModel("LuisModelId", "LuisSubKey")]
    public class CotacaoDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não entendi.");
        }

        [LuisIntent("Cumprimento")]
        public async Task Greetings(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Olá, sou o Manicure Bot, o seu bot para solicitar serviços de manicure e/ou pedicure em casa!!");
            await context.PostAsync("Para começar, digite algo como: Quais os serviços oferecidos?");
            await context.PostAsync("Ou peça o serviço diretamente: Quero uma manicure");
        }

        [LuisIntent("Sobre")]
        public async Task About(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Olá, sou o Manicure Bot, o seu bot para solicitar serviços de manicure e/ou pedicure em casa!!");
            await context.PostAsync("Oferecemos os serviços de: Manicure, Pedicure e Completo");
            await context.PostAsync("O pagamento é realizado no local, diretamente para o profissional");
            await context.PostAsync("Os nossos profissionais aceitam pagamento em dinheiro e cartão de débito");
            await context.PostAsync("Antes de terminar seu pedido, solicitaremos o meio de pagamento para incluir a máquina de cartões ou o troco necessário.");
        }

        [LuisIntent("SolicitarServico")]
        public async Task Solicitacao(IDialogContext context, LuisResult result)
        {
            // todo terminar a parte de solicitação de servicos
            // get all entities that LUIS recognized
            var items = result.Entities?.Select(e => e.Entity);
        }
    }
}