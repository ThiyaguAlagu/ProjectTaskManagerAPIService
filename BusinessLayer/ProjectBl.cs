using BusinessEntities;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;


namespace BusinessLayer
{
    public class ProjectBl
    {
        private readonly IProjectRepository _repo;
        public ProjectBl(IProjectRepository repo)
        {
            _repo = repo;
        }
        public virtual List<Project> GetAllProjects()
        {
           var result= _repo.GetAllProjects();
            return result;
        }
        public virtual Project GetProject(int id)
        {
            return _repo.GetProject(id);
        }
        public virtual bool UpdateProject(Project project)
        {
            return _repo.UpdateProject(project);
        }
        public virtual bool AddProject(Project project)
        {
            return _repo.AddProject(project);
        }
    }
}
