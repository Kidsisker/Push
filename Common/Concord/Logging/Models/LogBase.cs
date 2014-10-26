using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Concord.Logging.Models
{
    public abstract class LogBase
    {
        public abstract int ID { get; set; }

        public virtual Request Request { get; set; }

        [Required]
        public DateTime LogDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        
        public Guid? SystemGuid { get; set; }

        [StringLength(100)]
        public string SessionToken { get; set; }

        [StringLength(50)]
        public string Username { get; set; }



        public LogBase()
        {
            this.LogDate = DateTime.Now;
        }
    }
}
