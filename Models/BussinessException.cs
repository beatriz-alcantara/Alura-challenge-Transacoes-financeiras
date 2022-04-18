namespace Alura_Challange_Transacao_Financeira.Models
{
    public class BussinessException : Exception
    {
        public BussinessException(string message) : base(message)
        {
        }
    }
}
