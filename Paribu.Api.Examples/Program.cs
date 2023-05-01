using Paribu.Api.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paribu.Api.Examples;

class Program
{
    static async Task Main(string[] args)
    {
        // Rest Api Client
        var api = new ParibuRestClient();

        // Public Endpoints
        var p01 = await api.GetHealthAsync();
        var p02 = await api.GetExchangeInformationAsync();
        var p03 = await api.GetTickersAsync();
        var p04 = await api.GetPriceSeriesAsync();
        var p05 = await api.GetOrderBookAsync("btc_tl");
        var p06 = await api.GetLatestMatchesAsync("btc_tl");
        var p07 = await api.GetKlinesAsync("btc_tl", ParibuKlineInterval.OneDay, 1654387200, 1682899200, 100);
        var p08 = await api.GetKlinesAsync("btc_tl", ParibuKlineInterval.OneDay, new DateTime(2022, 01, 01), DateTime.Now, 100);

        // Authentication (Login)
        var a01 = await api.LoginAsync("+90", "532XXXXXXX", "Pa55w0rd");
        var a02 = await api.LoginVerifyAsync(a01.Data.VerificationToken, "---CODE---");

        // Authentication (Existing Token)
        api.SetDeviceId("0123456789abcdef0123456789abcdef");
        api.SetAccessToken("0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcd");

        // Private Endpoints
        var x01 = await api.GetUserAccountAsync();
        var x02 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderSide.Sell, ParibuOrderType.Limit, 21.0m, null, 100.0m); // Limit Order
        var x03 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderSide.Sell, ParibuOrderType.Market, null, null, 100.0m); // Market Order, 100.00 USDT
        var x04 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderSide.Buy, ParibuOrderType.Market, null, null, null, 2100.0m); // Market Order, 2100.00 TL
        var x05 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderSide.Buy, ParibuOrderType.Limit, 21.5m, 21.0m, 100.0m); // Conditional Order
        var x06 = await api.GetOrderAsync("----ORDER-ID-----");
        var x07 = await api.GetOrdersAsync("usdt_tl");
        var x08 = await api.GetOrdersHistoryAsync(new List<string> { "buy", "sell" });
        var x09 = await api.GetOrdersHistoryAsync(new List<string> { "buy", "sell", "deposit", "withdraw" }, new List<string> { "tl", "usdt" });
        var x10 = await api.GetOrdersHistoryAsync(new List<string> { "buy", "sell", "deposit", "withdraw" }, new List<string> { "tl", "usdt" }, new DateTime(2022, 01, 01), DateTime.Now, 1, 25);
        var x11 = await api.CancelOrderAsync("----ORDER-ID-----");
        var x12 = await api.CancelOrdersAsync(new List<string> { "----ORDER-ID-01-----", "----ORDER-ID-02-----", "----ORDER-ID-03-----" });
        var x13 = await api.CancelOrdersAsync(new List<string> { "----ORDER-ID-01-----", "----ORDER-ID-02-----", "----ORDER-ID-03-----" });
        var x14 = await api.CancelAllOrdersAsync();

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}