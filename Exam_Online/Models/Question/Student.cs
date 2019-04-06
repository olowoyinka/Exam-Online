using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class Student
    {
        public int StudentID { get; set; }

        public string Name { get; set; }

        public int AccessLevel { get; set; }

        public DateTime EntryDate { get; set; }
         
        public string Email { get; set; }

        public string Phone { get; set; }

        public string PassHash { get; set; }

        public Registration Registrations { get; set; }
    }
}
