using System;
using System.Collections.Generic;
using System.Text;

namespace ValetParking.Persistence.UnitOfWork
    {
    public interface IUnitOfWork : IDisposable
    {
        ValetParkingDbContext Context { get; }

        int Commit();

        }
    }
