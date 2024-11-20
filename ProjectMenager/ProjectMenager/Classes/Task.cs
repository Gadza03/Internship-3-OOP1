using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMenager.Classes
{
    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime DueDate { get; set; }
        public StatusTask Status { get; set; }
        public int ExpectedDuration { get; set; }
        public Guid AssociatedProjectId { get; set; }
        public Task(string name, string description, DateTime dueDate, StatusTask status, int expectedDuration, Guid project)
        {
            this.Name = name;
            this.Description = description;
            this.DueDate = dueDate;
            this.Status = status;
            this.ExpectedDuration = expectedDuration;
            this.AssociatedProjectId = project;
        }

    }
}
