using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Concord.Logging.Models
{
    public class Environment
    {
        [Column("Environment_ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        public Environment()
        {
        }

        public Environment(EnvironmentType type)
        {
            this.Name = type.ToString();
        }
    }
}
