using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace ProjectManagerApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/task")]
    [EnableCors("CorsPolicy")]
    public class TaskController : ControllerBase
    {
        private readonly TaskBl _taskBl;
        private readonly ITaskRepository _repo;
        public TaskController(ITaskRepository repo)
        {
            _repo = repo;
            _taskBl = new TaskBl(repo);
        }

        [Route("GetAllTasks")]
        [HttpGet]
        public List<Task> GetAllTasks()
        {
            return _taskBl.GetAllTasks();
        }

        [Route("GetAllParentTasks")]
        [HttpGet]
        public List<ParentTask> GetAllParentTasks()
        {
            return _taskBl.GetAllParentTasks();
        }

        [Route("GetAllTypeOfTasks")]
        [HttpGet]
        public List<Task> GetAllTypeOfTasks()
        {
            return _taskBl.GetAllTypeOfTasks();
        }

        [Route("UpdateTask")]
        [HttpPost]
        public bool UpdateTask([FromBody]Task task)
        {
            return _taskBl.UpdateTask(task);
        }

        [Route("AddTask")]
        [HttpPost]
        public bool AddTask([FromBody]Task task)
        {
            return _taskBl.AddTask(task);
        }

        [Route("DeleteTask")]
        [HttpPost]
        public bool DeleteTask([FromBody]Task task)
        {
            return _taskBl.DeleteTask(task);
        }

        [Route("EndTask")]
        [HttpPost]
        public bool EndTask([FromBody]Task task)
        {
            return _taskBl.EndTask(task.TaskId);
        }
        [Route("GetTask")]
        [HttpGet]
        public Task GetTask(int id, bool isParent)
        {
            return _taskBl.GetTask(id, isParent);
        }
    }
}
