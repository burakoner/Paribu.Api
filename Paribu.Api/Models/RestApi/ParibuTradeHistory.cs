namespace Paribu.Api.Models.RestApi;

public record ParibuTradeHistory
{
    [JsonProperty("paging")]
    public ParibuPaging Paging { get; set; } = default!;

    [JsonProperty("trades")]
    public List<ParibuTrade> Trades { get; set; } = [];
}

public record ParibuTrade
{
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("commission")]
    public decimal Commission { get; set; }

    [JsonProperty("createdAt"), JsonConverter(typeof(ParibuDateTimeConverter))]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("direction")]
    public ParibuOrderSide Direction { get; set; }

    [JsonProperty("marketCurrency")]
    public string BaseAsset { get; set; } = "";

    [JsonProperty("paymentCurrency")]
    public string QuoteAsset { get; set; } = "";

    public string Symbol { get => BaseAsset + "_" + QuoteAsset; }

    [JsonProperty("orderId")]
    public string OrderId { get; set; } = "";

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("role")]
    public ParibuTraderRole Role { get; set; }

    [JsonProperty("userId")]
    public string UserId { get; set; } = "";
}