using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.IndexModel
{
    public class QuestionModel
    {
        public int TotalQuestionInset { get; set; }

        public int QuestionNumber { get; set; }

        public int TestId { get; set; }

        public string TestName { get; set; }

        public string Question { get; set; }

        public string QuestionType { get; set; }

        public decimal Point { get; set; }

        public Guid Token { get; set; }

        public List<QXModel> Options { get; set; }
    }

    public class QXModel
    {
        public int ChoiceId { get; set; }

        public string Label { get; set; }

        public string Answer { get; set; }

    }
}
