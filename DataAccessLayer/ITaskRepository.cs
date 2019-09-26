using System.Collections.Generic;
using BusinessEntities;

namespace DataAccessLayer
{
    public interface ITaskRepository
    {
        bool AddParentTask(ParentTask task);
        bool AddTask(Task task);
        bool DeleteParentTask(ParentTask task);
        bool DeleteTask(Task task);
        bool EndTask(int taskId);
        List<ParentTask> GetAllParentTasks();
        List<Task> GetAllTasks();
        ParentTask GetParentTask(int id);
        Task GetTask(int id);
        bool UpdateParentTask(ParentTask task);
        bool UpdateTask(Task task);
    }
}
