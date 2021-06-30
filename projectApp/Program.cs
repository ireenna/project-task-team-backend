using System;
using System.Net.Http;
using System.Threading.Tasks;
using projectStructureApp;

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
                        "0. Get all information.\n" +
                        "1. Get the number of tasks from a specific user's project.\n" +
                        "2. Get a list of tasks assigned to a specific user (name < 45 symbols).\n" +
                        "3. Get tasks that are finished in the current year for a specific user.\n" +
                        "4. Get a list of teams with members over 10 years old, sorted and grouped.\n" +
                        "5. Get a list of users alphabetically with tasks sorted by name length.\n" +
                        "6. Get task info.\n" +
                        "7. Get project info.\n" +
                        "Choose an option: ");

                    int operation = Convert.ToInt32(Console.ReadLine());

                    switch (operation)
                    {
                        case 0:
                            var allinfo = await app.GetAllProjects();
                            allinfo.ForEach(x => Console.Write(x.ToString()));
                            break;

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
                            //Console.Write("Id: ");
                            //id = Convert.ToInt32(Console.ReadLine());
                            //var q2 = LinqQueries.GetUserTasks(id);
                            //if (q2.Count == 0)
                            //{
                            //    Console.WriteLine("There is nothing.");
                            //    break;
                            //}
                            //q2.ForEach(item => Console.WriteLine("Id: " + item.Id + " Name: " + item.Name));
                            break;

                        case 3:
                            //Console.Write("Id: ");
                            //id = Convert.ToInt32(Console.ReadLine());
                            //var q3 = LinqQueries.GetUserFinishedTasks(id);
                            //if (q3.Count == 0)
                            //{
                            //    Console.WriteLine("There is nothing.");
                            //    break;
                            //}
                            //q3.ForEach(item => Console.WriteLine($"{item.id} - {item.name}"));
                            break;

                        case 4:
                            //var q4 = LinqQueries.GetSortedUsersTeams();
                            //foreach (var item in q4)
                            //{
                            //    string usersStr = "";
                            //    item.users.ForEach(x => usersStr += $"Name: {x.FirstName} {x.LastName} B-Day: {x.BirthDay}\n");
                            //    Console.WriteLine($"{item.id}. {item.name}.\nParticipants:\n{usersStr}");
                            //}
                            break;

                        case 5:
                            //var q5 = LinqQueries.GetSortedUsersWithTasks();
                            //q5.ForEach(item =>
                            //{
                            //    Console.WriteLine("\nUSER: " + item.Key.Id + ". " + item.Key.FirstName);
                            //    foreach (var task in item)
                            //    {
                            //        Console.WriteLine(" * " + task.Name);
                            //    }
                            //});
                            break;

                        case 6:
                            //Console.Write("Id: ");
                            //id = Convert.ToInt32(Console.ReadLine());
                            //var q6 = LinqQueries.GetUserTasksInfo(id);
                            //Console.WriteLine(q6.ToString());
                            break;

                        case 7:
                            //var q7 = LinqQueries.GetProjectsInfo();
                            //q7.ForEach(item => Console.WriteLine(item.ToString()));
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
        }
    }
}
