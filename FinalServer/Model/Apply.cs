using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Model
{
    public class Apply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long Applyid { get; set; }
        public long EmployersId { get; set; }
        public string CompanyName { get; set; }
        public long JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobType { get; set; }
        public DateTime LastDate { get; set; }
        public long JobSeekerId { get; set; }
        public string Filepath { get; set; }
        public string Name { get; set; }

    }
}
