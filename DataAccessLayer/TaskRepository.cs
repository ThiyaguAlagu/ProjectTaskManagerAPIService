using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq;

namespace DataAccessLayer
{
    public class TaskRepository : ITaskRepository
    {
        ProjectManagerContext _context;
        public TaskRepository(ProjectManagerContext context)
        {
            _context = context;
        }
        public virtual List<Task> GetAllTasks()
        {
            var query = from task in _context.Set<Task>()
                        select task;
            return query.Include(t => t.Parent).Include(t=>t.Project).Include(t => t.User).AsNoTracking().ToList();
        }
        public virtual Task GetTask(int id)
        {
            var query = from task in _context.Set<Task>()
                        where task.TaskId==id
                        select task;
            return query.Include(t => t.Parent).Include(t => t.Project).Include(t => t.User).AsNoTracking().First();
        }
        public virtual ParentTask GetParentTask(int id)
        {
            var query = from task in _context.Set<ParentTask>()
                        where task.ParentId == id
                        select task;
            return query.AsNoTracking().First();
        }
        public virtual List<ParentTask> GetAllParentTasks()
        {
            var query = from parent in _context.Set<ParentTask>()
                        select parent;
            return query.AsNoTracking().ToList();
        }
        public virtual bool UpdateTask(Task task)
        {
            _context.Set<Task>().Update(task);
            _context.SaveChanges();
            return true;
        }
        public virtual bool UpdateParentTask(ParentTask task)
        {
            _context.Set<ParentTask>().Update(task);
            _context.SaveChanges();
            return true;
        }
        public virtual bool AddTask(Task task)
        {
            _context.Set<Task>().Add(task);
            _context.SaveChanges();
            return true;
        }
        public virtual bool AddParentTask(ParentTask task)
        {
            _context.Set<ParentTask>().Add(task);
            _context.SaveChanges();
            return true;
        }
        public virtual bool DeleteTask(Task task)
        {
            _context.Set<Task>().Remove(_context.Set<Task>().Where(t => t.TaskId == task.TaskId).FirstOrDefault());
            _context.SaveChanges();
            _context.SaveChanges();
            return true;
        }
        public virtual bool DeleteParentTask(ParentTask task)
        {
            _context.Set<ParentTask>().Remove(_context.Set<ParentTask>().Where(t => t.ParentId == task.ParentId).FirstOrDefault());
            _context.SaveChanges();
            return true;
        }
        public virtual bool EndTask(int taskId)
        {
            var task = _context.Set<Task>().Where(t => t.TaskId == taskId).FirstOrDefault();
            task.Status = false;
            _context.Set<Task>().Update(task);
            _context.SaveChanges();
            return true;
        }
    }
}

