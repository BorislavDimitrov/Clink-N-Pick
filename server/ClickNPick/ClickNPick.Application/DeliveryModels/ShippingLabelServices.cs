using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class ShippingLabelServices
{
    [JsonProperty("priorityTimeFrom")]
    public string? PriorityTimeFrom { get; set; }

    [JsonProperty("priorityTimeTo")]
    public string? PriorityTimeTo { get; set; }

    [JsonProperty("deliveryReceipt")]
    public bool DeliveryReceipt { get; set; }

    [JsonProperty("digitalReceipt")]
    public bool DigitalReceipt { get; set; }

    [JsonProperty("goodsReceipt")]
    public bool GoodsReceipt { get; set; }

    [JsonProperty("twoWayShipment")]
    public bool TwoWayShipment { get; set; }

    [JsonProperty("deliveryToFloor")]
    public bool DeliveryToFloor { get; set; }

    [JsonProperty("pack5")]
    public int Pack5 { get; set; }

    [JsonProperty("pack6")]
    public int Pack6 { get; set; }

    [JsonProperty("pack8")]
    public int Pack8 { get; set; }

    [JsonProperty("pack9")]
    public int Pack9 { get; set; }

    [JsonProperty("pack10")]
    public int Pack10 { get; set; }

    [JsonProperty("pack12")]
    public int Pack12 { get; set; }

    [JsonProperty("refrigeratedPack")]
    public int RefrigeratedPack { get; set; }

    [JsonProperty("declaredValueAmount")]
    public double DeclaredValueAmount { get; set; }

    [JsonProperty("declaredValueCurrency")]
    public string? DeclaredValueCurrency { get; set; }

    [JsonProperty("moneyTransferAmount")]
    public double MoneyTransferAmount { get; set; }

    [JsonProperty("expressMoneyTransfer")]
    public bool? ExpressMoneyTransfer { get; set; }

    [JsonProperty("CdAmount")]
    public double CdAmount { get; set; }

    [JsonProperty("cdType")]
    public string? CdType { get; set; }

    [JsonProperty("cdCurrency")]
    public string? CdCurrency { get; set; }

    [JsonProperty("cdPayOptionsTemplate")]
    public string? CdPayOptionsTemplate { get; set; }

    [JsonProperty("cdPayOptions")]
    public CdPayOptions? CdPayOptions { get; set; }

    [JsonProperty("invoiceBeforePayCd")]
    public bool InvoiceBeforePayCd { get; set; }

    [JsonProperty("smsNotification")]
    public bool SmsNotification { get; set; }

    [JsonProperty("invoiceNum")]
    public string? InvoiceNum { get; set; }
}
