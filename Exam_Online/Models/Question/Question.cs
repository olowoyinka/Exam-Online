using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class Question
    {
        public int QuestionID { get; set; }

        public int QuestionCategoryID { get; set; }

        public string QuestionType { get; set; }

        public string Question1 { get; set; }

        public int ExhibitID { get; set; }

        public int Points { get; set; }

        public bool IsActive { get; set; }

        public Exhibit Exhibits { get; set; }

        public QuestionCategory QuestionCategorys { get; set; }

        public ICollection<Choice> Choices { get; set; }

        public ICollection<TestQuestion> TestQuestions { get; set; }


    }
}
