﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("InfoStatus")]
    public class Status
    {
        [Key]
        public int idStatus { get; set;  }

        public string infoStatus { get; set; }

    }
}