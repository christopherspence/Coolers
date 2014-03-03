using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coolers.Managers.Contract;
using Coolers.Managers.Implementation;

using Moq;
using Coolers.Models;
using Coolers.DAL.Contract;
using Coolers.Repositories.Implementation;
using Coolers.Common.Validator.Implementation;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using Coolers.Repositories.Contract;
using Coolers.Common;

namespace Coolers.Tests
{
    [TestClass]
    public class CoolerManagerTests
    {
        private ICoolerManager _manager;
        public ICoolerManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    Mock<IQueryableUnitOfWork> mockUnitOfWork = new Mock<IQueryableUnitOfWork>();
                    mockUnitOfWork.Setup(m => m.CreateSet<Cooler>()).Returns<DbSet<Cooler>>(set => set);
                    mockUnitOfWork.Setup(m => m.Commit());

                    Mock<CoolerRepository> mockCoolerRepo = new Mock<CoolerRepository>(mockUnitOfWork.Object);
                    Mock<BeverageRepository> mockBeverageRepo = new Mock<BeverageRepository>(mockUnitOfWork.Object);

                    _manager = new CoolerManager(mockCoolerRepo.Object, mockBeverageRepo.Object, new DataAnnotationsEntityValidatorFactory());
                }

                return _manager;
            }
                
        }
        [TestMethod]
        public void CanInsertBeverageIntoCooler()
        {
            Cooler cooler = new Cooler
            {
                Id = Guid.NewGuid(),
                Name = "Test Cooler",
                MaxCapacity = 5f
            };

            Beverage beverage = new Beverage
            {
                Id = Guid.NewGuid(),
                Name = "Test Bev",
                Size = 5f
            };

            cooler.Beverages.Add(beverage);
            this.Manager.AddCooler(cooler);

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationValidationErrorsException))]
        public void WillThrowErrorOnExceededCoolerLimit()
        {
            Cooler cooler = new Cooler
            {
                Id = Guid.NewGuid(),
                Name = "Test Cooler",
                MaxCapacity = 5f
            };

            Beverage beverage = new Beverage
            {
                Id = Guid.NewGuid(),
                Name = "Test Bev",
                Size = 50f
            };

            cooler.Beverages.Add(beverage);
            this.Manager.AddCooler(cooler);

            Assert.Fail("Expected an ApplicationValidationErrorsException");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationValidationErrorsException))]
        public void WillThrowErrorOnInvalidBeverageInsert()
        {
            Cooler cooler = new Cooler
            {
                Id = Guid.NewGuid(),
                Name = "Test Cooler",
                MaxCapacity = 5f, 
                Sealed = true,
                Iced = false
            };

            Beverage beverage = new Beverage
            {
                Id = Guid.NewGuid(),
                Name = "Test Bev",
                Size = 50f,
                NeedsIced = true
            };

            cooler.Beverages.Add(beverage);
            this.Manager.AddCooler(cooler);

            Assert.Fail("Expected an ApplicationValidationErrorsException");
        }
        
    }
}
