using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ManicureBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace ManicureBot.Dialogs
{
    [Serializable]
    [LuisModel("LuisModelId", "LuisSubKey")]
    public class RootDialog : LuisDialog<object>
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
            // todo acessar /services
            await context.PostAsync("Oferecemos os serviços de: Manicure, Pedicure e Completo");
            await context.PostAsync("O pagamento é realizado no local, diretamente para o profissional");
            // todo acessar /paymentTypes
            await context.PostAsync("Os nossos profissionais aceitam pagamento em dinheiro e cartão de débito");
            await context.PostAsync("Antes de terminar seu pedido, solicitaremos o meio de pagamento para incluir a máquina de cartões ou o troco necessário.");
        }

        [LuisIntent("SolicitarServico")]
        public async Task Solicitacao(IDialogContext context, LuisResult result)
        {
            // todo terminar a parte de solicitação de servicos
            // get all entities that LUIS recognized
            var entities = result.Entities?.Select(e => e.Entity);

            var endpoint = "https://manicureapi.azurewebsites.net/api/appointments";

            using (var client = new HttpClient())
            {
                //var myObj = new Manicure () {
                //    Name = "Jaqueline",
                //    Phone = "19 999",
                //    Email = "teste",
                //    DayTime = new DateTime(2018, 5, 30, 10, 30, 0),
                //    PaymentType = PaymentTypes.Money,
                //    ServiceType = ServiceTypes.Completo,
                //    Place = "Teste",
                //};
                //var val = new StringContent(myObj.ToString(), System.Text.Encoding.UTF8, "application/json");

                //var botResponse = await client.PostAsync(endpoint, val);

                var botResponse = await client.GetAsync(endpoint);
                foreach (string entity in entities)
                {
                    if (entity.Contains("manicure"))
                    {
                        var json = await botResponse.Content.ReadAsStringAsync();
                        var objJson = JsonConvert.DeserializeObject(json);
                        await context.PostAsync($"{objJson}");
                    }
                }

                if (!botResponse.IsSuccessStatusCode)
                {
                    await context.PostAsync($"Um erro ocorreu. {botResponse.StatusCode}");
                    return;
                }
                else
                {
                    var json = await botResponse.Content.ReadAsStringAsync();
                    await context.PostAsync($"{json}");
                }
            }
        }
    }
}