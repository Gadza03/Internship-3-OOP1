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
            do
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
            } while (true);
            return foundedTask;
        }
        private static void DisplayTaskDetails(Task task)
        {
            Console.Clear();
            Console.WriteLine($"Naziv: {task.Name}\nOpis: {task.Description}\nRok: {task.DueDate.ToString("dd/MM/yyyy")}\n" +
                    $"Status: {task.Status}\nOčekivano trajanje: {task.ExpectedDuration} min\nPridruženi projekt (ID): {task.AssociatedProjectId.ToString()}");
        }
        private static void EditTaskStatus(Task task)
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

        }
        public static void ManageSpecificTask(Dictionary<Project, List<Task>> projectTasks, Task task)
        {
            do
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
                        EditTaskStatus(task);
                        break;
                    case "0":
                        Console.WriteLine("Vraćate se na glavni izbornik...");
                        return;
                    default:
                        Console.WriteLine("Neispravan unos. Pokušajte ponovno.");
                        break;
                }

                Console.ReadKey();
            } while (true);
        }
    
    }
}
