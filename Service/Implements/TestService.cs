using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            _appContext.Tests.Update(test);
            _appContext.SaveChanges();
        }

        public double GetCorrectAnsPercent(User user)
        {
            double general = _appContext.Statistics.Where(x => x.Test.User.ID == user.ID).Count();
            double right = _appContext.Statistics.Where(x => x.Test.User.ID == user.ID && x.HasAnswered).Count();
            return right / general;
        }

        public List<Test> GetUserTests(User user) => _appContext.Tests.Where(x => x.User.ID == user.ID).ToList();
    }
}
