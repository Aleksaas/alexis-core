namespace AlexisCorePro.Business.Common.Commands
{
    public interface IBaseCommand
    {
        //public virtual void Validate<T, U>(U validator) where T : BaseCommand where U : AbstractValidator<T>
        //{
        //    ValidationResult results = validator.Validate(this as T);

        //    if (!results.IsValid)
        //    {
        //        IList<ValidationFailure> failures = results.Errors;

        //        throw new ValidationError(failures);
        //    }
        //}
    }
}
