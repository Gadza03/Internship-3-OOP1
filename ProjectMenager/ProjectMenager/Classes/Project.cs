using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMenager.Classes
{
    public class Project
    {
        private Guid id;
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime { get; set; }
        public ProjectStatus Status { get; set; }
        
        public Project(string name, string desctiption, DateTime start, DateTime end, ProjectStatus status)
        {
            this.id = Guid.NewGuid();
            this.Name = name;
            this.Description = desctiption;
            this.StartDate = start;
            this.EndTime = end;
            this.Status = status;
        }
        public Guid GetId()
        {
            return this.id;
        }
    }
}
