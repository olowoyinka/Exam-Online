using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class QuestionDuration
    {
        public int QuestionDurationID { get; set; }

        public int RequestTime { get; set; }

        public int LeaveTime { get; set; }

        public int AnsweredTime { get; set; }

        public int RegistrationID { get; set; }

        public int TestQuestionID { get; set; }

        public Registration Registration { get; set; }

        public TestQuestion TestQuestion { get; set; }
    }
}
