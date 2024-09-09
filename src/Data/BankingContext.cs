using Microsoft.EntityFrameworkCore;
using apiTransferencia.Models;
using System.Collections.Generic;

namespace apiTransferencia.Data
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
    }
}
