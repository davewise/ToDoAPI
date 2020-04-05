﻿using System;
namespace ToDoAPI.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}
