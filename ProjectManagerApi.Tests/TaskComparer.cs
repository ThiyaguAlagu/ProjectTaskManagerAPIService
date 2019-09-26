using BusinessEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagerApi.Tests
{
    public class TaskComparer : IComparer, IComparer<Task>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Task;
            var rhs = actual as Task;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }
        public int Compare(Task expected, Task actual)
        {
            int temp;
            return (temp = expected.TaskId.CompareTo(actual.TaskId)) != 0 ? temp : expected.TaskName.CompareTo(actual.TaskName);
        }
    }
    public class ParentTaskComparer : IComparer, IComparer<ParentTask>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as ParentTask;
            var rhs = actual as ParentTask;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }
        public int Compare(ParentTask expected, ParentTask actual)
        {
            int temp;
            return (temp = expected.ParentId.CompareTo(actual.ParentId)) != 0 ? temp : expected.ParentTaskName.CompareTo(actual.ParentTaskName);
        }
    }
}

