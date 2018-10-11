using AlexisCorePro.Business.Common.Commands;
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
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public int CustomerId { get; set; }
    }

    public class ShipCommandValidator : AbstractValidator<ShipCommand>
    {
        private DatabaseContext ctx;

        public ShipCommandValidator(IStringLocalizer<SharedResource> stringLocalizer, DatabaseContext ctx)
        {
            this.ctx = ctx;

            RuleFor(cmd => cmd.Name).NotEmpty().MaximumLength(Default.TextFieldLength);
            RuleFor(cmd => cmd.Date).GreaterThan(DateTime.Today);
            RuleFor(cmd => cmd.Imd).NotEmpty();
            RuleFor(cmd => cmd.Mmsi).NotEmpty();
            RuleFor(cmd => cmd.CustomerId).NotEmpty();
            RuleFor(cmd => cmd)
            .Must(IsNameUnique)
                .WithMessage(stringLocalizer["NameUnique"])
            .Must(IsMaxNumberReachedForCustomer)
                .WithMessage(stringLocalizer["MuxNumberShips"])
            .Must(IsCustomerNotBlacklisted)
                .WithMessage(stringLocalizer["CustomerBlacklisted"]);

            // RuleFor(cmd => cmd.Name).Must((cmd, name) => name != cmd.Name);

            // If first one is not true, then chained one will not be executed, like in the first and last example here because of
            // ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure; in Startup.cs
        }

        private bool IsNameUnique(ShipCommand cmd)
        {
            return !ctx.Ships.Any(e => e.Name == cmd.Name);
        }

        private bool IsMaxNumberReachedForCustomer(ShipCommand cmd)
        {
            return ctx.Ships.Where(e => e.CustomerId == cmd.CustomerId).Count() < 3;
        }

        private bool IsCustomerNotBlacklisted(ShipCommand cmd)
        {
            return !ctx.Customers.Where(e => e.Id == cmd.CustomerId).Select(e => e.Blacklisted).First();
        }
    }
}
