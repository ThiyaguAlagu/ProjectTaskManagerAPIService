using BusinessEntities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;


namespace BusinessLayer
{
    public class TaskBl
    {
        private readonly ITaskRepository _repo;

        public TaskBl(ITaskRepository repo)
        {
            _repo = repo;
        }
        public List<Task> GetAllTasks()
        {
            return _repo.GetAllTasks();
        }
        public List<ParentTask> GetAllParentTasks()
        {
            return _repo.GetAllParentTasks();
        }
        public List<Task> GetAllTypeOfTasks()
        {
            var tasks = _repo.GetAllTasks();
            var parentTasks = _repo.GetAllParentTasks();
            foreach(var task in parentTasks)
            {
                tasks.Add(new Task { TaskId=task.ParentId, TaskName=task.ParentTaskName, ParentId = 0 });
            }
            return tasks;
        }
        public bool UpdateTask(Task task)
        {
            if (task.ParentId == 0)
            {
                return _repo.UpdateParentTask(new ParentTask { ParentId = task.TaskId, ParentTaskName = task.TaskName });
            }
            else
            {
                return _repo.UpdateTask(task);
            }
        }
        public bool AddTask(Task task)
        {
            if (task.ParentId == 0)
            {
                return _repo.AddParentTask(new ParentTask { ParentId = task.TaskId, ParentTaskName = task.TaskName });
            }
            else
            {
                return _repo.AddTask(task);
            }
        }
        public bool DeleteTask(Task task)
        {
            if (task.ParentId == 0)
            {
                return _repo.DeleteParentTask(new ParentTask { ParentId = task.TaskId, ParentTaskName = task.TaskName });
            }
            else
            {
                return _repo.DeleteTask(task);
            }
        }
        public bool EndTask(int taskId)
        {
           return _repo.EndTask(taskId);
        }
        public Task GetTask(int id, bool isParent)
        {
            if(isParent)
            {
                var task= _repo.GetParentTask(id);
                return new Task { TaskId=task.ParentId, TaskName=task.ParentTaskName, ParentId=0 };
            }
            else
            {
                return _repo.GetTask(id);
            }
        }
    }
}
