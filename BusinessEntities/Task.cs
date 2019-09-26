using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int? ParentId { get; set; }
        public ParentTask Parent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }
    }
}

