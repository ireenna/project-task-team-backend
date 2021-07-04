using System;
using System.Net.Http;
using System.Threading.Tasks;
using projectStructureApp;
using projectStructureApp.ModelsApp.Create;
using projectStructureApp.ModelsApp.Update;
using projectStructureApp.ModelsDTOapp;
using projectStructureApp.ModelsDTOapp.Create;
using projectStructureApp.ModelsDTOapp.Update;

namespace projectApp
{
    class Program
    {
        private static AppService app = new AppService();
        static async Task Main(string[] args)
        {
            var allInfo = await app.RefreshData();
            do
            {
                try
                {
                    Console.Clear();
                Console.Write("Options:\n" +
                    "1. Get the number of tasks from a specific user's project.\n" +
                    "2. Get a list of tasks assigned to a specific user (name < 45 symbols).\n" +
                    "3. Get tasks that are finished in the current year for a specific user.\n" +
                    "4. Get a list of teams with members over 10 years old, sorted and grouped.\n" +
                    "5. Get a list of users alphabetically with tasks sorted by name length.\n" +
                    "6. Get task info.\n" +
                    "7. Get project info.\n" +
                    "8. CRUD operations.\n" +
                    "Choose an option: ");

                int operation = Convert.ToInt32(Console.ReadLine());

                switch (operation)
                {
                    case 1:
                        Console.Write("Id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        var q1 = await app.GetQuantityOfUserTasks(id);
                        if (q1.Count == 0)
                        {
                            Console.WriteLine("There is nothing.");
                            break;
                        }
                        foreach (var item in q1)
                            Console.WriteLine("Project: " + item.Key + " Tasks count:" + item.Value);
                        break;

                    case 2:
                        Console.Write("Id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        var q2 = await app.GetUserTasks(id);
                        if (q2.Count == 0)
                        {
                            Console.WriteLine("There is nothing.");
                            break;
                        }
                        q2.ForEach(item => Console.WriteLine("Id: " + item.Id + " Name: " + item.Name));
                        break;

                    case 3:
                        Console.Write("Id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        var q3 = await app.GetUserFinishedTasks(id);
                        if (q3.Count == 0)
                        {
                            Console.WriteLine("There is nothing.");
                            break;
                        }
                        q3.ForEach(item => Console.WriteLine($"{item.id} - {item.name}"));
                        break;

                    case 4:
                        var q4 = await app.GetSortedUsersTeams();
                        foreach (var item in q4)
                        {
                            string usersStr = "";
                            item.users.ForEach(x => usersStr += $"Name: {x.FirstName} {x.LastName} B-Day: {x.BirthDay}\n");
                            Console.WriteLine($"{item.id}. {item.name}.\nParticipants:\n{usersStr}");
                        }
                        break;

                    case 5:
                        var q5 = await app.GetSortedUsersWithTasks();

                        q5.ForEach(item =>
                        {
                            item.ForEach(a =>
                            {
                                Console.WriteLine("User: " + a.Performer.LastName + ": * " + a.Name);
                            });
                        });
                        break;

                    case 6:
                        Console.Write("Id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        var q6 = await app.GetUserTaskInfo(id);
                        Console.WriteLine(q6.ToString());
                        break;

                    case 7:
                        var q7 = await app.GetProjectsInfo();
                        q7.ForEach(item => Console.WriteLine(item.ToString()));
                        break;

                    case 8:
                        Console.Write("CRUD operations:\n" +
                        "1. Create.\n" +
                        "2. Read.\n" +
                        "3. Update.\n" +
                        "4. Delete.\n" +
                        "Choose an operation: ");
                        int crudOperation = Convert.ToInt32(Console.ReadLine());
                        if (crudOperation > 5 || crudOperation < 0)
                            throw new Exception();

                        Console.Write("Objects:\n" +
                        "1. Projects.\n" +
                        "2. Users.\n" +
                        "3. Tasks.\n" +
                        "4. Teams.\n" +
                        "Choose objects: ");
                        int objectSelection = Convert.ToInt32(Console.ReadLine());
                        if (objectSelection > 5 || crudOperation < 0)
                            throw new Exception();
                        switch (crudOperation)
                        {
                            case 1: await Create(objectSelection); break;
                            case 2: await Read(objectSelection); break;
                            case 3: await Update(objectSelection); break;
                            case 4: await Delete(objectSelection); break;
                            default: break;

                        }
                        break;

                    default:
                        Console.WriteLine("There is no such operation. Please, try again.");
                        break;
                }
            }
                catch
            {
                Console.WriteLine("Wrong input. Please, try again.");
            }
            Console.ReadLine();
            } while (true);
            async Task Create(int obj)
            {
                bool isCreated = false;
                switch (obj)
                {
                    case 1:
                        Console.Write("AuthorId: ");
                        int authorId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("TeamId: ");
                        int teamId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Description: ");
                        string description = Console.ReadLine();
                        Console.Write("Deadline: ");
                        DateTime deadline = Convert.ToDateTime(Console.ReadLine());
                        isCreated = await app.CreateProject(new ProjectCreateApp() { 
                            AuthorId = authorId,
                            TeamId = teamId,
                            Name = name,
                            Description = description,
                            Deadline = deadline
                        });
                        break;
                    case 2:
                        Console.Write("FirstName:");
                        string fName = Console.ReadLine();
                        Console.Write("LastName:");
                        string lName = Console.ReadLine();
                        Console.Write("Email:");
                        string email = Console.ReadLine();
                        Console.Write("BirthDay:");
                        DateTime bday = Convert.ToDateTime(Console.ReadLine());
                        Console.Write("TeamId:");
                        int team_Id = Convert.ToInt32(Console.ReadLine());
                        isCreated = await app.CreateUser(new UserCreateApp { 
                            BirthDay = bday, 
                            Email = email, 
                            FirstName = fName, 
                            LastName = lName, 
                            TeamId = team_Id
                        });
                        break;
                    case 3:
                        Console.Write("ProjectId: ");
                        int projectId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("PerformerId: ");
                        int performerId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Name: ");
                        string taskName = Console.ReadLine();
                        Console.Write("Description: ");
                        string taskDescription = Console.ReadLine();
                        isCreated = await app.CreateTask(new TasksCreateApp()
                        {
                            Description = taskDescription,
                            Name = taskName,
                            PerformerId = performerId,
                            ProjectId = projectId
                        });
                        break;
                    case 4:
                        Console.Write("Name: ");
                        string teamName = Console.ReadLine();
                        isCreated = await app.CreateTeam(new TeamCreateApp()
                        {
                            Name = teamName
                        });
                        break;
                    default: break;
                }
                if (isCreated) Console.WriteLine("Created.");
            }
            async Task Read(int obj)
            {
                switch (obj)
                {
                    case 1:
                        var projects = await app.GetAllProjects();
                        projects.ForEach(x => Console.WriteLine(x.ToString()));
                        break;
                    case 2:
                        var users = await app.GetAllUsers();
                        users.ForEach(x => Console.WriteLine(x.ToString()));
                        break;
                    case 3:
                        var tasks = await app.GetAllTasks();
                        tasks.ForEach(x => Console.WriteLine(x.ToString()));
                        break;
                    case 4:
                        var teams = await app.GetAllTeams();
                        teams.ForEach(x => Console.WriteLine(x.ToString()));
                        break;
                    default: break;
                }
            }
            async Task Update(int obj)
            {
                switch (obj)
                {
                    case 1: await UpdateProject(obj); break;
                    case 2: await UpdateUser(obj); break;
                    case 3: await UpdateTask(obj); break;
                    case 4: await UpdateTeam(obj); break;
                }
            }
            async Task UpdateProject(int obj)
            {
                Console.Write("Id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                var project = await app.GetProject(id);
                Console.WriteLine(project.ToString());
                Console.Write("Properties:\n" +
                    "1.Name.\n2.TeamId.\n3.Descriprion.\n4.Deadline.\nChoose what you want to change: ");
                int command = Convert.ToInt32(Console.ReadLine());
                var updatedProject = new ProjectUpdateApp()
                {
                    Deadline = project.Deadline,
                    Description = project.Description,
                    Name = project.Name,
                    TeamId = project.TeamId
                };
                switch (command)
                {
                    case 1:
                        Console.Write($"New name (Previous: {updatedProject.Name}): ");
                        updatedProject.Name = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write($"New team (Previous: {updatedProject.TeamId}): ");
                        updatedProject.TeamId = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 3:
                        Console.Write($"New description (Previous: {updatedProject.Description}): ");
                        updatedProject.Description = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write($"New deadline (Previous: {updatedProject.Deadline}): ");
                        updatedProject.Deadline = Convert.ToDateTime(Console.ReadLine());
                        break;
                    default: break;
                }
                Console.Write("Update? (y/n): ");
                string yesNo = Console.ReadLine();
                switch (yesNo)
                {
                    case "y" or "yes":
                        bool isUpdated = await app.UpdateProject(updatedProject, project.Id);
                        if (isUpdated) Console.WriteLine("Updated successfully.");
                        break;
                    case "n" or "no":
                        break;
                    default: break;
                }
            }
            async Task UpdateTask(int obj)
            {
                Console.Write("Id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                var task = await app.GetTask(id);
                Console.WriteLine(task.ToString());
                Console.Write("Properties:\n" +
                    "1.Name.\n2.ProjectId\n" +
                    "3.Descriprion.\n4.PerformerId\n" +
                    "5.State.\n6.FinishedAt.\n" +
                    "Choose what you want to change: ");
                int command = Convert.ToInt32(Console.ReadLine());
                var updatedTask = new TasksUpdateApp()
                {
                    FinishedAt = task.FinishedAt,
                    Description = task.Description,
                    Name = task.Name,
                    PerformerId = task.PerformerId,
                    ProjectId = task.ProjectId,
                    State = task.State
                };
                switch (command)
                {
                    case 1:
                        Console.Write($"New name (Previous: {updatedTask.Name}): ");
                        updatedTask.Name = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write($"New projectId (Previous: {updatedTask.ProjectId}): ");
                        updatedTask.ProjectId = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 3:
                        Console.Write($"New description (Previous: {updatedTask.Description}): ");
                        updatedTask.Description = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write($"New performerId (Previous: {updatedTask.PerformerId}): ");
                        updatedTask.PerformerId = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 5:
                        Console.Write($"New state (Previous: {updatedTask.State} - {Convert.ToInt32(updatedTask.State)}): ");
                        updatedTask.State = (TaskStateDTOapp)Convert.ToInt32(Console.ReadLine());
                        break;
                    case 6:
                        Console.Write($"New finishedAt (Previous: {updatedTask.FinishedAt}): ");
                        updatedTask.FinishedAt = Convert.ToDateTime(Console.ReadLine());
                        break;
                    default: break;
                }
                Console.Write("Update? (y/n): ");
                string yesNo = Console.ReadLine();
                switch (yesNo)
                {
                    case "y" or "yes":
                        bool isUpdated = await app.UpdateTask(updatedTask, task.Id);
                        if (isUpdated) Console.WriteLine("Updated successfully.");
                        break;
                    case "n" or "no":
                        break;
                    default: break;
                }
            }
            async Task UpdateTeam(int obj)
            {
                Console.Write("Id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                var team = await app.GetTeam(id);
                Console.WriteLine(team.ToString());
                Console.Write("Properties:\n" +
                    "1.Name.\nChoose what you want to change: ");
                int command = Convert.ToInt32(Console.ReadLine());
                var updatedTeam = new TeamUpdateApp()
                {
                    Name = team.Name
                };
                switch (command)
                {
                    case 1:
                        Console.Write($"New name (Previous: {updatedTeam.Name}): ");
                        updatedTeam.Name = Console.ReadLine();
                        break;
                    default: break;
                }
                Console.Write("Update? (y/n): ");
                string yesNo = Console.ReadLine();
                switch (yesNo)
                {
                    case "y" or "yes":
                        bool isUpdated = await app.UpdateTeam(updatedTeam, team.Id);
                        if (isUpdated) Console.WriteLine("Updated successfully.");
                        break;
                    case "n" or "no":
                        break;
                    default: break;
                }
            }
            async Task UpdateUser(int obj)
            {
                Console.Write("Id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                var user = await app.GetUser(id);
                Console.WriteLine(user.ToString());
                Console.Write("Properties:\n" +
                    "1. FirstName.\n2. LastName.\n3. BirthDay.\n4. Email.\n5. TeamId.\nChoose what you want to change: ");
                int command = Convert.ToInt32(Console.ReadLine());
                var updatedUser = new UserUpdateApp()
                {
                    BirthDay = user.BirthDay,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TeamId = user.TeamId
                };
                switch (command)
                {
                    case 1:
                        Console.Write($"New FirstName (Previous: {updatedUser.FirstName}): ");
                        updatedUser.FirstName = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write($"New LastName (Previous: {updatedUser.LastName}): ");
                        updatedUser.LastName = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write($"New B-Day (Previous: {updatedUser.BirthDay}): ");
                        updatedUser.BirthDay = Convert.ToDateTime(Console.ReadLine());
                        break;
                    case 4:
                        Console.Write($"New Email (Previous: {updatedUser.Email}): ");
                        updatedUser.Email = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write($"New TeamId (Previous: {updatedUser.TeamId}): ");
                        updatedUser.TeamId = Convert.ToInt32(Console.ReadLine());
                        break;
                    default: break;
                }
                Console.Write("Update? (y/n): ");
                string yesNo = Console.ReadLine();
                switch (yesNo)
                {
                    case "y" or "yes":
                        bool isUpdated = await app.UpdateUser(updatedUser, user.Id);
                        if (isUpdated) Console.WriteLine("Updated successfully.");
                        break;
                    case "n" or "no":
                        break;
                    default: break;
                }
            }
            async Task Delete(int obj)
            {
                Console.Write("Id: ");
                int id = Convert.ToInt32(Console.ReadLine());
                bool isDeleted = false;
                switch (obj)
                {
                    case 1:
                        isDeleted = await app.DeleteProject(id);
                        break;
                    case 2:
                        isDeleted = await app.DeleteUser(id);
                        break;
                    case 3:
                        isDeleted = await app.DeleteTask(id);
                        break;
                    case 4:
                        isDeleted = await app.DeleteTeam(id);
                        break;
                    default: break;
                }
                if (isDeleted) Console.WriteLine("Deleted successfully.");
            }
        }
    }
}
