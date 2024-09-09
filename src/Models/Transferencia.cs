namespace apiTransferencia.Models
{
    public class Transferencia
    {
        public Guid Id { get; set; }
        public string NumeroContaOrigem { get; set; }
        public string NumeroContaDestino { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransferencia { get; set; }
        public bool Sucesso { get; set; }
    }
}
