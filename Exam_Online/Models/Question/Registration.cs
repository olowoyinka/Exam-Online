using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class Registration
    {
        public int RegistrationID { get; set; }

        public int StudentID { get; set; }

        public int TestID { get; set; }

        public DateTime RegistrationDate { get; set; }

        public Guid Token { get; set; }

        public DateTime TokenExpireTime { get; set; }

        public Student Students { get; set; }

        public Test Tests { get; set; }

        public ICollection<QuestionDuration> QuestionDurations { get; set; }

        public ICollection<TestPaper> TestPapers { get; set; }


    }
}
