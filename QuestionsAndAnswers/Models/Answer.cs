using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionsAndAnswers.Models
{
    public class Answer
    {
        public  int id { get; set; }
        public string text { get; set; }
        public int QuestionID { get; set; }
        public int UserID { get; set; }
    }
}