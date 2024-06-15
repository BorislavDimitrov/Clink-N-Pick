using ClickNPick.Application.Attributes;
using ClickNPick.Application.Common;
using ClickNPick.Application.Constants;
using ClickNPick.Application.Helpers;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ClickNPick.Application.DeliveryModels.Request
{
    public class RequestCourierRequestModel
    {
        [JsonProperty(
     PropertyName = "requestTimeFrom",
     Required = Required.Always)]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTime RequestTimeFrom { get; set; }

        [JsonProperty(
            PropertyName = "requestTimeTo",
            Required = Required.Always)]
        [JsonConverter(typeof(CustomDateTimeConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTime RequestTimeTo { get; set; }

        [JsonProperty(
            PropertyName = "shipmentType",
            Required = Required.Always)]
        [DefaultValue(ShipmentTypes.Pack)]
        [RequestSupportedValues<string>(
            ShipmentTypes.BigLetter,
            ShipmentTypes.Cargo,
            ShipmentTypes.Document,
            ShipmentTypes.DocumentPallet,
            ShipmentTypes.MoneyTransfer,
            ShipmentTypes.Pack,
            ShipmentTypes.Pallet,
            ShipmentTypes.PostPack,
            ShipmentTypes.PostTransfer,
            ShipmentTypes.SmallLetter)]
        public string ShipmentType { get; set; }

        [JsonProperty("shipmentPackCount")]
        public int? ShipmentPackCount { get; set; }

        [JsonProperty("shipmentWeight")]
        public double? ShipmentWeight { get; set; }

        [JsonProperty(
            PropertyName = "senderClient",
            Required = Required.Always)]
        public ClientProfile SenderClient { get; set; }

        [JsonProperty("senderAgent")]
        public ClientProfile? SenderAgent { get; set; }

        [JsonProperty(
            PropertyName = "senderAddress",
            Required = Required.Always)]
        public Address SenderAddress { get; set; }

        [JsonProperty("attachShipments")]
        public List<string>? AttachShipments { get; set; }
    }
}
