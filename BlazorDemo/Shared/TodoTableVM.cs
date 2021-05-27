using System;

namespace BlazorDemo.Shared
{
    public class TodoTableVM
    {
        public int Id { get; set; }
        public string Assignee { get; set; } = "";
        public string Summary { get; set; } = "";
        public bool Finished { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
