using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Localidade")]
    public class Localidade
    {
        [Key]
        public int idLocalidade { get; set; }

        public string infoLocalidade { get; set; }
    }
}
