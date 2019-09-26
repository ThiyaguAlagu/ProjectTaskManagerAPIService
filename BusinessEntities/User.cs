using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeId { get; set; }
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }
        public Project UserProject { get; set; }
        public Task UserTask { get; set; }
        public bool IsDeleted { get; set; }
    }
}

