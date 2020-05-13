using BotLeituraExcell.Context;
using BotLeituraExcell.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Acess
{
    public class AcessDBRepository
    {

        public void adcTabelaSqlComand(List<Incidentes> informacoesPlanilhas)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                //foreach (var item in informacoesPlanilhas)
                //{
                db.InformacoesPlanilha.AddRange(informacoesPlanilhas);
                Console.WriteLine("Foram adicionados o total de " + informacoesPlanilhas.Count() + " linhas na database");
                db.SaveChanges();
                //}

            }
        }

        public Categoria modCategoria(string colunaCategoria)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                List<Categoria> categoria = db.Categorias.Where(x => x.infoCategoria.Contains(colunaCategoria)).ToList();

                if (categoria.Count() == 0)
                {
                    Categoria categoriaAdd = new Categoria { infoCategoria = colunaCategoria };
                    db.Categorias.Add(categoriaAdd);
                    db.SaveChanges();
                    categoria = db.Categorias.Where(x => x.infoCategoria.Contains(colunaCategoria)).ToList();
                }

                return categoria[0];
            }
        }

        public Status modStatus(string colunaStatus)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                List<Status> status = db.Status.Where(x => x.infoStatus.Contains(colunaStatus)).ToList();

                if (status.Count() == 0)
                {
                    Status statusAdd = new Status { infoStatus = colunaStatus };
                    db.Status.Add(statusAdd);
                    db.SaveChanges();
                    status = db.Status.Where(x => x.infoStatus.Contains(colunaStatus)).ToList();
                }

                return status[0];
            }
        }

        public Severidade modSeveridade(string colunaSeveridade)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                List<Severidade> severidadeBusca = db.Severidade.Where(x => x.infoSeveridade.Contains(colunaSeveridade)).ToList();

                if (severidadeBusca.Count() == 0)
                {
                    Severidade severidade = new Severidade() { infoSeveridade = colunaSeveridade , peso = 0 };
                    if (colunaSeveridade == "2")
                    {
                        severidade.peso = 1;
                    }
                    else if (colunaSeveridade == "3")
                    {
                        severidade.peso = 3;
                    }
                    else if (colunaSeveridade == "4")
                    {
                        severidade.peso = 5;
                    }
                    else if (colunaSeveridade == "5")
                    {
                        severidade.peso = 20;
                    }
                    else
                    {
                        severidade.peso = 0;
                    }
                    db.Severidade.Add(severidade);
                    db.SaveChanges();
                    severidadeBusca = db.Severidade.Where(x => x.infoSeveridade.Contains(colunaSeveridade)).ToList();
                }

                return severidadeBusca[0];
            }
        }

        public GrupoExecutor modExecutor(string colunaExecutor)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                List<GrupoExecutor> executorBusca = db.GrupoExecutor.Where(x => x.infoGrupoExec.Contains(colunaExecutor)).ToList();

                if (executorBusca.Count() == 0)
                {
                    GrupoExecutor grupoExecutor = new GrupoExecutor() { infoGrupoExec = colunaExecutor };
                    db.GrupoExecutor.Add(grupoExecutor);
                    db.SaveChanges();
                    executorBusca = db.GrupoExecutor.Where(x => x.infoGrupoExec.Contains(colunaExecutor)).ToList();
                }

                return executorBusca[0];
            }
        }

        public Responsavel modResponsavel(string colunaResponsavel)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                List<Responsavel> responsavelBusca = db.Responsavel.Where(x => x.infoResponsavel.Contains(colunaResponsavel)).ToList();

                if (responsavelBusca.Count() == 0)
                {
                    Responsavel responsavel = new Responsavel() { infoResponsavel = colunaResponsavel };
                    db.Responsavel.Add(responsavel);
                    db.SaveChanges();
                    responsavelBusca = db.Responsavel.Where(x => x.infoResponsavel.Contains(colunaResponsavel)).ToList();
                }

                return responsavelBusca[0];
            }
        }

        public Violado modViolado(string colunaViolado)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                var violadoBusca = db.Violado.Where(x => x.infoViolado.Contains(colunaViolado)).ToList();

                if (violadoBusca.Count() == 0)
                {
                    Violado violado = new Violado() { infoViolado = colunaViolado };
                    if (violado.infoViolado == "1" || violado.infoViolado == "2")
                    {
                        violado.descViolado = "Violado";
                    }
                    else
                    {
                        violado.descViolado = "Não violado";
                    }

                    db.Violado.Add(violado);
                    db.SaveChanges();
                    violadoBusca = db.Violado.Where(x => x.infoViolado.Contains(colunaViolado)).ToList();
                }

                return violadoBusca[0];
            }
        }

        public Localidade modLocalidade(string coluna)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                var busca = db.Localidade.Where(x => x.infoLocalidade.Contains(coluna)).ToList();

                if (busca.Count() == 0)
                {
                    Localidade construtor = new Localidade() { infoLocalidade = coluna };
                    db.Localidade.Add(construtor);
                    db.SaveChanges();
                    busca = db.Localidade.Where(x => x.infoLocalidade.Contains(coluna)).ToList();
                }

                return busca[0];
            }
        }

        public ClassChamadoFinal modClassificacaoChamado(string coluna)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                var busca = db.ClassChamadoFinal.Where(x => x.infoClassChamadoFinal.Contains(coluna)).ToList();

                if (busca.Count() == 0)
                {
                    ClassChamadoFinal construtor = new ClassChamadoFinal() { infoClassChamadoFinal = coluna };

                    db.ClassChamadoFinal.Add(construtor);
                    db.SaveChanges();
                    busca = db.ClassChamadoFinal.Where(x => x.infoClassChamadoFinal.Contains(coluna)).ToList();

                }

                return busca[0];
            }
        }

        public UsuarioFinal modUsuario(string coluna)
        {
            using (var db = new InformacoesPlanilhaContext())
            {

                var busca = db.UsuarioFinals.Where(x => x.nomeUsuarioFinal.Contains(coluna)).ToList();

                if (busca.Count() == 0)
                {
                    UsuarioFinal construtor = new UsuarioFinal() { nomeUsuarioFinal = coluna };

                    db.UsuarioFinals.Add(construtor);
                    db.SaveChanges();
                    busca = db.UsuarioFinals.Where(x => x.nomeUsuarioFinal.Contains(coluna)).ToList();

                }

                return busca[0];
            }
        }

        public Departamento modDepartamento(string coluna)
        {
            using (var db = new InformacoesPlanilhaContext())
            {

                var busca = db.Departamentos.Where(x => x.infoDepartamento.Contains(coluna)).ToList();

                if (busca.Count() == 0)
                {
                    Departamento construtor = new Departamento() { infoDepartamento = coluna };

                    db.Departamentos.Add(construtor);
                    db.SaveChanges();
                    busca = db.Departamentos.Where(x => x.infoDepartamento.Contains(coluna)).ToList();

                }

                return busca[0];
            }
        }

        public Origem modOrigem(string coluna)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                var busca = db.Origems.Where(x => x.infoOrigem.Contains(coluna)).ToList();

                if (busca.Count() == 0)
                {
                    Origem construtor = new Origem() { infoOrigem = coluna };
                    db.Origems.Add(construtor);
                    db.SaveChanges();
                    busca = db.Origems.Where(x => x.infoOrigem.Contains(coluna)).ToList();

                }

                return busca[0];
            }
        }
    }
}
