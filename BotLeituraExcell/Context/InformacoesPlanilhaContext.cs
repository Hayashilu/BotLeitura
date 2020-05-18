using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using BotLeituraExcell.Data;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BotLeituraExcell.Context
{
    class InformacoesPlanilhaContext : DbContext
    {
        public InformacoesPlanilhaContext() : base("name=InformacoesPlanilhaContext")
        {
        }

        public DbSet<Incidentes> Incidentes { get; set; }
        public DbSet<Problemas> Problemas { get; set; }
        public DbSet<Solicitacoes> Solicitacoes { get; set; }
        public DbSet<Severidade> Severidade { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<GrupoExecutor> GrupoExecutor { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Violado> Violado { get; set; }
        public DbSet<Localidade> Localidade { get; set; }
        public DbSet<ClassChamadoFinal> ClassChamadoFinal { get; set; }
        public DbSet<UsuarioFinal> UsuarioFinals { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Origem> Origems { get; set; }
        public DbSet<Prazo> Prazos { get; set; }
        public DbSet<Atribuido> Atribuidos { get; set; }
    }
}
