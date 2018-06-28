using AlexisCorePro.Business.Common.Commands;
using FluentValidation;

namespace AlexisCorePro.Business.Ships.Commands
{
    public class ShipCommand : BaseCommand
    {
        public string Name { get; set; }

        public int Imd { get; set; }

        public int Mmsi { get; set; }

        public int CustomerId { get; set; }
    }

    public class ShipCommandValidator : AbstractValidator<ShipCommand>
    {
        public ShipCommandValidator()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().MaximumLength(100);
            RuleFor(cmd => cmd.Imd).NotEmpty();
            RuleFor(cmd => cmd.Mmsi).NotEmpty();
            RuleFor(cmd => cmd.CustomerId).NotEmpty();
            // RuleFor(cmd => cmd).Must(HaveUniqueName).Must(MustNotExceedMaxCustomerShipNum);
        }

        private bool HaveUniqueName(ShipCommand cmd)
        {
            return false;
        }

        private bool MustNotExceedMaxCustomerShipNum(ShipCommand cmd)
        {
            return false;
        }
    }
}
