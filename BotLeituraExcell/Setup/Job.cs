using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BotLeituraExcell.Data;
using BotLeituraExcell.Acess;
using BotLeituraExcell.Enum;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Setup
{
    public class Job
    {
        Enum.Meses meses = new Meses();
        Setup.Execute execute = new Execute();
        Acess.AcessDBRepository repositorio = new AcessDBRepository();

        public void lerArquivo()
        {
            //Acessa arquivo execute
            bool primeiraExecucao = execute.primeiraExecucao();

            if (primeiraExecucao)
            {
                lerArquivosExecucaoTotal();
            }
            else
            {
                lerArquivosExecucaoParcial();
            }
        }

        public void lerArquivosExecucaoTotal()
        {
            List<Incidentes> listaParaAdcTabelaIncidentes = new List<Incidentes>();
            List<Problemas> listaParaAdcTabelaProblemas = new List<Problemas>();
            List<Solicitacoes> listaParaAdcTabelaSolicitacoes = new List<Solicitacoes>();

            string selecao = selecaoTipo();
            IEnumerable<FileInfo> informacoesArquivos = execute.verificarPastaTotal(selecao);

            foreach (var info in informacoesArquivos)
            {
                Severidade intSeveridade = new Severidade();
                DateTime mesVerifcAbertura = new DateTime();
                DateTime mesVerifcFechamento = new DateTime();
                DateTime mesVerifcResolucao = new DateTime();
                DateTime dataReferenciaSalva = dataReferenciaAjuste(info.Name);
                var workbook = new XLWorkbook(info.FullName);
                var ws1 = workbook.Worksheet(1);
                var linhasNaoVazias = ws1.RowsUsed();
                Console.WriteLine("O Arquivo na pasta " + info.FullName);
                Console.WriteLine("Possui o total de " + (linhasNaoVazias.Count() - 1) + " usadas");

                foreach (var dataRow in linhasNaoVazias)
                {
                    if (dataRow.RowNumber() > 1)
                    {
                        if (selecao == "1")
                        {
                            intSeveridade = repositorio.modSeveridade(dataRow.Cell(3).Value.ToString());
                            mesVerifcAbertura = ajusteMes(verificaDateTime(dataRow.Cell(11).Value.ToString()).ToShortDateString());
                            mesVerifcResolucao = ajusteMes(verificaDateTime(dataRow.Cell(15).Value.ToString()).ToShortDateString());

                            #region Incidente
                            Incidentes infoAdd = new Incidentes()
                            {
                                numeroIncidente = dataRow.Cell(1).Value.ToString(),
                                resumo = dataRow.Cell(2).Value.ToString(),
                                idSeveridade = intSeveridade.idSeveridade,
                                idCategoria = repositorio.modCategoria(dataRow.Cell(4).Value.ToString()).idCategoria,
                                idStatus = repositorio.modStatus(dataRow.Cell(5).Value.ToString()).idStatus,
                                idGrupoExec = repositorio.modExecutor(dataRow.Cell(6).Value.ToString()).idGrupoExec,
                                idResponsavel = repositorio.modResponsavel(dataRow.Cell(7).Value.ToString()).idResponsavel,
                                violacao = verificaDateTime(dataRow.Cell(8).Value.ToString()),
                                idViolado = repositorio.modViolado(dataRow.Cell(9).Value.ToString()).idViolado,
                                idLocalidade = repositorio.modLocalidade(dataRow.Cell(10).Value.ToString()).idLocalidade,
                                dataAbertura = verificaDateTime(dataRow.Cell(11).Value.ToString()),
                                mesAbertura = mesVerifcAbertura,
                                ultimaAtualizacao = verificaDateTime(dataRow.Cell(12).Value.ToString()),
                                retornoChamado = dataRow.Cell(13).Value.ToString(),
                                idClassChamadoFinal = repositorio.modClassificacaoChamado(dataRow.Cell(14).Value.ToString()).idClassChamadoFinal,
                                dataResolucao = verificaDateTime(dataRow.Cell(15).Value.ToString()),
                                mesResolucao = mesVerifcResolucao,
                                descricao = dataRow.Cell(16).Value.ToString(),
                                idUsuarioFinal = repositorio.modUsuario(dataRow.Cell(17).Value.ToString()).idUsuarioFinal,
                                idDepartamento = repositorio.modDepartamento(dataRow.Cell(18).Value.ToString()).idDepartamento,
                                problema = dataRow.Cell(19).Value.ToString(),
                                parent = dataRow.Cell(20).Value.ToString(),
                                causedByOrder = dataRow.Cell(21).Value.ToString(),
                                idOrigem = repositorio.modOrigem(dataRow.Cell(22).Value.ToString()).idOrigem,
                                ticketExterno = dataRow.Cell(23).Value.ToString(),
                                dataReferencia = dataReferenciaSalva,
                            };

                            listaParaAdcTabelaIncidentes.Add(infoAdd);
                            #endregion
                        }
                        else if (selecao == "2")
                        {
                            intSeveridade = repositorio.modSeveridade(dataRow.Cell(4).Value.ToString());
                            mesVerifcAbertura = ajusteMes(verificaDateTime(dataRow.Cell(9).Value.ToString()).ToShortDateString());
                            mesVerifcResolucao = ajusteMes(verificaDateTime(dataRow.Cell(14).Value.ToString()).ToShortDateString());
                            mesVerifcFechamento = ajusteMes(verificaDateTime(dataRow.Cell(15).Value.ToString()).ToShortDateString());
                            #region Problemas
                            Problemas infoAdd = new Problemas()
                            {
                                numeroProblema = dataRow.Cell(1).Value.ToString(),
                                idPrazo = repositorio.modPrazo(dataRow.Cell(2).Value.ToString()),
                                resumo = dataRow.Cell(3).Value.ToString(),
                                idPrioridade = intSeveridade.idSeveridade,
                                idCategoria = repositorio.modCategoria(dataRow.Cell(5).Value.ToString()).idCategoria,
                                idGrupoExec = repositorio.modExecutor(dataRow.Cell(6).Value.ToString()).idGrupoExec,
                                idResponsavel = repositorio.modResponsavel(dataRow.Cell(7).Value.ToString()).idResponsavel,
                                idStatus = repositorio.modStatus(dataRow.Cell(8).Value.ToString()).idStatus,
                                dataAbertura = verificaDateTime(dataRow.Cell(9).Value.ToString()),
                                mesAbertura = mesVerifcAbertura,
                                idAtribuido = repositorio.modAtribuido(dataRow.Cell(10).Value.ToString()).idAtribuido,
                                idUsuarioFinal = repositorio.modUsuario(dataRow.Cell(11).Value.ToString()).idUsuarioFinal,
                                parent = dataRow.Cell(12).Value.ToString(),
                                causedByOrder = dataRow.Cell(13).Value.ToString(),
                                dataResolucao = verificaDateTime(dataRow.Cell(14).Value.ToString()),
                                mesResolucao = mesVerifcResolucao,
                                dataFechamento = verificaDateTime(dataRow.Cell(15).Value.ToString()),
                                mesFechamento = mesVerifcFechamento,
                                idDepartamento = repositorio.modDepartamento(dataRow.Cell(16).Value.ToString()).idDepartamento,
                                dataReferencia = dataReferenciaSalva,
                            };

                            listaParaAdcTabelaProblemas.Add(infoAdd);
                            #endregion
                        }
                        else if (selecao == "3")
                        {
                            intSeveridade = repositorio.modSeveridade(dataRow.Cell(3).Value.ToString());
                            mesVerifcAbertura = ajusteMes(verificaDateTime(dataRow.Cell(11).Value.ToString()).ToShortDateString());
                            mesVerifcResolucao = ajusteMes(verificaDateTime(dataRow.Cell(15).Value.ToString()).ToShortDateString());
                            #region Solicitacoes
                            Solicitacoes infoAdd = new Solicitacoes()
                            {
                                numeroSolicitacao = dataRow.Cell(1).Value.ToString(),
                                resumo = dataRow.Cell(2).Value.ToString(),
                                idSeveridade = intSeveridade.idSeveridade,
                                idCategoria = repositorio.modCategoria(dataRow.Cell(4).Value.ToString()).idCategoria,
                                idStatus = repositorio.modStatus(dataRow.Cell(5).Value.ToString()).idStatus,
                                idGrupoExec = repositorio.modExecutor(dataRow.Cell(6).Value.ToString()).idGrupoExec,
                                idResponsavel = repositorio.modResponsavel(dataRow.Cell(7).Value.ToString()).idResponsavel,
                                violacao = verificaDateTime(dataRow.Cell(8).Value.ToString()),
                                idViolado = repositorio.modViolado(dataRow.Cell(9).Value.ToString()).idViolado,
                                idLocalidade = repositorio.modLocalidade(dataRow.Cell(10).Value.ToString()).idLocalidade,
                                dataAbertura = verificaDateTime(dataRow.Cell(11).Value.ToString()),
                                mesAbertura = mesVerifcAbertura,
                                ultimaAtualizacao = verificaDateTime(dataRow.Cell(12).Value.ToString()),
                                retornoChamado = dataRow.Cell(13).Value.ToString(),
                                idClassChamadoFinal = repositorio.modClassificacaoChamado(dataRow.Cell(14).Value.ToString()).idClassChamadoFinal,
                                dataResolucao = verificaDateTime(dataRow.Cell(15).Value.ToString()),
                                mesResolucao = mesVerifcResolucao,
                                descricao = dataRow.Cell(16).Value.ToString(),
                                idUsuarioFinal = repositorio.modUsuario(dataRow.Cell(17).Value.ToString()).idUsuarioFinal,
                                idDepartamento = repositorio.modDepartamento(dataRow.Cell(18).Value.ToString()).idDepartamento,
                                parent = dataRow.Cell(19).Value.ToString(),
                                causedByOrder = dataRow.Cell(20).Value.ToString(),
                                idOrigem = repositorio.modOrigem(dataRow.Cell(21).Value.ToString()).idOrigem,
                                ticketExterno = dataRow.Cell(22).Value.ToString(),
                                dataReferencia = dataReferenciaSalva,
                            };

                            listaParaAdcTabelaSolicitacoes.Add(infoAdd);
                            #endregion
                        }
                        else
                        {
                            Console.WriteLine("Ocorreu um erro! Dado inserido na seleção é incorreto");
                            Console.WriteLine("Selecione novamente o tipo");
                            lerArquivosExecucaoTotal();
                        }
                    }
                }
                if (selecao == "1")
                {
                    adicionarDadosTabelaIncidentes(listaParaAdcTabelaIncidentes);
                    listaParaAdcTabelaIncidentes = new List<Incidentes>();
                }
                else if (selecao == "2")
                {
                    adicionarDadosTabelaProblemas(listaParaAdcTabelaProblemas);
                    listaParaAdcTabelaProblemas = new List<Problemas>();
                }
                else if (selecao == "3")
                {
                    adicionarDadosTabelaSolicitacoes(listaParaAdcTabelaSolicitacoes);
                    listaParaAdcTabelaSolicitacoes = new List<Solicitacoes>();
                }
                Console.WriteLine("Leitura do arquivo executada com sucesso, linhas e informações do arquivo salvas !");
            }
        }

        public void lerArquivosExecucaoParcial()
        {
            Console.WriteLine("O que você deseja executar ? ");
            Console.WriteLine("1 - Data anterior ");
            Console.WriteLine("2 - Selecionar Data");
            Console.WriteLine("3 - Encerrar execução");
            Console.Write("Digite o numero selecionado e pressione ENTER:  ");
            string selecao = Console.ReadLine();

            if (selecao == "1")
            {
                DateTime data = execute.ObterProximoDiaUtil(DateTime.Now.AddDays(-1));
                selecaoUm(data);
            }
            else if (selecao == "2")
            {
                selecaoDois();
            }
            else if (selecao == "3")
            {
                selecaoTres();
            }
            else
            {
                Console.WriteLine("Deesculpe não foi inserido um dado valido, pressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                lerArquivosExecucaoParcial();
            }
        }

        public List<Incidentes> lerArquivoUnicoIncidente(FileInfo[] arquivo)
        {
            List<Incidentes> listaParaAdcTabelaIncidentes = new List<Incidentes>();

            Severidade intSeveridade = new Severidade();
            DateTime mesVerifcAbertura = new DateTime();
            DateTime mesVerifcResolucao = new DateTime();
            DateTime dataReferenciaSalva = dataReferenciaAjuste(arquivo[0].Name);
            var workbook = new XLWorkbook(arquivo[0].FullName);
            var ws1 = workbook.Worksheet(1);
            var linhasNaoVazias = ws1.RowsUsed();
            Console.WriteLine("O Arquivo " + arquivo[0].FullName);
            Console.WriteLine("Possui o total de " + (linhasNaoVazias.Count() - 1) + " usadas");

            foreach (var dataRow in linhasNaoVazias)
            {
                if (dataRow.RowNumber() > 1)
                {
                    intSeveridade = repositorio.modSeveridade(dataRow.Cell(3).Value.ToString());
                    mesVerifcAbertura = ajusteMes(verificaDateTime(dataRow.Cell(11).Value.ToString()).ToShortDateString());
                    mesVerifcResolucao = ajusteMes(verificaDateTime(dataRow.Cell(15).Value.ToString()).ToShortDateString());
                    Incidentes infoAdd = new Incidentes()
                    {
                        numeroIncidente = dataRow.Cell(1).Value.ToString(),
                        resumo = dataRow.Cell(2).Value.ToString(),
                        idSeveridade = intSeveridade.idSeveridade,
                        idCategoria = repositorio.modCategoria(dataRow.Cell(4).Value.ToString()).idCategoria,
                        idStatus = repositorio.modStatus(dataRow.Cell(5).Value.ToString()).idStatus,
                        idGrupoExec = repositorio.modExecutor(dataRow.Cell(6).Value.ToString()).idGrupoExec,
                        idResponsavel = repositorio.modResponsavel(dataRow.Cell(7).Value.ToString()).idResponsavel,
                        violacao = verificaDateTime(dataRow.Cell(8).Value.ToString()),
                        idViolado = repositorio.modViolado(dataRow.Cell(9).Value.ToString()).idViolado,
                        idLocalidade = repositorio.modLocalidade(dataRow.Cell(10).Value.ToString()).idLocalidade,
                        dataAbertura = verificaDateTime(dataRow.Cell(11).Value.ToString()),
                        mesAbertura = mesVerifcAbertura,
                        ultimaAtualizacao = verificaDateTime(dataRow.Cell(12).Value.ToString()),
                        retornoChamado = dataRow.Cell(13).Value.ToString(),
                        idClassChamadoFinal = repositorio.modClassificacaoChamado(dataRow.Cell(14).Value.ToString()).idClassChamadoFinal,
                        dataResolucao = verificaDateTime(dataRow.Cell(15).Value.ToString()),
                        mesResolucao = mesVerifcResolucao,
                        descricao = dataRow.Cell(16).Value.ToString(),
                        idUsuarioFinal = repositorio.modUsuario(dataRow.Cell(17).Value.ToString()).idUsuarioFinal,
                        idDepartamento = repositorio.modDepartamento(dataRow.Cell(18).Value.ToString()).idDepartamento,
                        problema = dataRow.Cell(19).Value.ToString(),
                        parent = dataRow.Cell(20).Value.ToString(),
                        causedByOrder = dataRow.Cell(21).Value.ToString(),
                        idOrigem = repositorio.modOrigem(dataRow.Cell(22).Value.ToString()).idOrigem,
                        ticketExterno = dataRow.Cell(23).Value.ToString(),
                        dataReferencia = dataReferenciaSalva,
                    };

                    listaParaAdcTabelaIncidentes.Add(infoAdd);
                }
            }
            return listaParaAdcTabelaIncidentes;
        }

        public List<Problemas> lerArquivoUnicoProblema(FileInfo[] arquivo)
        {
            List<Problemas> listaParaAdcTabelaProblemas = new List<Problemas>();

            Severidade intSeveridade = new Severidade();
            DateTime mesVerifcAbertura = new DateTime();
            DateTime mesVerifcResolucao = new DateTime();
            DateTime mesVerifcFechamento = new DateTime();
            DateTime dataReferenciaSalva = dataReferenciaAjuste(arquivo[0].Name);
            var workbook = new XLWorkbook(arquivo[0].FullName);
            var ws1 = workbook.Worksheet(1);
            var linhasNaoVazias = ws1.RowsUsed();
            Console.WriteLine("O Arquivo " + arquivo[0].FullName);
            Console.WriteLine("Possui o total de " + (linhasNaoVazias.Count() - 1) + " usadas");

            foreach (var dataRow in linhasNaoVazias)
            {
                if (dataRow.RowNumber() > 1)
                {
                    intSeveridade = repositorio.modSeveridade(dataRow.Cell(4).Value.ToString());
                    mesVerifcAbertura = ajusteMes(verificaDateTime(dataRow.Cell(9).Value.ToString()).ToShortDateString());
                    mesVerifcResolucao = ajusteMes(verificaDateTime(dataRow.Cell(14).Value.ToString()).ToShortDateString());
                    mesVerifcFechamento = ajusteMes(verificaDateTime(dataRow.Cell(15).Value.ToString()).ToShortDateString());
                    Problemas infoAdd = new Problemas()
                    {
                        numeroProblema = dataRow.Cell(1).Value.ToString(),
                        idPrazo = repositorio.modPrazo(dataRow.Cell(2).Value.ToString()),
                        resumo = dataRow.Cell(3).Value.ToString(),
                        idPrioridade = intSeveridade.idSeveridade,
                        idCategoria = repositorio.modCategoria(dataRow.Cell(5).Value.ToString()).idCategoria,
                        idGrupoExec = repositorio.modExecutor(dataRow.Cell(6).Value.ToString()).idGrupoExec,
                        idResponsavel = repositorio.modResponsavel(dataRow.Cell(7).Value.ToString()).idResponsavel,
                        idStatus = repositorio.modStatus(dataRow.Cell(8).Value.ToString()).idStatus,
                        dataAbertura = verificaDateTime(dataRow.Cell(9).Value.ToString()),
                        mesAbertura = mesVerifcAbertura,
                        idAtribuido = repositorio.modResponsavel(dataRow.Cell(10).Value.ToString()).idResponsavel,
                        idUsuarioFinal = repositorio.modUsuario(dataRow.Cell(11).Value.ToString()).idUsuarioFinal,
                        parent = dataRow.Cell(12).Value.ToString(),
                        causedByOrder = dataRow.Cell(13).Value.ToString(),
                        dataResolucao = verificaDateTime(dataRow.Cell(14).Value.ToString()),
                        mesResolucao = mesVerifcResolucao,
                        dataFechamento = verificaDateTime(dataRow.Cell(15).Value.ToString()),
                        mesFechamento = mesVerifcFechamento,
                        idDepartamento = repositorio.modDepartamento(dataRow.Cell(16).Value.ToString()).idDepartamento,
                        dataReferencia = dataReferenciaSalva,
                    };

                    listaParaAdcTabelaProblemas.Add(infoAdd);
                }
            }
            return listaParaAdcTabelaProblemas;
        }

        public List<Solicitacoes> lerArquivoUnicoSolicitacoes(FileInfo[] arquivo)
        {
            List<Solicitacoes> listaParaAdcTabelaSolicitacoes = new List<Solicitacoes>();

            Severidade intSeveridade = new Severidade();
            DateTime mesVerifcAbertura = new DateTime();
            DateTime mesVerifcResolucao = new DateTime();
            DateTime dataReferenciaSalva = dataReferenciaAjuste(arquivo[0].Name);
            var workbook = new XLWorkbook(arquivo[0].FullName);
            var ws1 = workbook.Worksheet(1);
            var linhasNaoVazias = ws1.RowsUsed();
            Console.WriteLine("O Arquivo de Solicitações " + arquivo[0].FullName);
            Console.WriteLine("Possui o total de " + (linhasNaoVazias.Count() - 1) + " usadas");

            foreach (var dataRow in linhasNaoVazias)
            {
                if (dataRow.RowNumber() > 1)
                {
                    intSeveridade = repositorio.modSeveridade(dataRow.Cell(3).Value.ToString());
                    mesVerifcAbertura = ajusteMes(verificaDateTime(dataRow.Cell(11).Value.ToString()).ToShortDateString());
                    mesVerifcResolucao = ajusteMes(verificaDateTime(dataRow.Cell(15).Value.ToString()).ToShortDateString());
                    Solicitacoes infoAdd = new Solicitacoes()
                    {
                        numeroSolicitacao = dataRow.Cell(1).Value.ToString(),
                        resumo = dataRow.Cell(2).Value.ToString(),
                        idSeveridade = intSeveridade.idSeveridade,
                        idCategoria = repositorio.modCategoria(dataRow.Cell(4).Value.ToString()).idCategoria,
                        idStatus = repositorio.modStatus(dataRow.Cell(5).Value.ToString()).idStatus,
                        idGrupoExec = repositorio.modExecutor(dataRow.Cell(6).Value.ToString()).idGrupoExec,
                        idResponsavel = repositorio.modResponsavel(dataRow.Cell(7).Value.ToString()).idResponsavel,
                        violacao = verificaDateTime(dataRow.Cell(8).Value.ToString()),
                        idViolado = repositorio.modViolado(dataRow.Cell(9).Value.ToString()).idViolado,
                        idLocalidade = repositorio.modLocalidade(dataRow.Cell(10).Value.ToString()).idLocalidade,
                        dataAbertura = verificaDateTime(dataRow.Cell(11).Value.ToString()),
                        mesAbertura = mesVerifcAbertura,
                        ultimaAtualizacao = verificaDateTime(dataRow.Cell(12).Value.ToString()),
                        retornoChamado = dataRow.Cell(13).Value.ToString(),
                        idClassChamadoFinal = repositorio.modClassificacaoChamado(dataRow.Cell(14).Value.ToString()).idClassChamadoFinal,
                        dataResolucao = verificaDateTime(dataRow.Cell(15).Value.ToString()),
                        mesResolucao = mesVerifcResolucao,
                        descricao = dataRow.Cell(16).Value.ToString(),
                        idUsuarioFinal = repositorio.modUsuario(dataRow.Cell(17).Value.ToString()).idUsuarioFinal,
                        idDepartamento = repositorio.modDepartamento(dataRow.Cell(18).Value.ToString()).idDepartamento,
                        parent = dataRow.Cell(19).Value.ToString(),
                        causedByOrder = dataRow.Cell(20).Value.ToString(),
                        idOrigem = repositorio.modOrigem(dataRow.Cell(21).Value.ToString()).idOrigem,
                        ticketExterno = dataRow.Cell(22).Value.ToString(),
                        dataReferencia = dataReferenciaSalva,
                    };

                    listaParaAdcTabelaSolicitacoes.Add(infoAdd);
                }
            }
            return listaParaAdcTabelaSolicitacoes;
        }

        public string selecaoTipo()
        {
            Console.WriteLine("Deseja ler qual tipo de arquivo?");
            Console.WriteLine("1 - Incidentes");
            Console.WriteLine("2 - Problemas");
            Console.WriteLine("3 - Solicitações");
            Console.WriteLine("Digite o numero da opção e aperte ENTER");
            string selecao = Console.ReadLine();

            if (selecao == "1" || selecao == "2" || selecao == "3")
            {
                return selecao;
            }
            else
            {
                Console.WriteLine("Desculpe não foi inserido algo valido, irei refazer a pergunta");
                selecao = "Erro";
                return selecao;
            }
        }

        public void selecaoDois()
        {
            Console.WriteLine("Insira a data que deseja (DD/MM/YYYY):");
            string data = Console.ReadLine();
            if (execute.IsDate(data))
            {
                DateTime dataConvertida = verificaDateTime(data);
                selecaoUm(dataConvertida);
            }
            else
            {
                Console.WriteLine("Desculpe não foi inserido uma data valida!");
                Console.WriteLine("Deseja inserir uma nova data ou encerrar:");
                Console.WriteLine("1 - Nova Data ");
                Console.WriteLine("2 - Encerrar");
                Console.Write("Digite o numero selecionado e pressione ENTER:  ");
                string selecao = Console.ReadLine();
                if (selecao == "1")
                {
                    selecaoDois();
                }
                else if (selecao == "2")
                {
                    selecaoTres();
                }
                else
                {
                    Console.WriteLine("Desculpe não foi inserido algo valido, irei pedir para inserir uma nova data");
                    selecaoDois();
                }
            }
        }

        public void selecaoUm(DateTime data)
        {
            Console.WriteLine("A data (" + data.ToShortDateString() +  ") já foi selecionada agora selecione o tipo");
            string selecao = selecaoTipo();
            string selecaoTipoStr = string.Empty;
            List<Incidentes> listaParaAdcTabelaIncidentes = new List<Incidentes>();
            List<Problemas> listaParaAdcTabelaProblemas = new List<Problemas>();
            List<Solicitacoes> listaParaAdcTabelaSolicitacoes = new List<Solicitacoes>();
            string mesStr = string.Empty;
            string diaStr = string.Empty;
            string anoStr = string.Empty;
            if (selecao == "1")
            {
                selecaoTipoStr = "Incidentes";
            }
            else if (selecao == "2")
            {
                selecaoTipoStr = "Problemas";
            }
            else if (selecao == "3")
            {
                selecaoTipoStr = "Solicitacoes";
            }
            else
            {
                Console.WriteLine("Dado inserido incorretamente, vamos novamente");
                selecaoUm(data);
            }
            mesStr = data.Month.ToString();
            anoStr = data.Year.ToString();
            diaStr = data.Day.ToString();
            FileInfo[] arquivo = execute.verificarPastaParcial(diaStr, mesStr, anoStr, selecaoTipoStr);
            if(arquivo == null)
            {
                arquivoNulo();
            }
            else if(arquivo.Count() == 0)
            {
                buscaSemArquivos();
            }
            else
            {
                if (selecao == "1")
                {
                    listaParaAdcTabelaIncidentes = lerArquivoUnicoIncidente(arquivo);
                    adicionarDadosTabelaIncidentes(listaParaAdcTabelaIncidentes);
                }
                else if (selecao == "2")
                {
                    listaParaAdcTabelaProblemas = lerArquivoUnicoProblema(arquivo);
                    adicionarDadosTabelaProblemas(listaParaAdcTabelaProblemas);
                }
                else if (selecao == "3")
                {
                    listaParaAdcTabelaSolicitacoes = lerArquivoUnicoSolicitacoes(arquivo);
                    adicionarDadosTabelaSolicitacoes(listaParaAdcTabelaSolicitacoes);
                }
                Console.WriteLine("Leitura do arquivo executada com sucesso, linhas e informações do arquivo salvas !");
            }
        }

        public void arquivoNulo()
        {
            buscaSemArquivos();
        }

        public void selecaoTres()
        {
            Console.WriteLine("Execução encerrada com sucesso, pressione qualquer tecla ou ENTER para encerrar");
            Console.ReadKey();
        }

        public void buscaSemArquivos()
        {
            Console.WriteLine("Deseja inserir uma nova data ou encerrar:");
            Console.WriteLine("1 - Nova Data ");
            Console.WriteLine("2 - Encerrar");
            Console.Write("Digite o numero selecionado e pressione ENTER:  ");
            string selecao = Console.ReadLine();
            if (selecao == "1")
            {
                selecaoDois();
            }
            else if (selecao == "2")
            {
                selecaoTres();
            }
            else
            {
                Console.WriteLine("Desculpe não foi inserido algo valido, irei refazer a pergunta");
                buscaSemArquivos();
            }
        }

        public DateTime verificaDateTime(string data)
        {
            DateTime dataConvertida = new DateTime();

            if(data == "" || data == null)
            {
                data = null;
            }

            dataConvertida = Convert.ToDateTime(data);

            return dataConvertida;
        }

        public int verificarPeso(string severidade)
        {
            int peso = 0;
            if(severidade == "2")
            {
                peso = 1;
                return peso;
            }
            else if(severidade == "3")
            {
                peso = 3;
                return peso;
            }
            else if(severidade == "4")
            {
                peso = 5;
                return peso;
            }
            else if(severidade == "5")
            {
                peso = 20;
                return peso;
            }
            else
            {
                return peso;
            }
        }

        public DateTime dataReferenciaAjuste(string nomeArquivo)
        {
            string dataPrimeiroAjuste = nomeArquivo.Replace("_", "/");
            string removerTipo = dataPrimeiroAjuste.Replace(".xlsx", "");
            DateTime dataReferencia = Convert.ToDateTime(removerTipo);

            return dataReferencia;
        }

        public DateTime ajusteMes(string data)
        {
            string dataInicial = data.Substring(3);
            return Convert.ToDateTime(dataInicial);
        }

        public void adicionarDadosTabelaIncidentes(List<Incidentes> informacoesPlanilhas)
        {
            AcessDBRepository acess = new AcessDBRepository();

            acess.adcTabelaIncidentesSqlComand(informacoesPlanilhas);
        }

        public void adicionarDadosTabelaProblemas(List<Problemas> informacoesPlanilhas)
        {
            AcessDBRepository acess = new AcessDBRepository();

            acess.adcTabelaProblemasSqlComand(informacoesPlanilhas);
        }

        public void adicionarDadosTabelaSolicitacoes(List<Solicitacoes> informacoesPlanilhas)
        {
            AcessDBRepository acess = new AcessDBRepository();

            acess.adcTabelaSolicitacoesSqlComand(informacoesPlanilhas);
        }
    }
}
