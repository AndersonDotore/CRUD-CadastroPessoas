using CadastroPessoa.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoa.Data
{
    public class CadastroPessoaDbContext : DbContext
    {
        public CadastroPessoaDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
 