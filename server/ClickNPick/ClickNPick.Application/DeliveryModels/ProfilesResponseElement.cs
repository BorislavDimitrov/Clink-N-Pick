using Newtonsoft.Json;

namespace ClickNPick.Application.Common;

public class ProfilesResponseElement
{
    [JsonProperty("client")]
    public ClientProfile ClientProfile { get; set; }

    [JsonProperty("addresses")]
    public IEnumerable<Address> Address { get; set; }

    [JsonProperty("cdPayOptions")]
    public IEnumerable<CdPayOptions> CdPayOptions { get; set; }

    [JsonProperty("instructionTemplates")]
    public IEnumerable<Instruction> InstructionTemplates { get; set; }
}
