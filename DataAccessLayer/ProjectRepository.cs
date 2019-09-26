using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq;
namespace DataAccessLayer
{
    public class ProjectRepository : IProjectRepository
    {
        ProjectManagerContext _context;
        public ProjectRepository(ProjectManagerContext context)
        {
            _context = context;
        }
        public virtual List<Project> GetAllProjects()
        {
            var query = from project in _context.Set<Project>()
                        select project;
            return query.Include(t => t.ProjectManager).AsNoTracking().ToList();
        }
        public virtual Project GetProject(int id)
        {
            var query = from project in _context.Set<Project>()
                        where project.ProjectId == id
                        select project;
            return query.Include(t => t.ProjectManager).AsNoTracking().First();
        }
        public virtual bool UpdateProject(Project project)
        {
            _context.Set<Project>().Update(project);
            _context.SaveChanges();
            return true;
        }
        public virtual bool AddProject(Project project)
        {
            _context.Set<Project>().Add(project);
            _context.SaveChanges();
            return true;
        }
    }
}


