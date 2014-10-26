using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Concord.Logging.Models
{
    public class Application
    {
        [Column("Application_ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Version { get; set; }

        /// <summary>
        /// friendly name for the application
        /// </summary>
        [StringLength(20)]
        public string Alias { get; set; }

        public Environment Environment { get; set; }
    }
}
