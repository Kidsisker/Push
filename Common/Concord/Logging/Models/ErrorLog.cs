using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Concord.Logging.Models
{
    public class ErrorLog : LogBase
    {
        //public string StackTrace { get; set; }

        [Column("ErrorLog_ID")]
        public override int ID { get; set; }

        public string FullMessage { get; set; }

        [StringLength(200)]
        public string Type { get; set; }

    }
}
