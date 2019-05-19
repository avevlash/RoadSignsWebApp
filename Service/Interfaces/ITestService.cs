using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ITestService
    {
        Test AddUserTest(User user, string type);
        List<Test> GetUserTests(User user);
        void FinishUserTest(Test test);
        int GetCorrectAnsPercent(User user);
    }
}
