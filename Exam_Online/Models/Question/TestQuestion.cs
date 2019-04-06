using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class TestQuestion
    {
        public int TestQuestionID { get; set; }

        public int TestID { get; set; }

        public int QuestionID { get; set; }

        public int QuestionNumber { get; set; }

        public bool IsActive { get; set; }

        public Question Questions { get; set; } 

        public Test Tests { get; set; }

        public IList<QuestionDuration> QuestionDurations { get; set; }

        public IList<TestPaper> TestPapers { get; set; }


    }
}
