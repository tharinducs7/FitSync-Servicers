using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitSync_Servicers.Authentication
{
    public class Registration
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set;  }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string BloodType { get; set; }

        [Required(ErrorMessage = "Please select Gender.")]
        public string Gender { get; set; }
        public double DailyCalorieGoal { get; set; }
        public double DailyExerciseGoal { get; set; }
    }
}
