using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorDemo.Shared
{
    public class TodoDetailsVM 
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "Assignee name is too long.")]
        public string Assignee { get; set; } = "";

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Summary is too long.")]
        public string Summary { get; set; } = "";

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Details description is too long.")]
        public string Details { get; set; } = "";
        public DateTime? DateAssigned { get; set; }
        public DateTime? DateFinished { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
        public bool Validate( )
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(this, context, results);
        }
    }

}
