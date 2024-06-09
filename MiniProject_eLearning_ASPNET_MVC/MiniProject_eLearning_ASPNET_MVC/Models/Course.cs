﻿namespace MiniProject_eLearning_ASPNET_MVC.Models
{
    public class Course :BaseEntity
    {     
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public string Duration { get; set; }
        public int NumberOfStudents { get; set; }
        public string Images { get; set; }
        public int InstructorId { get; set; } 
        public Instructor Instructor { get; set; } 

    }
}