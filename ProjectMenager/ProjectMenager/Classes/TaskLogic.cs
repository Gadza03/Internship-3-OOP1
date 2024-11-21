using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMenager.Classes
{
    public static class TaskLogic
    {
        public static Task ChooseTask(Dictionary<Project, List<Task>> projectTasks)
        {
            Task foundedTask = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Popis svih zadataka...\n");
                foreach (var project in projectTasks)
                {
                    foreach (var task in project.Value)
                    {
                        Console.WriteLine($"- {task.Name}");
                    }
                }
                Console.Write("\nOdaberite zadatak: ");
                var selectedTask = Console.ReadLine().Trim().ToLower();
                foreach (var project in projectTasks)
                {
                    foreach (var task in project.Value)
                    {
                        if (task.Name.ToLower() == selectedTask)
                            foundedTask = task;
                    }
                }
                if (foundedTask == null)
                {
                    Console.WriteLine("Pogrešan unos zadatka. Unesite neki sa liste.");
                    Console.ReadKey();
                    continue;
                }
                break;
            } 
            return foundedTask;
        }
        private static void DisplayTaskDetails(Task task)
        {
            Console.Clear();
            Console.WriteLine($"Naziv: {task.Name}\nOpis: {task.Description}\nRok: {task.DueDate.ToString("dd/MM/yyyy")}\n" +
                    $"Status: {task.Status}\nOčekivano trajanje: {task.ExpectedDuration} min\nPrioritet: {task.Priority}\nPridruženi projekt (ID): {task.AssociatedProjectId.ToString()}");
        }
        private static void CheckIfAllTasksCompleted(Dictionary<Project, List<Task>> projectTasks, Task task)
        {
            var projects = projectTasks.Keys;
            Project foundedProject = null;
            foreach (var project in projects)
            {
                if (project.GetId() == task.AssociatedProjectId)
                    foundedProject = project;
            }
            if (foundedProject == null)
            {
                Console.WriteLine("Nismo pronasli odgovarajuci projekt za zadatak.");
                return;
            }
            var areAllTasksCompleted = projectTasks[foundedProject].All(t => t.Status == StatusTask.Completed);
            if (areAllTasksCompleted)
            {
                Console.WriteLine($"Svi zadaci u projektu {foundedProject.Name} su završeni, postavljamo i njega na completed.");
                foundedProject.Status = ProjectStatus.Completed;
            }                               
        }
        private static void EditTaskStatus(Dictionary<Project, List<Task>> projectTasks,Task task)
        {
            Console.Clear();
            StatusTask taskStatusOld = task.Status;
            if (task.Status == StatusTask.Completed)
            {
                Console.WriteLine("Zadatak je završen i ne možete mu mijenjati status.");
                return;
            }
            var status = ProjectLogic.GetValidTaskStatus();
            task.Status = status;
            Console.WriteLine($"Uspješno ste promjenili status zadatka sa {taskStatusOld} na {task.Status}");
            CheckIfAllTasksCompleted(projectTasks, task);

        }
        public static void ManageSpecificTask(Dictionary<Project, List<Task>> projectTasks, Task task)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Upravljanje zadatkom: {task.Name}\n");
                Console.WriteLine("1. Prikaz detalja odabranog zadatka");
                Console.WriteLine("2. Uređivanje statusa zadatka");
                Console.WriteLine("0. Izlaz");
                Console.Write("\nOdaberite opciju: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayTaskDetails(task);
                        break;
                    case "2":
                        EditTaskStatus(projectTasks, task);
                        break;
                    case "0":
                        Console.WriteLine("Vraćate se na glavni izbornik...");
                        return;
                    default:
                        Console.WriteLine("Neispravan unos. Pokušajte ponovno.");
                        break;
                }

                Console.ReadKey();
            }
        }

        //bonus taks
        private static List<Task> GetListOfAllTasks(Dictionary<Project, List<Task>> projectTasks)
        {
            var tasksList = new List<Task>();
            foreach (var tasks in projectTasks.Values)
                tasksList.AddRange(tasks);
            return tasksList;
        }      
        private static void SortTasksByPriority(Dictionary<Project, List<Task>> projectTasks)
        {
            Console.Clear();            
            var tasksList = GetListOfAllTasks(projectTasks);
            tasksList = tasksList.OrderBy(task => task.Priority).ToList();           
            foreach (var task in tasksList)            
                Console.WriteLine($"Naziv: {task.Name}\n\t- Opis: {task.Description} - Prioritet: {task.Priority}\n");            
        }
        private static void SortTasksByDuration(Dictionary<Project, List<Task>> projectTasks)
        {
            Console.Clear();
            var tasksList = GetListOfAllTasks(projectTasks);
            tasksList = tasksList.OrderBy(task => task.ExpectedDuration).ToList();
            foreach (var task in tasksList)
                Console.WriteLine($"Naziv: {task.Name}\n\t- Opis: {task.Description} - Prioritet: {task.ExpectedDuration} min\n");
        }
        public static void BonusMenu(Dictionary<Project, List<Task>> projectTasks)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bonus zadaci: \n");
                Console.WriteLine("1. Prikaz zadataka sortiranih od najkračeg do najduljeg\n2. Prikaz zadataka po prioritetu\n");
                Console.Write("Odaberite opciju: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        SortTasksByDuration(projectTasks);
                        break;
                    case "2":
                        SortTasksByPriority(projectTasks);
                        break;
                    case "0":
                        Console.WriteLine("Vraćate se na glavni izbornik...");
                        return;
                    default:
                        break;
                }
                Console.ReadKey();
            }
        }

    }
}
