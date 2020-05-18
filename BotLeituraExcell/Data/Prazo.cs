using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Prazo")]
    public class Prazo
    {
        [Key]
        public int idPrazo { get; set; }

        public string numeroPrazo { get; set; }

        public string infoPrazo { get; set; }
    }
}
