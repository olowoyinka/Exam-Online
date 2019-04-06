using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class TestPaper
    {
        public int TestPaperID { get; set; }

        public int ChoiceID { get; set; }

        public int RegistrationID { get; set; }

        public int TestQuestionID { get; set; }

        public string Answer { get; set; }

        public decimal MarkScored { get; set; }

        public Choice Choices  { get; set; }

        public Registration Registrations { get; set; }

        public TestQuestion TestQuestions { get; set; }
    }
}
