using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ToDoAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IList<ToDoItem> Items { get; set; }
    }
}
