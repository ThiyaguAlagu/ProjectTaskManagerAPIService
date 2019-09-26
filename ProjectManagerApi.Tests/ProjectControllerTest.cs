using BusinessEntities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProjectManagerApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjectManagerApi.Tests
{
    [TestFixture]
    public class ProjectControllerTest
    {
        private string _contextGuid;
        public string ContextGuid
        {
            get
            {
                return _contextGuid;
            }
            set
            {
                _contextGuid = value;
            }
        }

        [SetUp]
        public void Init()
        {
            ContextGuid = System.Guid.NewGuid().ToString();
        }
        private ProjectManagerContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectManagerContext>();
            optionsBuilder.UseInMemoryDatabase("ProjectManagerTestProjectDB" + ContextGuid).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            return new ProjectManagerContext(optionsBuilder.Options);
        }

        [TearDown]
        public void CleanUp()
        {
            DeleteDB();
        }

        [Test]
        public void AddProjectTest()
        {
            AddUser(MockFirstUser());
            var result = AddProject(MockFirstProject());
            Assert.AreEqual(result, true);
        }

        [Test]
        public void UpdateProjectTest()
        {
            AddUser(MockFirstUser());
            AddProject(MockFirstProject());
            var projectId = GetFirstProject().ProjectId;

            var project = MockFirstProject();
            project.ProjectId = projectId;
            project.ProjectName = "Insurance project";
            var result = UpdateProject(project);
            Assert.AreEqual(result, true);

            var updatedProject = GetFirstProject();
            Assert.AreEqual(updatedProject.ProjectName, "Insurance project");
        }

        [Test]
        public void GetProjectTest()
        {
            AddUser(MockFirstUser());
            AddProject(MockFirstProject());
            var projectId = GetFirstProject().ProjectId;
            var project = GetProject(projectId);
            Assert.AreEqual(projectId, project.ProjectId);
        }

        [Test]
        public void GetAllProjectsTest()
        {
            AddUser(MockFirstUser());
            AddProject(MockFirstProject());
            AddProject(MockSecondProject());

            var result = GetAllProjects();
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result.Count, 2);
        }

        private Project MockFirstProject()
        {
            Project project = new Project();
            project.ProjectId = 0;
            project.ProjectName = "Newdocker";
            project.StartDate = DateTime.Now;
            project.EndDate = DateTime.Now.AddMonths(4);
            project.Priority = 10;
            project.Status = "Active";
            project.ManagerId = 1;
            return project;
        }


        private User MockFirstUser()
        {
            User user = new User();
            user.UserId = 0;
            user.FirstName = "Thiyagu";
            user.LastName = "Alagu";
            user.EmployeeId = 596558;
            user.IsDeleted = false;
            return user;
        }
        private Project MockSecondProject()
        {
            Project project = new Project();
            project.ProjectId = 0;
            project.ProjectName = "Cash Accounting";
            project.StartDate = DateTime.Now;
            project.EndDate = DateTime.Now.AddMonths(3);
            project.Status = "Active";
            project.ManagerId = 1;
            project.Priority = 20;
            return project;
        }

        private bool AddProject(Project project)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IProjectRepository _repo = new ProjectRepository(_dbContext);
                var controller = new ProjectController(_repo);
                return controller.AddProject(project);
            }
        }

        private bool UpdateProject(Project project)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IProjectRepository _repo = new ProjectRepository(_dbContext);
                var controller = new ProjectController(_repo);
                return controller.UpdateProject(project);
            }
        }

        private Project GetFirstProject()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IProjectRepository _repo = new ProjectRepository(_dbContext);
                var controller = new ProjectController(_repo);
                return controller.GetAllProjects().First();
            }
        }

        private Project GetProject(int id)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IProjectRepository _repo = new ProjectRepository(_dbContext);
                var controller = new ProjectController(_repo);
                return controller.GetProject(id);
            }
        }

        private List<Project> GetAllProjects()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IProjectRepository _repo = new ProjectRepository(_dbContext);
                var controller = new ProjectController(_repo);
                return controller.GetAllProjects();
            }
        }

        private void DeleteDB()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                _dbContext.Database.EnsureDeleted();
            }
        }

        private bool AddUser(User user)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IUserRepository _repo = new UserRepository(_dbContext);
                var controller = new UserController(_repo);
                return controller.AddUser(user);
            }
        }
    }
}
