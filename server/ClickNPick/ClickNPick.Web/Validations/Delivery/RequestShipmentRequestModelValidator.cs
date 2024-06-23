using ClickNPick.Web.Models.Delivery.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Delivery;

public class RequestShipmentRequestModelValidator : AbstractValidator<RquestShipmentRequestModel>
{
    public RequestShipmentRequestModelValidator()
    {
        RuleFor(x => x.ReceiverName)
            .NotEmpty().WithMessage("ReceiverName is required.")
            .MaximumLength(40).WithMessage("ReceiverName must be up to 40 characters long.");

        RuleFor(x => x.ReceiverPhoneNumber)
           .NotEmpty().WithMessage("ReceiverPhoneNumber is required.")
           .MaximumLength(15).WithMessage("ReceiverPhoneNumber must be up to 15 characters long.");

        RuleFor(x => x.EmailOnDelivery)
           .NotEmpty().WithMessage("EmailOnDelivery is required.")
           .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(x => x.DeliveryLocation)
            .NotEmpty().WithMessage("DeliveryLocation is required.");

        RuleFor(x => x.ReceiverOfficeCode)
            .NotEmpty().When(x => x.DeliveryLocation == "Office").WithMessage("ReceiverOfficeCode is required.");

        RuleFor(x => x.CityOrVillage)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("CityOrVillage is required.");

        RuleFor(x => x.PostCode)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("PostCode is required.");

        RuleFor(x => x.Quarter)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("Quarter is required.");

        RuleFor(x => x.Street)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("Street is required.");

        RuleFor(x => x.StreetNumber)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("StreetNumber is required.");

        RuleFor(x => x.DeliverAddressInfo)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("DeliverAddressInfo is required.");
    }
}
