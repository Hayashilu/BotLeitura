using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Violado")]
    public class Violado
    {
        [Key]
        public int idViolado{ get; set; }

        public string infoViolado { get; set; }

        public string descViolado { get; set; }
    }
}
