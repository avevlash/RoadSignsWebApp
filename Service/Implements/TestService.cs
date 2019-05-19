using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Service.Implements
{
    public class TestService : ITestService
    {
        private readonly MainContext _appContext;
        public TestService(MainContext ctx)
        {
            _appContext = ctx;
        }
        public Test AddUserTest(User user, string type)
        {
            Test test = new Test() { User = user };
            var query = _appContext.Questions.AsQueryable();
            switch (type)
            {
                case "Drivers":
                    {
                        query = query.Where(x => x.ForDrivers);
                        break;
                    }
                case "Bikers":
                    {
                        query = query.Where(x => x.ForBikers);
                        break;
                    }
                case "Pedestrians":
                    {
                        query = query.Where(x => x.ForPedestrians);
                        break;
                    }
                case "Kids":
                    {
                        query = query.Where(x => x.ForKids);
                        break;
                    }
            }
            test.Stats = new List<Statistic>();
            var items = query.OrderBy(x => Guid.NewGuid()).Take(20).ToList();
            foreach (var item in items)
                test.Stats.Add(new Statistic() { Question = item, HasAnswered = false });
            _appContext.Tests.Add(test);
            _appContext.SaveChanges();
            return test;
        }

        public void FinishUserTest(Test test)
        {
            foreach (var stat in test.Stats)
            {
                _appContext.Entry(stat.Question.Sign).State = EntityState.Unchanged;
                _appContext.Entry(stat.Question.Sign.Type).State = EntityState.Unchanged;
            }
            _appContext.Tests.Update(test);
            _appContext.SaveChanges();
        }

        public int GetCorrectAnsPercent(User user)
        {
            int general = _appContext.Statistics.Count(x => x.Test.User.ID == user.ID);
            int right = _appContext.Statistics.Count(x => x.Test.User.ID == user.ID && x.HasAnswered);
            return right*100 / general;
        }

        public List<Test> GetUserTests(User user) => _appContext.Tests.Where(x => x.User.ID == user.ID).ToList();
    }
}
