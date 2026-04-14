using System.Collections.Generic;
using System.Linq;
using DailyRoutineApp.Models;

namespace DailyRoutineApp.Services
{
    public class RoutineService
    {
        private readonly List<Routine> _routines = new();
        private int _nextRoutineId = 1;
        private int _nextActivityId = 1;
        private readonly object _lock = new();

        public IEnumerable<Routine> GetAll()
        {
            lock (_lock)
            {
                return _routines
                    .OrderBy(r => r.Date)
                    .Select(CloneRoutine)
                    .ToList();
            }
        }

        public Routine? Get(int id)
        {
            lock (_lock)
            {
                var routine = _routines.FirstOrDefault(r => r.Id == id);
                return routine is null ? null : CloneRoutine(routine);
            }
        }

        public Routine Add(Routine routine)
        {
            lock (_lock)
            {
                routine.Id = _nextRoutineId++;
                routine.Activities = routine.Activities
                    .Where(a => !string.IsNullOrWhiteSpace(a.Name))
                    .Select(a =>
                    {
                        a.Id = _nextActivityId++;
                        a.RoutineId = routine.Id;
                        return a;
                    })
                    .ToList();

                var copy = CloneRoutine(routine);
                _routines.Add(copy);
                return CloneRoutine(copy);
            }
        }

        public bool Update(Routine routine)
        {
            lock (_lock)
            {
                var existing = _routines.FirstOrDefault(r => r.Id == routine.Id);
                if (existing == null) return false;

                existing.Title = routine.Title;
                existing.Date = routine.Date;
                existing.Notes = routine.Notes;
                existing.Activities = routine.Activities
                    .Where(a => !string.IsNullOrWhiteSpace(a.Name))
                    .Select(a =>
                    {
                        a.Id = _nextActivityId++;
                        a.RoutineId = existing.Id;
                        return a;
                    })
                    .ToList();

                return true;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                var routine = _routines.FirstOrDefault(r => r.Id == id);
                if (routine == null) return false;
                _routines.Remove(routine);
                return true;
            }
        }

        private static Routine CloneRoutine(Routine routine)
        {
            return new Routine
            {
                Id = routine.Id,
                Title = routine.Title,
                Date = routine.Date,
                Notes = routine.Notes,
                Activities = routine.Activities
                    .Select(a => new Activity
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Time = a.Time,
                        DurationMinutes = a.DurationMinutes,
                        Notes = a.Notes,
                        RoutineId = a.RoutineId
                    })
                    .ToList()
            };
        }
    }
}
