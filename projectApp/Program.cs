using System;
using System.Net.Http;
using System.Threading.Tasks;
using projectStructureApp;
using projectStructureApp.ModelsDTOapp.Create;

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
                //try
                //{
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
                            Console.WriteLine("Project: " + item.Key.Id + " Tasks count:" + item.Value);
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

                        Console.Write("CRUD operations:\n" +
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
                            //case 3: await Update(objectSelection); break;
                            //default: await Delete(objectSelection); break;

                        }
                        break;

                    default:
                        Console.WriteLine("There is no such operation. Please, try again.");
                        break;
                }
                //}
                //catch
                //{
                //    Console.WriteLine("Wrong input. Please, try again.");
                //}
                Console.ReadLine();
            } while (true);
            async Task Create(int obj)
            {
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
                        await app.CreateProject(new ProjectCreateApp() { 
                            AuthorId = authorId,
                            TeamId = teamId,
                            Name = name,
                            Description = description,
                            Deadline = deadline
                        });
                        break;
                }
            }
            async Task Read(int obj)
            {
                switch (obj)
                {
                    case 1:
                        var project = await app.GetAllProjects();
                        project.ForEach(x => Console.Write(x.ToString()));
                        break;
                }
            }
            //async Task Update(int obj)
            //{
            //}
            //async Task Delete(int obj)
            //{
            //    Console.WriteLine("Id: ");
            //    int id = Convert.ToInt32(Console.ReadLine());
            //    switch (obj)
            //    {
            //        case 1:
            //            break;
            //    }
            //}
        }
    }
}
