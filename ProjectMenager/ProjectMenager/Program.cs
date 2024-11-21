using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMenager.Classes;
using Task = ProjectMenager.Classes.Task;
namespace ProjectMenager
{
    class Program
    {        
        static void Main(string[] args)
        {
            Dictionary<Project, List<Task>> projectTasks = new Dictionary<Project, List<Task>>();
            CreateProjectsAndTasks(projectTasks);
            MainMenu(projectTasks);           
            
        }        
        static void MainMenu(Dictionary<Project, List<Task>> projectTasks)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Glavni izbornik:");
                Console.WriteLine("1. Ispis svih projekata s pripadajućim zadacima");
                Console.WriteLine("2. Dodavanje novog projekta");
                Console.WriteLine("3. Brisanje projekta");
                Console.WriteLine("4. Prikaz zadataka koji su due u sljedećih 7 dana");
                Console.WriteLine("5. Prikaz projekata filtriranih po statusu");
                Console.WriteLine("6. Upravljanje određenim projektom");
                Console.WriteLine("7. Upravljanje određenim zadatkom");
                Console.WriteLine("0. Izlaz");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProjectLogic.PrintProjectDetails(projectTasks);
                        break;
                    case "2":
                        ProjectLogic.AddNewProject(projectTasks);
                        break;
                    case "3":
                        ProjectLogic.DeleteProject(projectTasks);
                        break;
                    case "4":
                        ProjectLogic.ShowTasksDueInNext7Days(projectTasks);
                        break;
                    case "5":
                        ProjectLogic.FilterProjectsByStatus(projectTasks);
                        break;
                    case "6":
                        var selectedProject = ProjectLogic.ChooseProject(projectTasks);
                        ProjectLogic.ManageProject(projectTasks, selectedProject);
                        break;
                    case "7":
                        var selectedTask = TaskLogic.ChooseTask(projectTasks);
                        TaskLogic.ManageSpecificTask(projectTasks, selectedTask);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neispravan unos. Pokusajte ponovno!");
                        break;
                }
                Console.ReadKey();
            }
        }
        static void CreateProjectsAndTasks(Dictionary<Project, List<Task>> projectTasks)
        {
            var project1 = new Project(
               "Website Redesign",
               "Revamp the company's website to improve user experience and update branding.",
               new DateTime(2024, 11, 22),
               new DateTime(2025, 1, 15),
               ProjectStatus.Active
           );

            var project2 = new Project(
                "Mobile App Development",
                "Develop a mobile application for managing customer loyalty programs.",
                new DateTime(2024, 12, 1),
                new DateTime(2025, 4, 30),
                ProjectStatus.Pending
            );

            var project3 = new Project(
                "Cybersecurity Enhancement",
                "Upgrade the company's cybersecurity infrastructure to ensure compliance and safety.",
                new DateTime(2024, 9, 8),
                new DateTime(2025, 11, 12),
                ProjectStatus.Completed
            );

            var project4 = new Project(
                "AI Integration",
                "Implement AI tools to automate customer support and data analysis tasks.",
                new DateTime(2024, 11, 19),
                new DateTime(2025, 6, 1),
                ProjectStatus.Active
            );

            var project5 = new Project(
                "Employee Training Program",
                "Organize and execute a comprehensive training program for employees on new technologies.",
                new DateTime(2024, 12, 5),
                new DateTime(2025, 3, 10),
                ProjectStatus.Pending
            );

            var task1 = new Task(
                "Design Mockups",
                "Create design mockups for the new website.",
                project1.StartDate.AddDays(5),
                StatusTask.Active,
                180,
                project1.GetId()
            );

            var task2 = new Task(
                "Develop Backend",
                "Develop the backend for the website.",
                project1.StartDate.AddDays(20),
                StatusTask.Completed,
                240,
                project1.GetId()
            );

            var task3 = new Task(
                "App UI",
                "Design the UI for the mobile app.",
                project2.StartDate.AddDays(10),
                StatusTask.Postponed,
                120,
                project2.GetId()
            );

            var task4 = new Task(
                "API Integration",
                "Integrate APIs for mobile app functionalities.",
                project2.StartDate.AddDays(40),
                StatusTask.Active,
                180,
                project2.GetId()
            );

            var task5 = new Task(
                "Security Audit",
                "Conduct a security audit of the system.",
                project3.StartDate.AddDays(30),
                StatusTask.Active,
                240,
                project3.GetId()
            );

            var task6 = new Task(
                "Firewall Setup",
                "Setup and configure firewalls for enhanced security.",
                project3.StartDate.AddDays(50),
                StatusTask.Completed,
                220,
                project3.GetId()
            );

            var task7 = new Task(
                "AI Research",
                "Conduct research on AI tools and technologies to integrate.",
                project4.StartDate.AddDays(10),
                StatusTask.Active,
                200,
                project4.GetId()
            );

            var task8 = new Task(
                "AI Integration Setup",
                "Set up the environment for integrating AI tools into the system.",
                project4.StartDate.AddDays(40),
                StatusTask.Active,
                240,
                project4.GetId()
            );

            var task9 = new Task(
                "Create Training Materials",
                "Create training materials for employees on new technologies.",
                project5.StartDate.AddDays(5),
                StatusTask.Postponed,
                180,
                project5.GetId()
            );

            var task10 = new Task(
                "Training Sessions",
                "Conduct training sessions for employees on new technologies.",
                project5.StartDate.AddDays(30),
                StatusTask.Active,
                300,
                project5.GetId()
            );

            projectTasks.Add(project1, new List<Task> { task1, task2 });
            projectTasks.Add(project2, new List<Task> { task3, task4 });
            projectTasks.Add(project3, new List<Task> { task5, task6 });
            projectTasks.Add(project4, new List<Task> { task7, task8 });
            projectTasks.Add(project5, new List<Task> { task9, task10 });

        }
    }
}
