using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Server.Domain
{
    public class TodoEntity
    {
        public int? Id { get; set; }
        public string Assignee { get; set; } = "";
        public string Summary { get; set; } = "";
        public string Details { get; set; } = "";
        public DateTime? DateAssigned { get; set; }
        public DateTime? DateFinished { get; set; }
        public DateTime? DueDate { get; set; }
        public string SecretData { get; set; } = "";
    }
}
