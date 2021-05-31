using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Persistence.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IUnitOfWork unitOfWork;

        public IUnitOfWork Current
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork();
                }

                return unitOfWork;
            }
        }

        public void Dispose()
        {

            if (unitOfWork != null)
            {
                unitOfWork.Dispose();
            }
        }
    }
}
