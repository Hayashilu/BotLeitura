using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Resumo")]
    public class Resumo
    {
        [Key]
        public int idResumo { get; set; }

        public string informacaoResumo { get; set; }
    }
}
