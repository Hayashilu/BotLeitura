using BotLeituraExcell.Context;
using BotLeituraExcell.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Acess
{
    public class AcessDBRepository
    {

        public void adcTabelaIncidentesSqlComand(List<Incidentes> informacoesPlanilhas)
        {
            List<Incidentes> listaParaAdc = new List<Incidentes>();

            using (var db = new InformacoesPlanilhaContext())
            {
                //Remove itens que já existem de tal data
                verificaExistenciaIncidenteDataReferencia(informacoesPlanilhas);
                //Add 500 itens por vez
                int inicial = 0;
                int final = 500;
                int ultimo = 0;
                int totalDeVezes = informacoesPlanilhas.Count / 500;
                for (int i = 0; i <= totalDeVezes; i++)
                {
                    try
                    {
                        listaParaAdc = informacoesPlanilhas.GetRange(inicial, final);
                        inicial = 500 * (i + 1);
                    }
                    catch
                    {
                        ultimo = informacoesPlanilhas.Count() - inicial - 1;
                        listaParaAdc = informacoesPlanilhas.GetRange(inicial,ultimo);
                    }

                    db.Incidentes.AddRange(listaParaAdc);
                }

                Console.WriteLine("Foram adicionados o total de " + informacoesPlanilhas.Count() + " linhas na Tabela Incidentes");
                db.SaveChanges();
            }
        }

        public void adcTabelaProblemasSqlComand(List<Problemas> informacoesPlanilhas)
        {
            IEnumerable<Problemas> listaParaAdc = new List<Problemas>();

            using (var db = new InformacoesPlanilhaContext())
            {
                //Remove itens que já existem de tal data
                verificaExistenciaProblemaDataReferencia(informacoesPlanilhas);
                //Add 500 itens por vez
                int inicial = 0;
                int final = 500;
                int ultimo = 0;
                int totalDeVezes = informacoesPlanilhas.Count / 500;
                for (int i = 0; i <= totalDeVezes; i++)
                {
                    try
                    {
                        listaParaAdc = informacoesPlanilhas.GetRange(inicial, final);
                        inicial = 500 * (i + 1);
                    }
                    catch
                    {
                        ultimo = informacoesPlanilhas.Count() - inicial - 1;
                        listaParaAdc = informacoesPlanilhas.GetRange(inicial, ultimo);
                    }

                    db.Problemas.AddRange(listaParaAdc);
                }

                Console.WriteLine("Foram adicionados o total de " + informacoesPlanilhas.Count() + " linhas na Tabela Problemas");
                db.SaveChanges();
            }
        }

        public void adcTabelaSolicitacoesSqlComand(List<Solicitacoes> informacoesPlanilhas)
        {
            IEnumerable<Solicitacoes> listaParaAdc = new List<Solicitacoes>();

            using (var db = new InformacoesPlanilhaContext())
            {
                //Remove itens que já existem de tal data
                verificaExistenciaSolicitacaoDataReferencia(informacoesPlanilhas);
                //Add 500 itens por vez
                int inicial = 0;
                int final = 500;
                int ultimo = 0;
                int totalDeVezes = informacoesPlanilhas.Count / 500;
                for (int i = 0; i <= totalDeVezes; i++)
                {
                    try
                    {
                        listaParaAdc = informacoesPlanilhas.GetRange(inicial, final);
                        inicial = 500 * (i + 1);
                    }
                    catch
                    {
                        ultimo = informacoesPlanilhas.Count() - inicial - 1;
                        listaParaAdc = informacoesPlanilhas.GetRange(inicial, ultimo);
                    }

                    db.Solicitacoes.AddRange(listaParaAdc);
                }

                Console.WriteLine("Foram adicionados o total de " + informacoesPlanilhas.Count() + " linhas na Tabela Solicitações");
                db.SaveChanges();
            }
        }

        public int modPrazo(string coluna)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                var busca = db.Prazos.Where(x => x.numeroPrazo == coluna).ToList();

                if (busca.Count() == 0)
                {
                    Prazo construtor = new Prazo() { numeroPrazo = coluna };
                    db.Prazos.Add(construtor);
                    db.SaveChanges();
                    busca = db.Prazos.Where(x => x.numeroPrazo == coluna).ToList();
                }

                return busca[0].idPrazo;
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
                    Severidade severidade = new Severidade() { infoSeveridade = colunaSeveridade, peso = 0 };
                    if (colunaSeveridade.Contains("2"))
                    {
                        severidade.peso = 1;
                    }
                    else if (colunaSeveridade.Contains("3"))
                    {
                        severidade.peso = 3;
                    }
                    else if (colunaSeveridade.Contains("4"))
                    {
                        severidade.peso = 5;
                    }
                    else if (colunaSeveridade.Contains("5"))
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

        public Atribuido modAtribuido(string colunaAtribuido)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                List<Atribuido> atribuidoBusca = db.Atribuidos.Where(x => x.infoAtribuido.Contains(colunaAtribuido)).ToList();

                if (atribuidoBusca.Count() == 0)
                {
                    Atribuido Atribuido = new Atribuido() { infoAtribuido = colunaAtribuido };
                    db.Atribuidos.Add(Atribuido);
                    db.SaveChanges();
                    atribuidoBusca = db.Atribuidos.Where(x => x.infoAtribuido.Contains(colunaAtribuido)).ToList();
                }

                return atribuidoBusca[0];
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

        public void verificaExistenciaIncidenteDataReferencia(List<Incidentes> itens)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
                DateTime dataReferencia = itens[0].dataReferencia;
                var busca = db.Incidentes.Where(x => x.dataReferencia == dataReferencia).ToList();

                if (busca.Count() != 0)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    foreach (var itemIncidente in busca)
                    {
                        db.Incidentes.Attach(itemIncidente);
                        db.Entry(itemIncidente).State = EntityState.Deleted;
                    }
                    db.SaveChanges();

                }
            }
        }

        public void verificaExistenciaProblemaDataReferencia(List<Problemas> itens)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
                DateTime dataReferencia = itens[0].dataReferencia;
                var busca = db.Problemas.Where(x => x.dataReferencia == dataReferencia).ToList();

                if (busca.Count() != 0)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    foreach (var itemProblem in busca)
                    {
                        db.Problemas.Attach(itemProblem);
                        db.Entry(itemProblem).State = EntityState.Deleted;
                    }
                    db.SaveChanges();
                }
            }
        }

        public void verificaExistenciaSolicitacaoDataReferencia(List<Solicitacoes> itens)
        {
            using (var db = new InformacoesPlanilhaContext())
            {
                bool oldValidateOnSaveEnabled = db.Configuration.ValidateOnSaveEnabled;
                DateTime dataReferencia = itens[0].dataReferencia;
                var busca = db.Solicitacoes.Where(x => x.dataReferencia == dataReferencia).ToList();

                if (busca.Count() != 0)
                {
                    foreach (var itemSolicitacao in busca)
                    {
                        db.Solicitacoes.Attach(itemSolicitacao);
                        db.Entry(itemSolicitacao).State = EntityState.Deleted;
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
