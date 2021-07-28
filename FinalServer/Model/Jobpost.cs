using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalServer.Model
{
    public class Jobpost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long mapId { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobType { get; set; }
        public string Experience { get; set; }
        public string Salary { get; set; }
        public int Vacancy { get; set; }
        public string Qualification { get; set; }
        public string Skills { get; set; }
        public DateTime LastDate { get; set; }
        public string Location { get; set; }
    }
}
