using System.ComponentModel.DataAnnotations;

namespace ClickNPick.Application.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RequestSupportedValuesAttribute<T> : ValidationAttribute
{
    private readonly T[] supportedValues;

    public RequestSupportedValuesAttribute(params T[] supportedValues)
        => this.supportedValues = supportedValues;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (supportedValues != null &&
            supportedValues.Any() &&
            value != null &&
            !supportedValues.Contains((T)value))
        {
            return new ValidationResult($"The value '{value}' is not allowed for the field '{validationContext.DisplayName}'. Allowed values are: {string.Join(", ", supportedValues)}.");
        }

        return ValidationResult.Success;
    }
}
