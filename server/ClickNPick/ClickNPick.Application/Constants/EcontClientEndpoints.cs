namespace ClickNPick.Application.Constants;

public static class EcontClientEndpoints
{
    // Nomenclatures Service
    private const string NomenclaturesService = "Nomenclatures/NomenclaturesService";

    public const string Cities = $"{NomenclaturesService}.getCities.json";
    public const string Quarters = $"{NomenclaturesService}.getQuarters.json";
    public const string Streets = $"{NomenclaturesService}.getStreets.json";

    // Address Service
    private const string AddressService = "Nomenclatures/AddressService";

    public const string ValidateAddress = $"{AddressService}.validateAddress.json";

    // Label Service
    private const string LabelService = "Shipments/LabelService";

    public const string CreateLabel = $"{LabelService}.createLabel.json";
    public const string DeleteLabels = $"{LabelService}.deleteLabels.json";

    // Shipment Service
    private const string ShipmentService = "Shipments/ShipmentService";

    public const string RequestCourier = $"{ShipmentService}.requestCourier.json";
    public const string GetShipmentStatuses = $"{ShipmentService}.getShipmentStatuses.json";
    public const string GetCourierStatuses = $"{ShipmentService}.getRequestCourierStatus.json";
}
