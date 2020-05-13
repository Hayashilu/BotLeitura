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
            List<Incidentes> listaParaAdcTabela = new List<Incidentes>();
            IEnumerable<FileInfo> informacoesArquivos = execute.verificarPastaTotal();

            foreach (var info in informacoesArquivos)
            {
                Severidade intSeveridade = new Severidade();
                DateTime mesVerifcAbertura = new DateTime();
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

                        listaParaAdcTabela.Add(infoAdd);
                    }
                }
                adicionarDadosTabela(listaParaAdcTabela);
                listaParaAdcTabela = new List<Incidentes>();
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

        public List<Incidentes> lerArquivoUnico(FileInfo[] arquivo)
        {
            List<Incidentes> listaParaAdcTabela = new List<Incidentes>();
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

                    //InformacoesPlanilha infoAdd = new InformacoesPlanilha();
                    //infoAdd.numeroIncidente = dataRow.Cell(1).Value.ToString();
                    //infoAdd.resumo = dataRow.Cell(2).Value.ToString();
                    //infoAdd.severidade = repositorio.modSeveridade(dataRow.Cell(3).Value.ToString());
                    //infoAdd.categoria = repositorio.modCategoria(dataRow.Cell(4).Value.ToString());
                    //infoAdd.status = repositorio.modStatus(dataRow.Cell(5).Value.ToString());
                    //infoAdd.executor = repositorio.modExecutor(dataRow.Cell(6).Value.ToString());
                    //infoAdd.responsavel = repositorio.modResponsavel(dataRow.Cell(7).Value.ToString());
                    //infoAdd.violacao = verificaDateTime(dataRow.Cell(8).Value.ToString());
                    //infoAdd.violado = repositorio.modViolado(dataRow.Cell(9).Value.ToString());
                    //infoAdd.localidade = repositorio.modLocalidade(dataRow.Cell(10).Value.ToString());
                    //infoAdd.dataAbertura = verificaDateTime(dataRow.Cell(11).Value.ToString());
                    //infoAdd.ultimaAtualizacao = verificaDateTime(dataRow.Cell(12).Value.ToString());
                    //infoAdd.retornoChamado = dataRow.Cell(13).Value.ToString();
                    //infoAdd.classificacaoChamado = repositorio.modClassificacaoChamado(dataRow.Cell(14).Value.ToString());
                    //infoAdd.dataResolucao = verificaDateTime(dataRow.Cell(15).Value.ToString());
                    //infoAdd.descricao = dataRow.Cell(16).Value.ToString();
                    //infoAdd.usuarioAfetado = repositorio.modUsuario(dataRow.Cell(17).Value.ToString());
                    //infoAdd.departamento = repositorio.modDepartamento(dataRow.Cell(18).Value.ToString());
                    //infoAdd.problema = dataRow.Cell(19).Value.ToString();
                    //infoAdd.parent = dataRow.Cell(20).Value.ToString();
                    //infoAdd.causedByOrder = dataRow.Cell(21).Value.ToString();
                    //infoAdd.origem = repositorio.modOrigem(dataRow.Cell(22).Value.ToString());
                    //infoAdd.ticketExterno = dataRow.Cell(23).Value.ToString();

                    listaParaAdcTabela.Add(infoAdd);
                }
            }
            return listaParaAdcTabela;
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
            }
        }

        public void selecaoUm(DateTime data)
        {
            List<Incidentes> listaParaAdcTabela = new List<Incidentes>();
            string mesStr = string.Empty;
            string diaStr = string.Empty;
            string anoStr = string.Empty;

            mesStr = data.Month.ToString();
            anoStr = data.Year.ToString();
            diaStr = data.Day.ToString();
            FileInfo[] arquivo = execute.verificarPastaParcial(diaStr, mesStr, anoStr);
            if (arquivo.Count() == 0)
            {
                buscaSemArquivos();
            }
            listaParaAdcTabela = lerArquivoUnico(arquivo);
            adicionarDadosTabela(listaParaAdcTabela);
            Console.WriteLine("Leitura do arquivo executada com sucesso, linhas e informações do arquivo salvas !");
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
        }

        public DateTime verificaDateTime(string data)
        {
            DateTime dataConvertida = new DateTime();

            if(data == "" || data == null)
            {
                data = null;
            }

            return dataConvertida = Convert.ToDateTime(data);
        }

        public int verificarPeso(string severidade)
        {
            int peso = 0;
            if(severidade == "2")
            {
                return peso = 1;
            }
            else if(severidade == "3")
            {
                return peso = 3;
            }
            else if(severidade == "4")
            {
                return peso = 5;
            }
            else if(severidade == "5")
            {
                return peso = 20;
            }
            else
            {
                return peso;
            }
        }

        public DateTime dataReferenciaAjuste(string nomeArquivo)
        {
            string dataPrimeiroAjuste = nomeArquivo.Replace("_", "/");
            string dataSegundoAjuste = dataPrimeiroAjuste.Replace(".xlsx", "");

            DateTime dataReferencia = Convert.ToDateTime(dataSegundoAjuste);

            return dataReferencia;
        }

        public DateTime ajusteMes(string data)
        {
            string dataInicial = data.Substring(3);
            return Convert.ToDateTime(dataInicial);
        }

        public void adicionarDadosTabela(List<Incidentes> informacoesPlanilhas)
        {
            AcessDBRepository acess = new AcessDBRepository();

            acess.adcTabelaSqlComand(informacoesPlanilhas);
        }
    }
}
