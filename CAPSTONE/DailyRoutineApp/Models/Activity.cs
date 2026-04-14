using System;

namespace DailyRoutineApp.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TimeSpan Time { get; set; }
        public int DurationMinutes { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int RoutineId { get; set; }
    }
}