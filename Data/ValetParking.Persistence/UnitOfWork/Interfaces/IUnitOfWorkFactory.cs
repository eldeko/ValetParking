using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Persistence.UnitOfWork
{
    public interface IUnitOfWorkFactory : IDisposable
    {
        IUnitOfWork Current { get; }
    }
}
