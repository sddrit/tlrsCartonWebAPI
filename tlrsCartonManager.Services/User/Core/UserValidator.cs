﻿using FluentValidation;
using System.Collections.Generic;
using tlrsCartonManager.Core.Enums;
using tlrsCartonManager.DAL.Reporsitory.IRepository;

namespace TransnationalLanka.ThreePL.Services.User.Core
{
    public class UserValidator : AbstractValidator<tlrsCartonManager.DAL.Dtos.UserDto>
    {
      
        public UserValidator(string transactionType)
        {
            RuleFor(p => p.UserName).NotNull();
            RuleFor(p => p.UserPassword).NotNull();
            if (transactionType != TransactionType.Reset.ToString())          
            {            
              
                RuleFor(p => p.EmpId).NotNull();
                RuleFor(p => p.UserFullName).NotNull();

            }
        }
     
    }
  

    //public class SupplierAddressValidator : AbstractValidator<Dal.Entities.SupplierAddress>
    //{
    //    public SupplierAddressValidator()
    //    {
    //        RuleFor(p => p.AddressLine1).NotEmpty();
    //        RuleFor(p => p.PostalCode).NotEmpty();
    //        RuleFor(p => p.CityId).NotEqual(0);
    //    }
    //}
}
