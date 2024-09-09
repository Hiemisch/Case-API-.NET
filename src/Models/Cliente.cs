using System.ComponentModel.DataAnnotations;

namespace apiTransferencia.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; }
    }
}
