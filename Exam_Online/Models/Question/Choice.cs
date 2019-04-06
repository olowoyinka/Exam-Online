using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class Choice
    {
        public int ChoiceID { get; set; }

        public string Label { get; set; }

        public decimal Points { get; set; }

        public bool IsActive { get; set; }

        public int QuestionID { get; set; }

        public Question Questions { get; set; }

        public  ICollection<TestPaper> TestPapers { get; set; }

    }
}
