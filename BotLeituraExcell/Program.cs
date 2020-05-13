using BotLeituraExcell.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Job executar = new Job();
                Console.WriteLine("Executando programa...!");
                executar.lerArquivo();
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocorreu um erro durante a execução!");
                Console.WriteLine("Erro : " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
