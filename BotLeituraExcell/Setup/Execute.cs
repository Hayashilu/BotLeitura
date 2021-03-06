﻿using BotLeituraExcell.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Setup
{
    public class Execute
    {
        public bool primeiraExecucao()
        {
            string primeiraExecucao = ConfigurationManager.AppSettings["PrimeiraExecucao"].ToString();

            if(primeiraExecucao.ToUpper() == "TRUE")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string caminhoArquivos()
        {
            string  caminho = ConfigurationManager.AppSettings["CaminhoArquivos"].ToString();
            return caminho;
        }

        public IEnumerable<FileInfo> verificarPastaTotal(string selecao)
        {
            string anoExec = ConfigurationManager.AppSettings["AnoPrimeiraExec"].ToString();
            string selecaoTipoStr = string.Empty;
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
            //Marca o diretório a ser listado
            string pasta = caminhoArquivos();
            DirectoryInfo diretorio = new DirectoryInfo(pasta + "\\" + anoExec + "\\" +  selecaoTipoStr);

            //Pega todos os arquivos presentes em pastas do tipo excell 
            IEnumerable<FileInfo> files = diretorio.EnumerateFiles("*.xlsx", SearchOption.AllDirectories);

            return files;
        }

        public FileInfo[] verificarPastaParcial(string dia, string mes, string ano, string selecaoTipo)
        {
            Meses meses = new Meses();
            string mesNome = meses.mesTransformacao(mes);

            if (Convert.ToInt16(mes) < 10)
            {
                mes = "0" + mes;
            }
            if (Convert.ToInt16(dia) < 10)
            {
                dia = "0" + dia;
            }

            //Marca o diretório a ser listado
            string pasta = caminhoArquivos();
            string caminhoArquivo = pasta + "\\"  + ano + "\\" + selecaoTipo +  "\\"  + mesNome;
            DirectoryInfo diretorio = new DirectoryInfo(caminhoArquivo);

            if (!diretorio.Exists)
            {
                Console.WriteLine("Diretorio da pasta referente ao mês informado não encontrado");
                SelecaoCasoErro();
            }
            //Pega todos os arquivos presentes em pastas do tipo excell xls e xlsx
            FileInfo[] file = diretorio.GetFiles(dia + ".xlsx");
            if (file.Length == 0)
            {
                file = diretorio.GetFiles(dia + ".xlsx");
            }

            if(file.Length == 0)
            {
                file = diretorio.GetFiles(dia + "_" + mes + "_" + ano.Substring(2) + ".xls");
                if (file.Length == 0)
                {
                    file = diretorio.GetFiles(dia + "_" + mes + "_" + ano.Substring(2) + ".xlsx");
                }

                if (file.Length == 0)
                {
                    Console.WriteLine("Nenhum arquivo referente ao que foi informado, foi não encontrado");
                    SelecaoCasoErro();
                }
            }

            if(file.Length != 0)
            {
                if (file[0].FullName.Contains(".xls") && !file[0].FullName.Contains(".xlsx"))
                {
                    Console.WriteLine("Infelizmente este programa não lê arquivios do tipo .xls, por serem antigos");
                    Console.WriteLine("Formate o arquivo e tente novamente");
                    file = null;
                }
            }

            return file;
        }

        public DateTime ObterProximoDiaUtil(DateTime data)
        {
            while (this.EhDiaUtil(data) == false)
            {
                data = data.AddDays(-1);
            }

            return data;
        }

        public bool EhDiaUtil(DateTime data)
        {
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return false;
            }

            return true;
        }

        public void SelecaoCasoErro()
        {
            Job job = new Job();
            job.buscaSemArquivos();
        }

        public bool IsDate(string data)

        {
            return IsDateValidate(data, "dd/MM/yyyy");
        }

        public bool IsDateValidate(string date, string format)
        {

            DateTime parsedDate;

            bool isValidDate;

            isValidDate = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

            return isValidDate;
        }
    }
}