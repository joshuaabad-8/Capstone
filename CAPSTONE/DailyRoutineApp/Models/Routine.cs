using System;
using System.Collections.Generic;

namespace DailyRoutineApp.Models
{
    public class Routine
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
        public List<Activity> Activities { get; set; } = new();
        public string Notes { get; set; } = string.Empty;
    }
}