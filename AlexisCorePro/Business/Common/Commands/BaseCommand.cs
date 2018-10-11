using AlexisCorePro.Business.Common.Model;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace AlexisCorePro.Business.Common.Commands
{
    public abstract class BaseCommand
    {
        public virtual void Validate<T, U>(U validator) where T : BaseCommand where U : AbstractValidator<T>
        {
            // var validator = new U();

            ValidationResult results = validator.Validate(this as T);

            if (!results.IsValid)
            {
                IList<ValidationFailure> failures = results.Errors;

                throw new ValidationError(failures);
            }
        }
    }
}
