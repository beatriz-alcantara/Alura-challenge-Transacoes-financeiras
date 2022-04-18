namespace Alura_Challange_Transacao_Financeira.Models
{
    public class FileData
    {
        public string Nome { get; set; }
        private float _tamanho;
        public float Tamanho
        {
            get { return _tamanho; }
            set
            {
                _tamanho = value / (float) Math.Pow(10, 6);
            }
        }

    }
}
