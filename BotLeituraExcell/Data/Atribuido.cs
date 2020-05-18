using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Atribuido")]
    public class Atribuido
    {
        [Key]
        public int idAtribuido { get; set; }

        public string infoAtribuido { get; set; }
    }
}
