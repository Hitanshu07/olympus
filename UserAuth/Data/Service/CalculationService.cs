using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserAuth.ApiModels;
using UserAuth.Controllers;
using UserAuth.Data.Core;
using UserAuth.Data.Entities;
using UserAuth.Helpers;
using WebGrease.Css.Extensions;

namespace UserAuth.Data.Service
{
    public class CalculationService : ICalculationService
    {
        private readonly UserDbContext _dbContext;

        public CalculationService(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Calculation> GetAll()
        {
            return _dbContext.Calculations.ToList();
        }

        public Calculation Find(int id)
        {
            return _dbContext.Calculations.Find(id);
        }


        public Calculation Add(Calculation calculation)
        {
            calculation.UpdatedOnUtc = calculation.CreatedOnUtc = DateTime.UtcNow;
            _dbContext.Calculations.Add(calculation);
            _dbContext.SaveChanges();
            return calculation;
        }

        public Calculation update(Calculation calculation)
        {
            var oldcalculation = Find(calculation.CalculationId);
            oldcalculation.CreatedOnUtc = oldcalculation.CreatedOnUtc;
            oldcalculation.IsDisabled = calculation.IsDisabled;
            oldcalculation.PRAId = calculation.PRAId;
            oldcalculation.Time1Scope = calculation.Time1Scope;
            oldcalculation.Time2Scope = calculation.Time2Scope;
            oldcalculation.Time3Scope = calculation.Time3Scope;
            oldcalculation.Time4Scope = calculation.Time4Scope;
            oldcalculation.Time5Scope = calculation.Time5Scope;
            oldcalculation.UpdatedOnUtc = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return calculation;
        }

        public void Delete(Calculation calculation)
        {
            _dbContext.Calculations.Remove(calculation);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var Calculation = Find(id);
            _dbContext.Calculations.Remove(Calculation);
            _dbContext.SaveChanges();
        }
    }
}