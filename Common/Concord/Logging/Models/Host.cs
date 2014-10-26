using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace Concord.Logging.Models
{
    /// <summary>
    /// machine details for an application host
    /// </summary>
    public class Host
    {
        [Column("Host_ID")]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(20)]
        public string IP { get; set; }

        [StringLength(20)]
        public string Alias { get; set; }

        [StringLength(50)]
        public string OSVersion { get; set; }



        public Host()
        {
            this.Name = System.Environment.MachineName;
            this.OSVersion = System.Environment.OSVersion.ToString();

        }
    }
}
