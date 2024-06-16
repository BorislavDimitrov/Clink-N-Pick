using ClickNPick.Application.DeliveryModels.Request;
using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class ShipmentStatus
{
    [JsonProperty("shipmentNumber")]
    public string? ShipmentNumber { get; set; }

    [JsonProperty("storageOfficeName")]
    public string? StorageOfficeName { get; set; }

    [JsonProperty("storagePersonName")]
    public string? StoragePersonName { get; set; }

    [JsonProperty("createdTime")]
    public long CreatedTime { get; set; }

    [JsonProperty("sendTime")]
    public long SendTime { get; set; }

    [JsonProperty("deliveryTime")]
    public long DeliveryTime { get; set; }

    [JsonProperty("shipmentType")]
    public string? ShipmentType { get; set; }

    [JsonProperty("packCount")]
    public int? PackCount { get; set; }

    [JsonProperty("shipmentDescription")]
    public string? ShipmentDescription { get; set; }

    [JsonProperty("weight")]
    public double? Weight { get; set; }

    [JsonProperty("senderDeliveryType")]
    public string? SenderDeliveryType { get; set; }

    [JsonProperty("senderClient")]
    public ClientProfile? SenderClient { get; set; }

    [JsonProperty("senderAgent")]
    public ClientProfile? SenderAgent { get; set; }

    [JsonProperty("senderOfficeCode")]
    public string? SenderOfficeCode { get; set; }

    [JsonProperty("senderAddress")]
    public Address? SenderAddress { get; set; }

    [JsonProperty("receiverDeliveryType")]
    public string? ReceiverDeliveryType { get; set; }

    [JsonProperty("receiverClient")]
    public ClientProfile? ReceiverClient { get; set; }

    [JsonProperty("receiverAgent")]
    public ClientProfile? ReceiverAgent { get; set; }

    [JsonProperty("receiverOfficeCode")]
    public string? ReceiverOfficeCode { get; set; }

    [JsonProperty("receiverAddress")]
    public Address? ReceiverAddress { get; set; }

    [JsonProperty("cdCollectedAmount")]
    public double? CdCollectedAmount { get; set; }

    [JsonProperty("cdCollectedCurrency")]
    public string? CdCollectedCurrency { get; set; }

    [JsonProperty("cdCollectedTime")]
    public long CdCollectedTime { get; set; }

    [JsonProperty("cdPaidAmount")]
    public double? CdPaidAmount { get; set; }

    [JsonProperty("cdPaidCurrency")]
    public string? CdPaidCurrency { get; set; }

    [JsonProperty("cdPaidTime")]
    public long CdPaidTime { get; set; }

    [JsonProperty("totalPrice")]
    public double? TotalPrice { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("discountPercent")]
    public double? DiscountPercent { get; set; }

    [JsonProperty("discountAmount")]
    public double? DiscountAmount { get; set; }

    [JsonProperty("discountDescription")]
    public string? DiscountDescription { get; set; }

    [JsonProperty("senderDueAmount")]
    public double? SenderDueAmount { get; set; }

    [JsonProperty("receiverDueAmount")]
    public double? ReceiverDueAmount { get; set; }

    [JsonProperty("otherDueAmount")]
    public double? OtherDueAmount { get; set; }

    [JsonProperty("deliveryAttemptCount")]
    public int? DeliveryAttemptCount { get; set; }

    [JsonProperty("previousShipmentNumber")]
    public string? PreviousShipmentNumber { get; set; }

    [JsonProperty("services")]
    public List<ShipmentStatusService>? Services { get; set; }

    [JsonProperty("lastProcessedInstruction")]
    public string? LastProcessedInstruction { get; set; }

    [JsonProperty("nextShipments")]
    public List<NextShipmentElement>? NextShipments { get; set; }

    [JsonProperty("trackingEvents")]
    public List<ShipmentTrackingEvent>? TrackingEvents { get; set; }

    [JsonProperty("pdfURL")]
    public string? PdfUrl { get; set; }

    [JsonProperty("expectedDeliveryDate")]
    public long ExpectedDeliveryDate { get; set; }

    [JsonProperty("returnShipmentURL")]
    public string? ReturnShipmentUrl { get; set; }

    [JsonProperty("rejectOriginalParcelPaySide")]
    public string? RejectOriginalParcelPaySide { get; set; }

    [JsonProperty("rejectReturnParcelPaySide")]
    public string? RejectReturnParcelPaySide { get; set; }

    [JsonProperty("previousShipment")]
    public PreviousShipment? PreviousShipment { get; set; }
}