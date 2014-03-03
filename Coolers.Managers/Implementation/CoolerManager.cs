using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Coolers.Common;
using Coolers.Common.Validator.Contract;
using Coolers.Common.Validator.Implementation;
using Coolers.Managers.Contract;
using Coolers.Models;
using Coolers.Repositories.Contract;

namespace Coolers.Managers.Implementation
{
    public class CoolerManager : ICoolerManager
    {
        private ICoolerRepository _coolerRepository;
        private IBeverageRepository _beverageRepository;
        private IEntityValidatorFactory _entityValidatorFactory;

        public CoolerManager(ICoolerRepository coolerRepository, IBeverageRepository beverageRepository, IEntityValidatorFactory entityValidatorFactory)
        {
            this._coolerRepository = coolerRepository;
            this._beverageRepository = beverageRepository;
            this._entityValidatorFactory = entityValidatorFactory;
        }

        public IList<Cooler> GetAllCoolers()
        {
            return this._coolerRepository.GetAll().ToList();
        }

        public Cooler AddCooler(Cooler cooler)
        {
            return CreateCooler(cooler);
        }

        public void UpdateCooler(Cooler cooler)
        {
            Cooler existingCooler = _coolerRepository.Get(cooler.Id);

            cooler = UpdateCooler(existingCooler, cooler);

        }

        #region Private methods

        private Cooler CreateCooler(Cooler cooler)
        {
            IEntityValidator validator = this._entityValidatorFactory.Create();

            if (validator.IsValid<Cooler>(cooler))
            {
                this._coolerRepository.Add(cooler);
                this._coolerRepository.UnitOfWork.Commit();
                return cooler;
            }
            else 
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages(cooler));
        }

        private Cooler UpdateCooler(Cooler currentCooler, Cooler updatedCooler)
        {
            // TODO: Make this a factory
            IEntityValidator validator = this._entityValidatorFactory.Create();

            if (validator.IsValid<Cooler>(updatedCooler))
            {
                _coolerRepository.Merge(currentCooler, updatedCooler);
                _coolerRepository.UnitOfWork.Commit();

                return updatedCooler;
            }
            else
                throw new ApplicationValidationErrorsException(validator.GetInvalidMessages(updatedCooler));
        }

        #endregion
    }
}
