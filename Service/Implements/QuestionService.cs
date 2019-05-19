using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Service.Implements
{
    public class QuestionService : IQuestionService
    {
        private readonly MainContext _appContext;
        public QuestionService(MainContext ctx)
        {
            _appContext = ctx;
        }

        public Question GetQuestion(int id) => _appContext.Questions.Find(id);
        public void AddQuestion(Question question)
        {
            _appContext.Questions.Add(question);
            _appContext.Entry(question.Sign).State = EntityState.Unchanged;
            _appContext.Entry(question.Sign.Type).State = EntityState.Unchanged;
            _appContext.SaveChanges();
        }

        public void EditQuestion(Question question)
        {
            _appContext.Variants.RemoveRange(_appContext.Variants.Where(x=>question.Variants.Contains(x)));
            _appContext.Entry(question.Sign).State = EntityState.Unchanged;
            _appContext.Entry(question.Sign.Type).State = EntityState.Unchanged;
            _appContext.Questions.Update(question);
            _appContext.SaveChanges();
        }

        public List<Question> GetAllQuestions() => _appContext.Questions.ToList();

        public List<Question> GetTypedQuestions(string type, int count)
        {
            var query = _appContext.Questions.AsQueryable();
            switch(type)
            {
                case "Driver":
                    {
                        query = query.Where(x => x.ForDrivers);
                        break;
                    }
                case "Biker":
                    {
                        query = query.Where(x => x.ForBikers);
                        break;
                    }
                case "Pedestrian":
                    {
                        query = query.Where(x => x.ForPedestrians);
                        break;
                    }
                case "Kid":
                    {
                        query = query.Where(x => x.ForKids);
                        break;
                    }
            }
            return query.OrderBy(x=>Guid.NewGuid()).Take(count).ToList();
        }

        public void RemoveQuestion(Question question)
        {
            _appContext.Questions.Remove(question);
            _appContext.SaveChanges();
        }
    }
}
