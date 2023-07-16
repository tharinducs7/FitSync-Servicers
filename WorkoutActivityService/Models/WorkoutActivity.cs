using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutActivityService.Models
{
    public class WorkoutActivity
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please select a workout type.")]
        public string WorkoutType { get; set; }

        [Required(ErrorMessage = "Please enter the duration in minutes.")]
        public int DurationInMinutes { get; set; }
        public double CaloriesBurnedPerMinute { get; set; }
        public double DistanceInKm { get; set; }
        public DateTime DateTime { get; set; }
    }
}
