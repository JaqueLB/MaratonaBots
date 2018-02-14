using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace MeuPrimeiroBot.Dialogs
{
    [Serializable]
    [LuisModel("LuisModelId", "LuisSubKey")]
    public class CotacaoDialog : LuisDialog<object>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Desculpe, não consegui entender {result.Query}");
        }

        [LuisIntent("Cumprimento")]
        public async Task Greedings(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Olá, tudo ótimo! Sou um bot que informa cotação de moedas.");
        }

        [LuisIntent("Cotacao")]
        public async Task Quote(IDialogContext context, LuisResult result)
        {
            // get all entities that LUIS recognized
            var currencies = result.Entities?.Select(e => e.Entity);

            foreach (string currency in currencies)
            {
                var code = "";
                if (currency.Contains("dólar") || currency.Contains("dolar"))
                {
                    code = "USD";
                } else if (currency.Contains("euro")) {
                    code = "EUR";
                }
                var endpoint = $"{code}";

                using (var client = new HttpClient())
                {
                    var botResponse = await client.GetAsync(endpoint);
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
}