using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMenager.Classes
{
    public static class ProjectLogic
    {
        public static void PrintProjectDetails(Dictionary<Project, List<Task>> projectTasks)
        {
            Console.Clear();
            foreach (var project in projectTasks)
            {
                Console.WriteLine($"\nIme projekta: {project.Key.Name}");
                foreach (var task in project.Value)
                {
                    Console.WriteLine($"   Zadatak: {task.Name}, Status: {task.Status}, Rok: {task.DueDate.ToString("dd.MM.yyyy")}");
                }
                
            }
            
        }
        public static void ShowTasksDueInNext7Days(Dictionary<Project, List<Task>> projectTasks)
        {
            Console.Clear();
            Console.WriteLine("Ispis svih zadataka kojima je rok u sljedecih 7 dana...\n");
            DateTime currentDate = DateTime.Now;
            foreach (var project in projectTasks)
            {
                foreach (var task in project.Value)
                {
                    TimeSpan difference = task.DueDate - currentDate;
                    Console.WriteLine(difference);
                    if (difference.Days <= 7 && difference.Days >= 0)
                        Console.WriteLine($"Zadatak {task.Name} ima rok u sljedecih {difference} dana.");
                }
            }
        }
        public static void AddNewProject(Dictionary<Project, List<Task>> projectTasks)
        {
            var projectName = GetValidName(projectTasks);
            var projectDescription = GetDescription();
            var projectStartDate = GetValidDate("Unesite datum početka projekta: ");
            var projectEndDate = GetValidDate("Unesite datum kraja projekta: ");
            var projectStatus = GetValidStatus();
            Console.Clear();
            var newProject = new Project(projectName, projectDescription, projectStartDate, projectEndDate, projectStatus);
            projectTasks.Add(newProject, new List<Task>());
            Console.WriteLine($"Projekt {projectName} je uspjesno dodan.");

        }
        private static string GetValidName(Dictionary<Project, List<Task>> projectTasks)
        {
            string name;
            var isNameExists = true;
            do
            {
                Console.Clear();
                Console.Write("Unesite ime projekta: ");
                name = Console.ReadLine().Trim();
                isNameExists = projectTasks.Keys.Any(p => p.Name.ToLower() == name.ToLower());
                if (string.IsNullOrEmpty(name) || isNameExists)
                {
                    Console.WriteLine("Ime projekta ne može biti prazno. Također ime projekta ne smije biti isto kao kod postojećih projekta.");
                    Console.ReadKey();
                    continue;
                }
                isNameExists = false;
            } while (isNameExists);  
            return name;
        }
        private static string GetDescription()
        {
            Console.Clear();
            Console.Write("Unesite opis projekta: ");
            return Console.ReadLine();
        }
        private static DateTime GetValidDate(string prompt)
        {            
            DateTime date;
            while (true)
            {
                Console.Clear();
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out date))                
                    break;
                
                Console.WriteLine("Neispravan datum! Pokušajte ponovo.");
                Console.ReadKey();
            }
            return date;
        }
        private static ProjectStatus GetValidStatus()
        {
            ProjectStatus status;
            while (true)
            {
                Console.Clear();
                Console.Write("Unesite status projekta:\n0 - Aktivno\n1 - Na čekanju\n2 - Završen\n ");
                if (Enum.TryParse(Console.ReadLine(), out status) && Enum.IsDefined(typeof(ProjectStatus), status))               
                    break;
                
                Console.WriteLine("Neispravan status! Pokušajte ponovo.");
                Console.ReadKey();
            }
            return status;
        }

    }
}
