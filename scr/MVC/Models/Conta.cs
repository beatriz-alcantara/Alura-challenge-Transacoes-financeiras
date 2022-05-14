namespace Alura_Challange_Transacao_Financeira.Models
{
    public struct Conta
    {
        public string Banco { get; set; }
        public long NumeroConta { get; set; }
        public int Agencia { get; set; }

        public Conta(string banco, string agencia, string numeroConta)
        {
            Banco = banco;
            NumeroConta = long.Parse(numeroConta);
            Agencia = int.Parse(agencia);
        }
    }
}
