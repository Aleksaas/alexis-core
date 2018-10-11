using AlexisCorePro.Business.Common.Commands;
using AlexisCorePro.Domain;
using FluentValidation;
using Localization.Resources;
using Microsoft.Extensions.Localization;
using System;

namespace AlexisCorePro.Business.Ships.Commands
{
    public class ShipCommand : BaseCommand
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public int CustomerId { get; set; }
    }

    public class ShipCommandValidator : AbstractValidator<ShipCommand>
    {
        public ShipCommandValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(cmd => cmd.Name).NotEmpty().MaximumLength(Default.TextFieldLength);
            RuleFor(cmd => cmd.Date).GreaterThan(DateTime.Today);
            RuleFor(cmd => cmd.Imd).NotEmpty();
            RuleFor(cmd => cmd.Mmsi).NotEmpty();
            RuleFor(cmd => cmd.CustomerId).NotEmpty();
            RuleFor(cmd => cmd)
            .Must(HaveUniqueName)
                .WithMessage(stringLocalizer["NameUnique"])
            .Must(MustNotExceedMaxCustomerShipNum)
                .WithMessage(stringLocalizer["MuxNumberShips"]);

            // If first one is not true, then chained one will not be executed, like in the first and last example here because of
            // ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure; in Startup.cs
        }

        private bool HaveUniqueName(ShipCommand cmd)
        {
            return false;
        }

        private bool MustNotExceedMaxCustomerShipNum(ShipCommand cmd)
        {
            return true;
        }
    }
}
