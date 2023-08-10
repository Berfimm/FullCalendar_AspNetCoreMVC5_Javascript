using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TCPortal.Models
{
    public class RDWorklogTracking
    {
        [Key]
        public int ID { get; set; }
        public int WL_Row { get; set; }
        public string WL_No { get; set; }
        public string WL_CreatedBy { get; set; }
        public int WL_CreatedById { get; set; }
        public Nullable<DateTime> WL_CreatedDate { get; set; }
        public Nullable<DateTime> WL_StartDate { get; set; }
        public Nullable<DateTime> WL_FinishDate { get; set; }
        public string WL_Project { get; set; }
        public string WL_Title { get; set; }
        public string WL_Description { get; set; }

    }
}
