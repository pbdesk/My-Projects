using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI6.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}