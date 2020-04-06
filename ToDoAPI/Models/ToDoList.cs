using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDoList
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public IList<ToDoItem> ToDoItems { get; set; }
    }
}
