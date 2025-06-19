using System;

namespace Sistema
{
    public enum Prioridade
    {
        Vermelha,
        Amarela,
        Verde
    }

    public class Paciente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public double PressaoArterial { get; set; }
        public double Temperatura { get; set; }
        public double Oxigenacao { get; set; }
        public Prioridade PrioridadeAtendimento { get; set; }

        public Paciente(string cpf, string nome, double pa, double temp, double oxi)
        {
            CPF = cpf;
            Nome = nome;
            PressaoArterial = pa;
            Temperatura = temp;
            Oxigenacao = oxi;
            PrioridadeAtendimento = CalcularPrioridade();
        }

        public Prioridade CalcularPrioridade()
        {
            if (PressaoArterial > 18 || Temperatura > 39 || Oxigenacao < 90)
                return Prioridade.Vermelha;
            if (PressaoArterial < 12 || Temperatura < 36 || Oxigenacao < 95)
                return Prioridade.Amarela;
            return Prioridade.Verde;
        }

        public void AtualizarDados(double pa, double temp, double oxi)
        {
            PressaoArterial = pa;
            Temperatura = temp;
            Oxigenacao = oxi;
            PrioridadeAtendimento = CalcularPrioridade();
        }
    }
}
