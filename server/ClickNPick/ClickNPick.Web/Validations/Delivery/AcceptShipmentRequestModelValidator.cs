using ClickNPick.Web.Models.Delivery.Request;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClickNPick.Web.Validations.Delivery;

public class AcceptShipmentRequestModelValidator : AbstractValidator<AcceptShipmentRequestModel>
{
    public AcceptShipmentRequestModelValidator()
    {
        RuleFor(x => x.RequestShipmentId)
            .NotEmpty().WithMessage("RequestShipmentId is required.");

        RuleFor(x => x.SenderName)
            .NotEmpty().WithMessage("SenderName is required.")
            .MaximumLength(40).WithMessage("SenderName must be up to 40 characters long.");

        RuleFor(x => x.SenderPhoneNumber)
            .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s.\0-9]*$").WithMessage("Enter a valid phone number.");

        RuleFor(x => x.DeliveryLocation)
            .NotEmpty().WithMessage("DeliveryLocation is required.");

        RuleFor(x => x.PackCount)
            .NotEmpty().WithMessage("PackCount is required.");

        RuleFor(x => x.ShipmentType)
            .NotEmpty().WithMessage("ShipmentType is required.");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Weight is required.");

        RuleFor(x => x.ShipmentDescription)
           .NotEmpty().WithMessage("ShipmentDescription is required.");

        RuleFor(x => x.OrderNumber)
           .NotEmpty().WithMessage("OrderNumber is required.");

        RuleFor(x => x.PaymentReceiverAmount)
          .NotEmpty().WithMessage("PaymentReceiverAmount is required.")
          .InclusiveBetween(1, 50_000).WithMessage("PaymentReceiverAmount must be between 1 and 50,000");

        RuleFor(x => x.SendDate)
            .NotEmpty().When(x => x.DeliveryLocation == "Office").WithMessage("SendDate is required.");

        RuleFor(x => x.SenderOfficeCode)
            .NotEmpty().When(x => x.DeliveryLocation == "Office").WithMessage("SenderOfficeCode is required.");

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

        RuleFor(x => x.RequestTimeFrom)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("RequestTimeFrom is required.")
            .Must(x => x >= DateTime.Today).When(x => x.DeliveryLocation == "Address").WithMessage("RequestTimeFrom cannot be before todays date");

        RuleFor(x => x.RequestTimeTo)
            .NotEmpty().When(x => x.DeliveryLocation == "Address").WithMessage("RequestTimeTo is required.")
            .Must((model, requestTimeTo) => requestTimeTo > model.RequestTimeFrom).When(x => x.DeliveryLocation == "Address").WithMessage("RequestTimeTo cannot be smaller than RequestTimeFrom");
    }
}
