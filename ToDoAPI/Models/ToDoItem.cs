﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
        public ToDoList ToDoList { get; set; }
        public long ToDoListId { get; set; }
        public bool IsComplete { get; set; }
        public string UserId { get; set; }
    }
}
