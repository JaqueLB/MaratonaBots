using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ManicureBot.Forms;
using ManicureBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace ManicureBot.Dialogs
{
    [Serializable]
    [LuisModel("LuisModelId", "LuisSubKey")]
    public class RootDialog : LuisDialog<ManicureOrder>
    {
        // code to change the dialog to FormFlow
        private BuildFormDelegate<ManicureOrder> MakeManicureForm;

        internal BuildFormDelegate<ManicureOrder> ManicureOrderDialog(BuildFormDelegate<ManicureOrder> makeManicureForm)
        {
            return this.MakeManicureForm = makeManicureForm;
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Desculpe, não entendi.");
        }

        [LuisIntent("Cumprimento")]
        public async Task Greetings(IDialogContext context, IAwaitable<IMessageActivity> argument, LuisResult result)
        {
            var activity = await argument as Activity;
            var userName = activity.From.Name;

            await context.PostAsync("Olá! Sou o ManicureBot, seu serviço de solicitação de manicures em casa.");
            await context.PostAsync($"Bem-vindo(a) {userName} .\r\n" +
                $"Para começar, digite algo como: Quais os serviços oferecidos? Ou peça o serviço diretamente: Quero uma manicure.");
        }

        [LuisIntent("Sobre")]
        public async Task About(IDialogContext context, IAwaitable<IMessageActivity> argument, LuisResult result)
        {
            var activity = await argument as Activity;
            var message = activity.CreateReply();

            var heroCard = new HeroCard();
            heroCard.Title = "Manicure Bot";
            heroCard.Subtitle = "Sobre";
            heroCard.Images = new List<CardImage>
            {
                new CardImage("https://cdn.pixabay.com/photo/2017/03/02/20/54/nail-varnish-2112358_960_720.jpg", "Esmaltes")
            };
            var att = heroCard.ToAttachment();
            message.Attachments.Add(att);
            await context.PostAsync(message);

            await context.PostAsync("Sou um bot para solicitar serviços de manicure e/ou pedicure em casa.\r\n" +
                "Oferecemos serviços de Manicure, Pedicure ou Completo.\r\n");
            await context.PostAsync("Você escolhe o serviço, o dia da semana e o horário.\r\n" +
               "Se você solicitar para terça-feira, e hoje é uma terça, tenha em mente que o serviço será agendado para a próxima terça-feira.\r\n");
            await context.PostAsync("O pagamento é realizado no local, diretamente para a Dorinha, nossa profissional.\r\n" +
                "Aceitamos pagamento em dinheiro e cartões de débito.\r\n");
            await context.PostAsync("Atendemos de Segunda à Sábado, das 8h às 18h.");
        }

        [LuisIntent("SolicitarServico")]
        public async Task Solicitacao(IDialogContext context, LuisResult result)
        {
            // get all entities that LUIS recognized
            var entities = new List<EntityRecommendation>(result.Entities);
            if (!entities.Any((entity) => entity.Type == "ServiceType"))
            {
                // Infer serviceType and skip this step in FormFlow
                foreach (var entity in result.Entities)
                {
                    string kind = null;
                    switch (entity.Type)
                    {
                        case "manicure": kind = "Manicure"; break;
                        case "pedicure": kind = "Pedicure"; break;
                        case "completo": kind = "Completo"; break;
                        default: kind = "Manicure"; break;
                    }
                    if (kind != null)
                    {
                        entities.Add(new EntityRecommendation(type: "ServiceType") { Entity = kind });
                        break;
                    }
                }
            }
            // call FormFlow to place an order
            var manicureForm = new FormDialog<ManicureOrder>(new ManicureOrder(), this.MakeManicureForm, FormOptions.PromptInStart, entities);
            context.Call(manicureForm, ManicureFormComplete);
        }

        // on complete form
        private async Task ManicureFormComplete(IDialogContext context, IAwaitable<ManicureOrder> result)
        {
            ManicureOrder order = null;
            try
            {
                order = await result;
                var userName = order.Name;
            }
            catch (OperationCanceledException)
            {
                await context.PostAsync("Você cancelou o formulário!");
                return;
            }

            if (order != null)
            {
                var endpointServices = "https://manicureapi.azurewebsites.net/api/services";
                double orderPrice = 0.00;
                DateTime today = DateTime.Today;
                // get date and time
                int current = (int)today.DayOfWeek;
                int desired = (int)order.Day;
                int n = (7 - current + desired);

                int daysUntilWeekDay = (n > 7) ? n % 7 : n;
                DateTime nextWeekDay = today.AddDays(daysUntilWeekDay);
                DateTime agendado = new DateTime(nextWeekDay.Year, nextWeekDay.Month, nextWeekDay.Day, (int)order.Hour, 0, 0);
                string scheduled = agendado.ToString();

                using (var client = new HttpClient())
                {
                    var botResponse = await client.GetAsync(endpointServices);
                    if (!botResponse.IsSuccessStatusCode)
                    {
                        await context.PostAsync("Por favor, confirme o preço com nossa profissional por WhatsApp/SMS/Ligação: 019 9999-9999");
                    }
                    else
                    {
                        var json = await botResponse.Content.ReadAsStringAsync();
                        var services = JsonConvert.DeserializeObject<List<Service>>(json);
                        // get price
                        foreach (Service opt in services)
                        {
                            if ((opt.Name == order.ServiceType.ToString()) || (opt.Name.Contains("Completo") && order.ServiceType.ToString().Contains("Completo")))
                            {
                                orderPrice = Double.Parse(opt.Price);
                                break;
                            }
                        }
                        //await context.PostAsync($"Retorno api: {json}");
                    }
                }

                // send order data to api
                var endpointAppointments = "https://manicureapi.azurewebsites.net/api/appointments";
                var manicureOrderJson = JsonConvert.SerializeObject(new Manicure {
                    Name = order.Name,
                    Place = order.Address,
                    Phone = order.Phone,
                    DayTime = agendado,
                    PaymentType = (int)order.PaymentType,
                    ServiceType = (int)order.ServiceType,
                    Email = order.Email
                });

                var manicureOrderData = new StringContent(manicureOrderJson.ToString(), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var botResponse = await client.PostAsync(endpointAppointments, manicureOrderData);
                    if (!botResponse.IsSuccessStatusCode)
                    {
                        await context.PostAsync("Um erro ocorreu.");
                        return;
                    }
                    else
                    {
                        var json = await botResponse.Content.ReadAsStringAsync();
                        await context.PostAsync("Veja os dados do seu pedido");
                        await context.PostAsync("Seu Nome: " + order.Name);
                        await context.PostAsync("Serviço: " + order.ServiceType);
                        await context.PostAsync("Valor: " + orderPrice);
                        await context.PostAsync("Agendado para: " + scheduled);
                        await context.PostAsync("Endereço: " + order.Address);
                        await context.PostAsync("Telefone: " + order.Phone);
                        await context.PostAsync("Email: " + order.Email);
                        //await context.PostAsync($"Retorno api: {json}");
                    }
                }
            }
            else
            {
                await context.PostAsync("Formulário retornou resposta vazia!");
            }

            context.Wait(MessageReceived);
        }

        //[LuisIntent("ChecarSolicitacoes")]
        //public async Task MinhasSolicitacoes(IDialogContext context, LuisResult result)
        //{
        //    // get all entities that LUIS recognized
        //    var entities = new List<EntityRecommendation>(result.Entities);

        //    // get user data

        //    var endpoint = "";

        //    using (var client = new HttpClient())
        //    {
        //        var botResponse = await client.GetAsync(endpoint);
        //        if (!botResponse.IsSuccessStatusCode)
        //        {
        //            await context.PostAsync($"Um erro ocorreu. {botResponse.StatusCode}");
        //            return;
        //        }
        //        else
        //        {
        //            var json = await botResponse.Content.ReadAsStringAsync();
        //            await context.PostAsync($"{json}");
        //        }
        //    }
        //}
    }
}