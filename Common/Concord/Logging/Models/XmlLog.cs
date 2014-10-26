﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Concord.Logging.Models
{
    public class XmlLog : LogBase
    {
        [Column("XmlLog_ID")]
        public override int ID { get; set; }

        public string Xml { get; set; }

        [StringLength(20)]
        public string Type { get; set; }
    }
}