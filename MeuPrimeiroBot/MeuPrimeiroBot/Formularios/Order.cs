using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeuPrimeiroBot.Formularios
{
    // to send correctly between bot and user
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe, não entendi \"{0}\".")]
    public class Order
    {
        // salgadinhos, bebidas, entrega em casa ou não, confirmar dados
        public Salgadinhos Salgadinhos { get; set; }
        public Bebidas Bebidas { get; set; }
        public TipoEntrega TipoEntrega { get; set; }
        public CPFNaNota CPFNaNota { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public static IForm<Order> BuildForm()
        {
            var form = new FormBuilder<Order>();
            // padrão de apresentação dos itens na tela é botões - limita opções na tela :)
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            // options for confirmation/denial
            form.Configuration.Yes = new String[]{ "y", "s", "sim", "yes", "yep" };
            form.Configuration.No = new String[] { "n", "no", "não", "ñ", "nao", "not" };
            // first message that will be sent
            form.Message("Olá, seja bem vindo. Será um prazer atender você.");
            form.OnCompletion(async (context, order) => {
                // save in db, create order, integrate with services..

                // last message that will be sent
                await context.PostAsync("Seu pedido número X foi gerado e em instantes será entregue.");
            });

            return form.Build();
        }
    }

    // describe = the label
    [Describe("Tipo de Entrega")]
    // enum to allow sms flows, with options send with numeric info
    public enum TipoEntrega
    {
        // to identify where it starts, the first argument must be initialized as value 1
        // terms = synonymous
        [Terms("Retirar no Local", "Retirar", "Buscar", "retiro ai", "eu pego")]
        [Describe("Retirar no Local")]
        RetirarNoLocal = 1,
        [Terms("Entrega à Domicílio", "receber em casa", "casa", "motoboy")]
        [Describe("Entrega à Domicílio")]
        EntregaADomicilio
    }

    [Describe("Salgado")]
    public enum Salgadinhos
    {
        [Terms("Esfirra", "esfiha", "sfiha")]
        [Describe("Esfirra")]
        Esfirra = 1,
        [Terms("Quibe", "kibe")]
        [Describe("Quibe")]
        Quibe,
        [Terms("Coxinha", "Cochinha")]
        [Describe("Coxinha")]
        Coxinha
    }

    [Describe("Bebida")]
    public enum Bebidas
    {
        [Terms("Água", "agua", "h2o", "water")]
        [Describe("Água")]
        Agua = 1,
        [Terms("Refrigerante", "refri")]
        [Describe("Refrigerante")]
        Refrigerante,
        [Terms("Suco")]
        [Describe("Suco")]
        Suco
    }

    [Describe("CPF na Nota")]
    public enum CPFNaNota
    {
        [Terms("Sim", "yes", "yep", "y", "s")]
        [Describe("Sim")]
        Sim = 1,
        [Terms("Não", "n", "nao", "no", "not")]
        [Describe("Não")]
        Nao
    }
}