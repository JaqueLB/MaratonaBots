
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;

namespace ManicureBot.Forms
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Desculpe, não entendi \"{0}\".")]
    public class ManicureOrder
    {
        [Describe("Tipo de Serviço")]
        public ServiceTypes ServiceType { get; set; }
        [Describe("Dia da Semana")]
        public Days Day {get; set;}
        [Describe("Horário")]
        public AvailableHours Hour { get; set; }
        [Describe("Forma de Pagamento")]
        public PaymentTypes PaymentType { get; set; }
        [Describe("Nome Completo")]
        public string Name { get; set; }
        [Describe("Endereço Completo")]
        public string Address { get; set; }
        [Describe("Telefone")]
        public string Phone { get; set; }
        [Describe("Email")]
        public string Email { get; set; }

        public static IForm<ManicureOrder> BuildForm()
        {
            var form = new FormBuilder<ManicureOrder>();
            // padrão de apresentação dos itens na tela é botões - limita opções na tela :)
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;
            // options for confirmation/denial
            form.Configuration.Yes = new String[] { "y", "s", "sim", "yes", "yep" };
            form.Configuration.No = new String[] { "n", "no", "não", "ñ", "nao", "not" };
        
            return form.Build();
        }
    }

    public enum PaymentTypes
    {
        // to identify where it starts, the first argument must be initialized as value 1
        // terms = synonymous
        [Terms("Dinheiro", "dinheiro", "money")]
        [Describe("Dinheiro")]
        Money = 1,
        [Terms("cartao", "cartão de débito", "debito", "débito")]
        [Describe("Cartão de Débito")]
        DebitCard
    }

    public enum AvailableHours
    {
        [Terms("oito", "oito da manha", "oito da manhã", "8", "8:00", "8h", "08:00")]
        [Describe("8h")]
        OitoDaManha = 8,
        [Terms("nove", "nove da manha", "nove da manhã", "9", "9:00", "9h", "09:00")]
        [Describe("9h")]
        NoveDaManha = 9,
        [Terms("dez", "dez da manha", "dez da manhã", "10", "10:00", "10h", "10:00")]
        [Describe("10h")]
        DezDaManha = 10,
        [Terms("onze", "onze da manha", "onze da manhã", "11", "11:00", "11h", "11:00")]
        [Describe("11h")]
        OnzeDaManha = 11,
        [Terms("duas da tarde", "14:00", "14h", "14:00", "quatorze", "catorze")]
        [Describe("14h")]
        DuasDaTarde = 14,
        [Terms("três da tarde", "tres da tarde", "15:00", "15h", "15", "15:00", "quinze")]
        [Describe("15h")]
        TresDaTarde = 15,
        [Terms("quatro da tarde", "16", "16:00", "16h", "16:00", "dezesseis")]
        [Describe("16h")]
        QuatroDaTarde = 16,
        [Terms("cinco da tarde", "17", "17:00", "17h", "17:00", "dezessete")]
        [Describe("17h")]
        CincoDaTarde = 17,
        [Terms("seis da tarde", "18:00", "18h", "18:00", "18", "dezoito")]
        [Describe("18h")]
        SeisDaTarde = 18
    }

    public enum Days
    {
        [Terms("segunda", "segunda-feira", "Segunda-Feira", "Segunda")]
        [Describe("Segunda-Feira")]
        Monday = 1,
        [Terms("terça", "terça-feira", "Terça-Feira", "Terça")]
        [Describe("Terça-Feira")]
        Tuesday,
        [Terms("quarta", "quarta-feira", "Quarta-Feira", "Quarta")]
        [Describe("Quarta-Feira")]
        Wednesday,
        [Terms("quinta", "quinta-feira", "Quinta-Feira", "Quinta")]
        [Describe("Quinta-Feira")]
        Thursday,
        [Terms("sexta", "sexta-feira", "Sexta-Feira", "Sexta")]
        [Describe("Sexta-Feira")]
        Friday,
        [Terms("sábado", "sabado", "Sábado", "Sabado")]
        [Describe("Sábado")]
        Saturday
    }

    public enum ServiceTypes
    {
        Manicure = 1,
        Pedicure,
        [Describe("Completo - Manicure e Pedicure")]
        Completo
    }
}