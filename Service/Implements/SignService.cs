using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implements
{
    public class SignService : ISignService
    {
        private readonly MainContext _appContext;
        public SignService(MainContext appContext)
        {
            _appContext = appContext;
        }

        public Sign GetSign(int id) => _appContext.Signs.Find(id);
        public void AddSign(Sign sign)
        {
            if (sign.ID != 0)
            {
                _appContext.Signs.Update(sign);

            }
            else
            {
                _appContext.Signs.Add(sign);
            }
            _appContext.SaveChanges();
        }
        public List<Sign> GetTypesSigns(string type)
        {
            switch (type)
            {
                case "Kids":
                    {
                        return _appContext.Signs.Where(x => x.ForKids).ToList();
                    }
                case "Pedestrians":
                    {
                        return _appContext.Signs.Where(x => x.ForPedestrians).ToList();
                    }
                case "Drivers":
                    {
                        return _appContext.Signs.Where(x => x.ForDrivers).ToList();
                    }
                case "Bikers":
                    {
                        return _appContext.Signs.Where(x => x.ForBikers).ToList();
                    }
                default:
                    {
                        return _appContext.Signs.ToList();
                    }
            }
        }

        public void RemoveSign(Sign sign)
        {
            _appContext.Signs.Remove(sign);
            _appContext.SaveChanges();
        }
    }
}
