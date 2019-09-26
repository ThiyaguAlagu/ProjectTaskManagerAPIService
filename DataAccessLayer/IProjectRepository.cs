using System.Collections.Generic;
using BusinessEntities;

namespace DataAccessLayer
{
    public interface IProjectRepository
    {
        bool AddProject(Project project);
        List<Project> GetAllProjects();
        Project GetProject(int id);
        bool UpdateProject(Project project);
    }
}
