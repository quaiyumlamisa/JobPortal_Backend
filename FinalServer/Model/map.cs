using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Model
{
    public class map
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long eId { get; set; }
        public long mapid { get; set; }
        public long seekerid { get; set; }
    }
}
