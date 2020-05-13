using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("ClassChamadoFinal")]
    public class ClassChamadoFinal
    {
        [Key]
        public int idClassChamadoFinal { get; set; }

        public string infoClassChamadoFinal { get; set; }
    }
}
