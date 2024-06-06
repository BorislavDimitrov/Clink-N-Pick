using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ClickNPick.Application.Configurations.Cache;
using Newtonsoft.Json;

namespace ClickNPick.Web.Models.Delivery.Request;

public class GetCitiesRequestModel : ICacheable
{
    [DefaultValue("BGR")]
    [JsonProperty("countryCode")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "You must enter three-letter ISO Alpha-3 code of the country (e.g. AUT, BGR, GRC, etc.)")]
    public string? CountryCode { get; set; }

    public IEnumerable<CacheParameter> GetCacheParameters()
        => CachePropertyExtractor.GetCacheParameters(this);

    public GetCitiesRequestDto ToGetCitiesRequestDto()
    {
        return new GetCitiesRequestDto
        {
            CountryCode = this.CountryCode,
        };
    }
}

