using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ISignService
    {
        List<Sign> GetTypesSigns(string type);
        void AddSign(Sign sign);
        Sign GetSign(int id);
        void RemoveSign(Sign sign);
        List<SignType> GetSignTypes();
    }
}
