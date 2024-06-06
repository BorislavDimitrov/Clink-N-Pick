using ClickNPick.Application.Helpers;
using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class ShippingLabel
{
    [JsonProperty("shipmentNumber")]
    public string? ShipmentNumber { get; set; }

    [JsonProperty("previousShipmentNumber")]
    public string? PreviousShipmentNumber { get; set; }

    [JsonProperty("previousShipmentReceiverPhone")]
    public string? PreviousShipmentReceiverPhone { get; set; }

    [JsonProperty("senderClient")]
    public ClientProfile? SenderClient { get; set; }

    [JsonProperty("senderAgent")]
    public ClientProfile? SenderAgent { get; set; }

    [JsonProperty("senderAddress")]
    public Address? SenderAddress { get; set; }

    [JsonProperty("senderOfficeCode")]
    public string? SenderOfficeCode { get; set; }

    [JsonProperty("emailOnDelivery")]
    public string? EmailOnDelivery { get; set; }

    [JsonProperty("smsOnDelivery")]
    public string? SmsOnDelivery { get; set; }

    [JsonProperty("receiverClient")]
    public ClientProfile? ReceiverClient { get; set; }

    [JsonProperty("receiverAgent")]
    public ClientProfile? ReceiverAgent { get; set; }

    [JsonProperty("receiverAddress")]
    public Address? ReceiverAddress { get; set; }

    [JsonProperty("receiverOfficeCode")]
    public string? ReceiverOfficeCode { get; set; }

    [JsonProperty("receiverProviderId")]
    public int? ReceiverProviderId { get; set; }

    [JsonProperty("receiverBic")]
    public string? ReceiverBic { get; set; }

    [JsonProperty("receiverIban")]
    public string? ReceiverIban { get; set; }

    [JsonProperty("envelopeNumbers")]
    public List<string>? EnvelopeNumbers { get; set; }

    [JsonProperty("packCount")]
    public int? PackCount { get; set; }

    [JsonProperty("packs")]
    public List<PackElement>? Packs { get; set; }

    [JsonProperty("shipmentType")]
    public string? ShipmentType { get; set; }

    [JsonProperty("weight")]
    public double? Weight { get; set; }

    [JsonProperty("sizeUnder60Cm")]
    public bool? SizeUnder60Cm { get; set; }

    [JsonProperty("shipmentDimensionsL")]
    public double? ShipmentDimensionsL { get; set; }

    [JsonProperty("shipmentDimensionsW")]
    public double? ShipmentDimensionsW { get; set; }

    [JsonProperty("shipmentDimensionsH")]
    public double? ShipmentDimensionsH { get; set; }

    [JsonProperty("shipmentDescription")]
    public string? ShipmentDescription { get; set; }

    [JsonProperty("orderNumber")]
    public string? OrderNumber { get; set; }

    [JsonProperty("sendDate")]
    [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd")]
    public DateTime? SendDate { get; set; }

    [JsonProperty("holidayDeliveryDay")]
    public string? HolidayDeliveryDay { get; set; }

    [JsonProperty("keepUpright")]
    public bool? KeepUpright { get; set; }

    [JsonProperty("services")]
    public ShippingLabelServices? Services { get; set; }

    [JsonProperty("instructions")]
    public List<Instruction>? Instructions { get; set; }

    [JsonProperty("payAfterAccept")]
    public bool? PayAfterAccept { get; set; }

    [JsonProperty("payAfterTest")]
    public bool? PayAfterTest { get; set; }

    [JsonProperty("packingListType")]
    public string? PackingListType { get; set; }

    [JsonProperty("packingList")]
    public List<PackingListElement>? PackingList { get; set; }

    [JsonProperty("partialDelivery")]
    public bool? PartialDelivery { get; set; }

    [JsonProperty("paymentSenderMethod")]
    public string? PaymentSenderMethod { get; set; }

    [JsonProperty("paymentReceiverMethod")]
    public string? PaymentReceiverMethod { get; set; }

    [JsonProperty("paymentReceiverAmount")]
    public double? PaymentReceiverAmount { get; set; }

    [JsonProperty("paymentReceiverAmountIsPercent")]
    public bool? PaymentReceiverAmountIsPercent { get; set; }

    [JsonProperty("paymentOtherClientNumber")]
    public string? PaymentOtherClientNumber { get; set; }

    [JsonProperty("paymentOtherAmount")]
    public double? PaymentOtherAmount { get; set; }

    [JsonProperty("paymentOtherAmountIsPercent")]
    public bool? PaymentOtherAmountIsPercent { get; set; }

    [JsonProperty("mediator")]
    public string? Mediator { get; set; }

    [JsonProperty("paymentToken")]
    public string? PaymentToken { get; set; }

    [JsonProperty("customsList")]
    public List<CustomsListElement>? CustomsList { get; set; }

    [JsonProperty("customsInvoice")]
    public string? CustomsInvoice { get; set; }
}
