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
    [Route("api/project")]
    [EnableCors("CorsPolicy")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectBl _projectBl;
        private readonly IProjectRepository _repo;
        public ProjectController(IProjectRepository repo)
        {
            _repo = repo;
            _projectBl = new ProjectBl(repo);
        }

        [Route("GetAllProjects")]
        [HttpGet]
        public List<Project> GetAllProjects()
        {
            return _projectBl.GetAllProjects();
        }
        [Route("GetProject")]
        [HttpGet]
        public Project GetProject(int id)
        {
            return _projectBl.GetProject(id);
        }
        [Route("UpdateProject")]
        [HttpPost]
        public bool UpdateProject(Project project)
        {
            return _projectBl.UpdateProject(project);
        }
        [Route("AddProject")]
        [HttpPost]
        public bool AddProject(Project project)
        {
            return _projectBl.AddProject(project);
        }
    }
}
