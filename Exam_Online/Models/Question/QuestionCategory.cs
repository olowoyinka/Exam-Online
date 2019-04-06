using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class QuestionCategory
    {
        public int QuestionCategoryID { get; set; }

        public string Category { get; set; }

        public ICollection<Question> Questions { get; set; }

    }
}
