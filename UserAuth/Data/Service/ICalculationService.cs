using System.Collections.Generic;
using UserAuth.ApiModels;
using UserAuth.Controllers;
using UserAuth.Data.Entities;

namespace UserAuth.Data.Service
{
    public interface ICalculationService
    {
        IEnumerable<Calculation> GetAll();
        Calculation Find(int id);
        Calculation Add(Calculation calculation);
        void Delete(Calculation calculation);
        void Delete(int id);
        Calculation update(Calculation calculation);

    }
}