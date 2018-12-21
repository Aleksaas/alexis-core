﻿using AlexisCorePro.Business.Common.Commands;
using AlexisCorePro.Business.Customers.Validations;
using AlexisCorePro.Business.Ships.Validations;
using AlexisCorePro.Domain;
using FluentValidation;
using Localization.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;

namespace AlexisCorePro.Business.Ships.Commands
{
    public class ShipCommand : BaseCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public int CustomerId { get; set; }
    }

    public class ShipCommandValidator : AbstractValidator<ShipCommand>
    {
        private readonly ShipValidations shipValidations;
        private readonly CustomerValidations customerValidations;

        public ShipCommandValidator(IStringLocalizer<SharedResource> stringLocalizer
            , ShipValidations shipValidations, CustomerValidations customerValidations)
        {
            this.shipValidations = shipValidations;
            this.customerValidations = customerValidations;

            RuleFor(cmd => cmd.Name).NotEmpty().MaximumLength(Default.TextFieldLength);
            RuleFor(cmd => cmd.Date).GreaterThan(DateTime.Today);
            RuleFor(cmd => cmd.Imd).NotEmpty();
            RuleFor(cmd => cmd.Mmsi).NotEmpty();
            RuleFor(cmd => cmd.CustomerId).NotEmpty();
            RuleFor(cmd => cmd)
            .Must((cmd) => shipValidations.IsNameUnique(cmd.Name, cmd.Id)).WithMessage(stringLocalizer["NameUnique"])
            .Must((cmd) => shipValidations.IsMaxNumberReachedForCustomer(cmd.CustomerId)).WithMessage(stringLocalizer["MuxNumberShips"])
            .Must((cmd) => customerValidations.IsCustomerNotBlacklisted(cmd.CustomerId)).WithMessage(stringLocalizer["CustomerBlacklisted"]);

            // RuleFor(cmd => cmd.Name).Must((cmd, name) => name != cmd.Name);

            // If first one is not true, then chained one will not be executed, like in the first and last example here because of
            // ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure; in Startup.cs
        }
    }
}