using System;
using System.Collections.Generic;
using System.Text;
using ValetParking.Dto;
using ValetParking.Models.Domain;

namespace ValetParking.BusinessLogic.Interfaces
{
    public interface IVehicleService    
{
       
        void CreateVehicle(VehicleDto vehicle, string userEmail);
        List<VehicleDomain> GetAllByUserId(string email);
    }
}
