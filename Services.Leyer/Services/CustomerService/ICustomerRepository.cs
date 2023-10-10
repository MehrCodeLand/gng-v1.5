﻿using Data.Leyer.Models.Structs;
using Data.Leyer.Models.ViewModels.Customer;
using goolrang_sales_v1.Models;

namespace Services.Leyer.Services.CustomerService
{
    public interface ICustomerRepository
    {
        Task<Responses<Customer>> CreateCustomer(CreateCustomerVm createCustomerVm);
    }
}