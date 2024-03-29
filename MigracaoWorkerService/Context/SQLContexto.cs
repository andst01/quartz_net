using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MigracaoWorkerService.Service.Models.Execute;
using MigracaoWorkerService.Service.Mappings;

namespace MigracaoWorkerService.Context
{
    public class SQLContexto : DbContext
    {
       /// <summary>
       /// public SQLContexto(DbContextOptions options) : base(options) { }
       /// </summary>
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Funcao> Funcao { get; set; }

        public DbSet<FuncaoUsuario> FuncaoUsuario { get; set; }

        public DbSet<Pessoa> Pessoa { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Funcionario> Funcionario { get; set; }

        public DbSet<Fornecedor> Fornecedor { get; set; }

        public DbSet<Empresa> Empresa { get; set; }

        public DbSet<UnidadeVenda> UnidadeVenda { get; set; }

        public DbSet<Cargo> Cargo { get; set; }

        public DbSet<CargoFuncionario> CargoFuncionario { get; set; }

        public DbSet<Agenda> Agenda { get; set; }

        public DbSet<Agendamento> Agendamento { get; set; }

        public DbSet<Servico> Servico { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = $"Server=DESKTOP-1GGA1R2\\SQLEXPRESS;Database=SGAGNT;User Id=anderson;Password=123456;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                //.UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new FuncaoMap());
            modelBuilder.ApplyConfiguration(new FuncaoUsuarioMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new EmpresaMap());
            modelBuilder.ApplyConfiguration(new PessoaMap());
            modelBuilder.ApplyConfiguration(new UnidadeVendaMap());
            modelBuilder.ApplyConfiguration(new CargoMap());
            modelBuilder.ApplyConfiguration(new CargoFuncionarioMap());
            modelBuilder.ApplyConfiguration(new ServicoMap());
            modelBuilder.ApplyConfiguration(new AgendaMap());
            modelBuilder.ApplyConfiguration(new AgendamentoMap());


        }
    }
}
