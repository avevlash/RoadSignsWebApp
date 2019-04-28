using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IQuestionService
    {
        void AddQuestion(Question question);
        void RemoveQuestion(Question question);
        List<Question> GetTypedQuestions(string type, int count);
        List<Question> GetAllQuestions();
        Question GetQuestion(int id);
        void EditQuestion(Question question);
    }
}
