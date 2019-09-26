using System.Collections.Generic;
using NUnit.Framework;
using BusinessEntities;
using ProjectManagerApi.Controllers;
using Moq;
using DataAccessLayer;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using System;


namespace ProjectManagerApi.Tests
{
    [TestFixture]
    public class TaskControllerTest
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
            optionsBuilder.UseInMemoryDatabase("ProjectManagerTestTaskDB" + ContextGuid).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            return new ProjectManagerContext(optionsBuilder.Options);
        }

        [TearDown]
        public void CleanUp()
        {
            DeleteDB();
        }

        [Test]
        public void AddTaskTest()
        {
            AddDependencies();
            var result = AddTask(MockFirstTask());
            Assert.AreEqual(result, true);
        }

        [Test]
        public void AddParentTaskTest()
        {
            var result = AddTask(MockFirstParentTask());
            Assert.AreEqual(result, true);
        }

        [Test]
        public void UpdateTaskTest()
        {
            AddDependencies();
            AddTask(MockFirstTask());
            var taskId = GetFirstTask().TaskId;

            var task = MockFirstTask();
            task.TaskId = taskId;
            task.TaskName = "Task1 Modified";
            var result = UpdateTask(task);
            Assert.AreEqual(result, true);

            var updatedTask = GetFirstTask();
            Assert.AreEqual(updatedTask.TaskName, "Task1 Modified");
        }

        [Test]
        public void UpdateParentTaskTest()
        {
            AddTask(MockFirstParentTask());
            var taskId = GetFirstParentTask().ParentId;

            var task = MockFirstParentTask();
            task.TaskId = taskId;
            task.TaskName = "Parent Task1 Modified";
            var result = UpdateTask(task);
            Assert.AreEqual(result, true);

            var updatedTask = GetFirstParentTask();
            Assert.AreEqual(updatedTask.ParentTaskName, "Parent Task1 Modified");
        }

        [Test]
        public void EndTaskTest()
        {
            AddDependencies();
            AddTask(MockFirstTask());
            var task = GetFirstTask();
            EndTask(task);
            var endedTask = GetFirstTask();
            Assert.AreEqual(endedTask.Status, false);
        }


        [Test]
        public void DeleteTaskTest()
        {
            AddDependencies();
            AddTask(MockFirstTask());
            var task = GetFirstTask();
            DeleteTask(task);
            var tasks = GetAllTasks();
            Assert.AreEqual(tasks.Any(), false);
        }

        [Test]
        public void GetTaskTest()
        {
            AddDependencies();
            AddTask(MockFirstTask());
            var tasktId = GetFirstTask().TaskId;
            var task = GetTask(tasktId);
            Assert.AreEqual(tasktId, task.TaskId);
        }

        [Test]
        public void GetAllTestsTest()
        {
            AddDependencies();
            AddTask(MockFirstTask());
            AddTask(MockSecondTask());

            var result = GetAllTasks();
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result.Count, 2);
        }

        [Test]
        public void GetAllTypesOfTestsTest()
        {
            AddDependencies();
            AddTask(MockFirstTask());
            AddTask(MockSecondTask());

            var result = GetAllTypesOfTasks();
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result.Count, 3);
        }

        private Project MockFirstProject()
        {
            Project project = new Project();
            project.ProjectId = 0;
            project.ProjectName = "Library Management";
            project.StartDate = DateTime.Now;
            project.EndDate = DateTime.Now.AddMonths(4);
            project.Status = "Active";
            project.ManagerId = 1;
            project.Priority = 20;
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


        private Task MockFirstTask()
        {
            Task task = new Task();
            task.TaskId = 0;
            task.TaskName = "Task1";
            task.ParentId = 1;
            task.StartDate = DateTime.Now;
            task.EndDate = DateTime.Now.AddDays(7);
            task.Priority = 10;
            task.Status = true;
            task.ProjectId = 1;
            task.UserId = 1;
            return task;
        }

        private Task MockSecondTask()
        {
            Task task = new Task();
            task.TaskId = 0;
            task.TaskName = "Task2";
            task.ParentId = 1;
            task.StartDate = DateTime.Now;
            task.EndDate = DateTime.Now.AddDays(5);
            task.Priority = 20;
            task.Status = true;
            task.ProjectId = 1;
            task.UserId = 1;
            return task;
        }

        private Task MockFirstParentTask()
        {
            Task task = new Task();
            task.TaskId = 0;
            task.TaskName = "Parent Task1";
            task.ParentId = 0;
            return task;
        }

        private bool AddTask(Task task)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                var result = controller.AddTask(task);
                return result;
            }
        }

        private bool UpdateTask(Task task)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.UpdateTask(task);
            }
        }

        private bool EndTask(Task task)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.EndTask(task);
            }
        }

        private bool DeleteTask(Task task)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.DeleteTask(task);
            }
        }

        private Task GetFirstTask()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.GetAllTasks().First();
            }
        }

        private ParentTask GetFirstParentTask()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.GetAllParentTasks().First();
            }
        }

        private Task GetTask(int id)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.GetTask(id, false);
            }
        }

        private List<Task> GetAllTasks()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.GetAllTasks();
            }
        }

        private List<Task> GetAllTypesOfTasks()
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                ITaskRepository _repo = new TaskRepository(_dbContext);
                var controller = new TaskController(_repo);
                return controller.GetAllTypeOfTasks();
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

        private bool AddProject(Project project)
        {
            using (ProjectManagerContext _dbContext = CreateContext())
            {
                IProjectRepository _repo = new ProjectRepository(_dbContext);
                var controller = new ProjectController(_repo);
                return controller.AddProject(project);
            }
        }

        private void AddDependencies()
        {
            AddUser(MockFirstUser());
            AddProject(MockFirstProject());
            AddTask(MockFirstParentTask());
        }
    }
}
