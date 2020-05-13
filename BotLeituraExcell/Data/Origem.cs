using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Origem")]
    public class Origem
    {
        [Key]
        public int idOrigem { get; set; }

        public string infoOrigem { get; set; }
    }
}
