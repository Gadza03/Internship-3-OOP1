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
            var taskExists = false;
            foreach (var project in projectTasks)
            {
                foreach (var task in project.Value)
                {
                    TimeSpan difference = task.DueDate - currentDate;
                    if (difference.Days <= 7 && difference.Days >= 0)
                    {
                        Console.WriteLine($"Zadatak {task.Name} ima rok u sljedecih {difference.Days} dana.");
                        taskExists = true;
                    }
                }
            }
            if (!taskExists)
                Console.WriteLine("Ne postoji niti jedan zadatak kojemu je rok u sljedecih 7 dana.");
        }
        public static void AddNewProject(Dictionary<Project, List<Task>> projectTasks)
        {
            var projectName = GetValidProjectName(projectTasks);
            var projectDescription = GetDescription();
            var projectStartDate = GetValidDate("Unesite datum početka projekta: ");
            var projectEndDate = GetValidDate("Unesite datum kraja projekta: ");
            var projectStatus = GetValidStatus("");
            Console.Clear();
            var newProject = new Project(projectName, projectDescription, projectStartDate, projectEndDate, projectStatus);
            projectTasks.Add(newProject, new List<Task>());
            Console.WriteLine($"Projekt {projectName} je uspjesno dodan.");

        }
        private static string GetValidProjectName(Dictionary<Project, List<Task>> projectTasks)
        {
            string name;
            var isValid = false;
            do
            {
                Console.Clear();
                Console.Write("Unesite ime projekta: ");
                name = Console.ReadLine().Trim();
                var isNameExists = projectTasks.Keys.Any(p => p.Name.ToLower() == name.ToLower());
                var isUniqueName = CheckIfNameIsUnique(name, isNameExists, "projekta", "Ne smiju biti ista imena dvaju projekata");
                if (!isUniqueName)
                    continue;

                isValid = true;
            } while (!isValid);  
            return name;
        }
        private static bool CheckIfNameIsUnique(string name, bool nameExists, string firstPrompt, string secondPrompt)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine($"Ime {firstPrompt} ne može biti prazno.");
                Console.ReadKey();
                return false;
            }
            if (nameExists)
            {
                Console.WriteLine($"Ime {firstPrompt} je zauzeto. {secondPrompt}.");
                Console.ReadKey();
                return false;
            }
            return true; 
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
        private static ProjectStatus GetValidStatus(string prompt)
        {
            ProjectStatus status;
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {prompt}status projekta:\n0 - Aktivno\n1 - Na čekanju\n2 - Završen\n");
                if (Enum.TryParse(Console.ReadLine(), out status) && Enum.IsDefined(typeof(ProjectStatus), status))               
                    break;
                
                Console.WriteLine("Neispravan status! Pokušajte ponovo.");
                Console.ReadKey();
            }
            return status;
        }
        public static Project ChooseProject(Dictionary<Project, List<Task>> projectTasks)
        {
            var isFoundedProject = false;
            Project foundedProject;
            do
            {
                Console.Clear();
                Console.WriteLine("Popis projekata...\n");
                foreach (var project in projectTasks)
                    Console.WriteLine($"- {project.Key.Name}");
                Console.Write("Odaberite koji projekt: ");
                var selectedProject = Console.ReadLine().Trim();
                foundedProject = projectTasks.Keys.FirstOrDefault(p => p.Name.ToLower() == selectedProject.ToLower());
                if (foundedProject == null)
                {
                    Console.WriteLine("Ne postoji project sa tim imenom. Pokusajte ponovno!");
                    Console.ReadKey();
                    continue;
                }
                isFoundedProject = true;                
            } while (!isFoundedProject);
            Console.WriteLine($"Uspjesno ste obrisali projekt {foundedProject.Name}.");
            return foundedProject;
        }
        private static bool ConfirmationDialog()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Zelite li zaista obrisati projekt (Da/Ne): ");
                string enteredChoice = Console.ReadLine().Trim().ToLower();
                if (enteredChoice == "da")
                    return true;
                else if (enteredChoice == "ne")
                    return false;
                else
                {
                    Console.WriteLine("Neispravan unos. Unesite Da ili Ne.");
                    Console.ReadKey();
                    continue;
                }
            }
        }
        public static void DeleteProject(Dictionary<Project, List<Task>> projectTasks)
        {
            var foundedProject = ChooseProject(projectTasks);
            var confirmationToDelete = ConfirmationDialog();

            if (confirmationToDelete)
            {
                projectTasks.Remove(foundedProject);
                Console.WriteLine($"Uspjesno brisanje projekta {foundedProject.Name}.");
            }            
            else
                Console.WriteLine($"Otkazano brisanje projekta {foundedProject.Name}.");
        }
        public static void FilterProjectsByStatus(Dictionary<Project, List<Task>> projectTasks)
        {
            ProjectStatus status = GetValidStatus("");
            Console.Clear();
            foreach (var project in projectTasks)
            {
                if (project.Key.Status == status)
                    Console.WriteLine($"Naziv: {project.Key.Name} - Status: {project.Key.Status.ToString()}");
            }
        }
        private static void DisplayTasksInProject(Dictionary<Project, List<Task>> projectTasks, Project project)
        {
            Console.Clear();
            var tasks = projectTasks[project];
            if (tasks.Any())
            {
                Console.WriteLine($"Popis zadataka za projekt {project.Name}:\n");
                foreach (var task in tasks)                
                    Console.WriteLine($"- {task.Name} (Status: {task.Status}, Rok: {task.DueDate.ToString("dd/MM/yyyy")})"); 
                //za commit samo napravljen menu za manage project i napravljena prva tocka
            }
        }
        private static void DisplayProjectDetails(Project project)
        {
            Console.Clear();
            Console.WriteLine("Prikaz podataka odabranog projekta: \n");
            Console.WriteLine($"ID: {project.GetId()}\nName: {project.Name}\nDescription: {project.Description}" +
                $"\nStart date: {project.StartDate.ToString("dd/MM/yyyy")}\nEnd date: {project.EndDate.ToString("dd/MM/yyyy")}\nStatus: {project.Status}");
        }
        private static void EditProjectStatus(Project project)
        {
            ProjectStatus projectCopyOld = project.Status;
            if (project.Status == ProjectStatus.Completed)
            {
                Console.WriteLine("Projekt je završen i ne možete mu mijenjati status.");
                return;
            }
            var status = GetValidStatus("novi ");
            project.Status = status;
            Console.WriteLine($"Projektu je uspješno promjenjen status sa {projectCopyOld} na {project.Status}.");
        }
        private static string GetValidTaskName(Dictionary<Project, List<Task>> projectTasks, Project project)
        {            
            string name;
            var isValid = false;
            do
            {
                Console.Clear();
                Console.Write("Unesite ime zadatka: ");
                name = Console.ReadLine().Trim();
                var isNameExists = projectTasks[project].Any(task => task.Name.ToLower() == name.ToLower());
                var isUniqueName = CheckIfNameIsUnique(name, isNameExists, "zadatka", "Ne smiju biti ista imena dvaju zadataka u istom projektu");
                if (!isUniqueName)
                    continue;
                
                isValid = true;
            } while (!isValid);
            return name;
        }
        public static StatusTask GetValidTaskStatus()
        {
            StatusTask status;
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite status projekta:\n0 - Aktivan\n1 - Završen\n2 - Odgođen\n");
                if (Enum.TryParse(Console.ReadLine(), out status) && Enum.IsDefined(typeof(StatusTask), status))
                    break;

                Console.WriteLine("Neispravan status! Pokušajte ponovo.");
                Console.ReadKey();
            }
            return status;
        }
        private static int GetExpectedDuration()
        {
            var expectedDuration = 0;
            var isValid = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Unestie očekivano vrijeme trajanja zadatka (min): ");
                if (!int.TryParse(Console.ReadLine(), out expectedDuration) || expectedDuration <= 0)
                {
                    Console.WriteLine("Neispravan unos, mora biti broj veci od 0. Pokušajte ponovno.");
                    Console.ReadKey();
                    continue;
                }
                isValid = true;
            } while (!isValid);
            return expectedDuration;
        }
        public static void AddTaskToProject(Dictionary<Project, List<Task>> projectTasks, Project project)
        {
            var taskName = GetValidTaskName(projectTasks,project);
            var taskDescription = GetDescription();
            var taskDueDate = GetValidDate("Unesite datum za rok izvršavanja zadataka: ");
            if (taskDueDate < project.StartDate || taskDueDate > project.EndDate)
            {
                Console.WriteLine($"Datum mora biti u rasponu od {project.StartDate.ToString("dd/MM/yyyy")} do {project.EndDate.ToString("dd/MM/yyyy")}");
                Console.ReadKey();
                taskDueDate = GetValidDate("Unesite datum za rok izvršavanja zadataka: ");
            }
            var taskStatus = GetValidTaskStatus();
            var taskExpectedDuration = GetExpectedDuration();
            var associatedProjectId = project.GetId();

            var newTask = new Task(taskName, taskDescription, taskDueDate, taskStatus, taskExpectedDuration, associatedProjectId);
            projectTasks[project].Add(newTask);
            Console.WriteLine($"Uspješno ste dodali zadatak {taskName} u projekt {project.Name}.");


        }

        public static void DeleteTaskFromProject(Dictionary<Project, List<Task>> projectTasks, Project project)
        {
            Task foundedTask;
            do
            {
                Console.Clear();
                Console.WriteLine($"Lista zadataka za {project.Name}: ");
                foreach (var task in projectTasks[project])
                    Console.WriteLine($"- {task.Name}");
                Console.Write("\nOdaberite koji zadatak želite: ");
                var selectedTask = Console.ReadLine().Trim().ToLower();
                foundedTask = projectTasks[project].FirstOrDefault(task => task.Name.ToLower() == selectedTask);
                if (foundedTask == null)
                {
                    Console.WriteLine("Uneseni zadatak ne postoji. Molimo pokušajte ponovno");
                    Console.ReadKey();
                    continue;
                }
                break;
            } while (true);
            var confirmationToDelete = ConfirmationDialog();
            if (confirmationToDelete)
            {
                projectTasks[project].Remove(foundedTask);
                Console.WriteLine($"Uspjesno ste uklonili zadatak {foundedTask.Name} iz projekta {project.Name}.");
            }
            else
                Console.WriteLine($"Prekinuto uklanjanje zadataka {foundedTask.Name} iz projekta {project.Name}.");
        }
        public static void DisplayActiveTasksTime(Dictionary<Project, List<Task>> projectTasks, Project project)
        {
            Console.Clear();
            var activeTasksList = projectTasks[project].Where(task => task.Status == StatusTask.Active).ToList();
            var sumOfDuration = 0;
            foreach (var task in activeTasksList)            
                sumOfDuration += task.ExpectedDuration;

            if (sumOfDuration == 0)            
                Console.WriteLine($"Projekt {project.Name} ne sadrži zadatke.");
            else
                Console.WriteLine($"Ukupno očekivano vrijeme potrebno za sve zadatke iznosi: {sumOfDuration} min");          
        }
        public static void ManageProject(Dictionary<Project, List<Task>> projectTasks, Project project)
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"Upravljanje projektom {project.Name}:\n");
                Console.WriteLine("1. Ispis svih zadataka unutar odabranog projekta");
                Console.WriteLine("2. Prikaz detalja odabranog projekta");
                Console.WriteLine("3. Uređivanje statusa projekta");
                Console.WriteLine("4. Dodavanje zadatka unutar projekta");
                Console.WriteLine("5. Brisanje zadatka iz projekta");
                Console.WriteLine("6. Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu");
                Console.WriteLine("0. Izlaz\n");
                Console.Write("Odaberite opciju: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayTasksInProject(projectTasks, project);
                        break;
                    case "2":
                        DisplayProjectDetails(project);
                        break;
                    case "3":
                        EditProjectStatus(project);
                        break;
                    case "4":
                        AddTaskToProject(projectTasks,project);
                        break;
                    case "5":
                        DeleteTaskFromProject(projectTasks,project);
                        break;
                    case "6":
                        DisplayActiveTasksTime(projectTasks,project);
                        break;
                    case "0":
                        Console.WriteLine("Vracate se korak nazad...");
                        return;
                    default:
                        Console.WriteLine("Nepostojeća opcija. Molimo odaberite ponovo.");
                        break;
                }
                Console.ReadKey();
            } while (true);
            
        }
    }
}
