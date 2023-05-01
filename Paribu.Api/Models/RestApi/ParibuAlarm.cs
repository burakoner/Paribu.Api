namespace Paribu.Api.Models.RestApi;

public class ParibuAlarm
{
    [JsonProperty("uid")]
    public string Id { get; set; }

    [JsonProperty("user_uid")]
    public string UserId { get; set; }

    [JsonProperty("market")]
    public string Symbol { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("creation_price")]
    public decimal CreationPrice { get; set; }

    [JsonProperty("trigger_price")]
    public decimal TriggerPrice { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(LabelConverter<ParibuAlarmDirection>))]
    public ParibuAlarmDirection Direction { get; set; }
}