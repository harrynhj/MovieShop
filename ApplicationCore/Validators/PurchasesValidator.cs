using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validators;

public class PurchasesValidator : ValidationAttribute
{
    public PurchasesValidator()
    {
        ErrorMessage = "Purchase date time is invalid";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime purchaseDate)
        {
            if (purchaseDate.Date < DateTime.Today)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}