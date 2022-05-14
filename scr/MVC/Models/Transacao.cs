namespace Alura_Challange_Transacao_Financeira.Models
{
    public class Transacao
    {
        public Conta ContaOrigem { get; set; }
        public Conta ContaDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }

        public static IList<Transacao> CreateList(string[][] linhas)
        {
            List<Transacao> transacoes = new List<Transacao>();
            DateTime dataTransacoes = DateTime.Parse(linhas[0][7]);
            foreach (string[] line in linhas)
            {
                var diferencaDatas = dataTransacoes.Date - DateTime.Parse(line[7]).Date;
                if (diferencaDatas.Days == 0 && line.All(item => !String.IsNullOrEmpty(item)))
                    transacoes.Add(new Transacao
                    {
                        ContaOrigem = new Conta(line[0], line[1], line[2].Replace("-", "")),
                        ContaDestino = new Conta(line[3], line[4], line[5].Replace("-", "")),
                        Valor = decimal.Parse(line[6]),
                        DataHora = DateTime.Parse(line[7])
                    });
            }
            return transacoes;
        }
    }

    
}
