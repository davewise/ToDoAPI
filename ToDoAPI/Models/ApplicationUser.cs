using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ToDoAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IList<ToDoItem> ToDoItems { get; set; }
        public IList<ToDoList> ToDoLists { get; set; }
    }
}
