using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierInputModel, Supplier>();
            this.CreateMap<ImportPartsInputModel, Part>();
            this.CreateMap<ImportCustomersInputModel, Customer>();
            this.CreateMap<ImportSalesInputModel, Sale>();
        
        }
    }
}
