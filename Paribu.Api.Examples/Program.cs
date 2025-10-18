using Paribu.Api.Enums;
using System;
using System.Threading.Tasks;

namespace Paribu.Api.Examples;

class Program
{
    static async Task Main(string[] args)
    {
        // Rest Api Client
        var api = new ParibuRestClient();

        // Public Endpoints
        var p01 = await api.PingAsync();
        var p02 = await api.GetExchangeInformationAsync();
        var p03 = await api.GetTickersAsync();
        var p04 = await api.GetPublicTradesAsync("---SYMBOL---");
        var p05 = await api.GetOrderBookAsync("---SYMBOL---");
        var p06 = await api.GetOrderBookAsync("---SYMBOL---", 20);
        var p07 = await api.GetKlinesAsync("btc_tl", ParibuKlineInterval.OneDay);

        // Private Endpoints
        api.SetApiCredentials("-----PUBLIC-KEY-----", "-----SECRET-KEY-----");
        var p11 = await api.GetUserInformationAsync();
        var p12 = await api.GetBalancesAsync();
        var p13 = await api.GetTransfersAsync();
        var p14 = await api.GetTransfersAsync(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
        var p15 = await api.GetUserTradesAsync();
        var p16 = await api.GetUserTradesAsync("---SYMBOL---", ParibuOrderSide.Buy, DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
        var p17 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderType.Limit, ParibuOrderSide.Sell, amount: 100.0m, price: 40.0m); // Limit Order
        var p18 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderType.Market, ParibuOrderSide.Sell, amount: 100.0m); // Market Order, 100.00 USDT
        var p19 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderType.Market, ParibuOrderSide.Buy, amount: null, total: 2100.0m, null, null); // Market Order, 2100.00 TL
        var p20 = await api.PlaceOrderAsync("usdt_tl", ParibuOrderType.Limit, ParibuOrderSide.Buy, amount: 100.0m, price: 39.0m, condition: 40.0m); // Conditional Order
        var p21 = await api.GetOrderAsync("---ORDER-ID---");
        var p22 = await api.CancelOrderAsync("---ORDER-ID---");
        var p23 = await api.GetOpenOrderAsync();
        var p24 = await api.GetOpenOrderAsync("---SYMBOL---");
        var p25 = await api.GetDepositAddressAsync("btc","btc");
        var p26 = await api.GetDepositAddressAsync("usdc", "eth");

        // Web Socket Client
        var wss = new ParibuStreamClient();

        // Ticker Subscriptions
        var sub01 = await wss.SubscribeToTickerAsync("usdt_tl", (data) =>
        {
            Console.WriteLine($"Ticker >> {data.Symbol} " +
                $"O:{data.First} " +
                $"H:{data.High} " +
                $"L:{data.Low} " +
                $"C:{data.Last} " +
                $"V:{data.Volume} " +
                $"CH:{data.Change} " +
                $"CP:{data.Percentage} " +
                $"AVG:{data.Average} "
                );
        });
        var sub02 = await wss.SubscribeToTickerAsync(["usdt_tl", "btc_tl", "eth_tl", "usdc_tl", "xrp_tl", "doge_tl", "chz_tl", "avax_tl"], (data) =>
        {
            Console.WriteLine($"Ticker >> {data.Symbol} " +
                $"O:{data.First} " +
                $"H:{data.High} " +
                $"L:{data.Low} " +
                $"C:{data.Last} " +
                $"V:{data.Volume} " +
                $"CH:{data.Change} " +
                $"CP:{data.Percentage} " +
                $"AVG:{data.Average} "
                );
        });

        // Order Book Difference (Delta) Subscriptions
        var sub03 = await wss.SubscribeToOrderBookAsync("usdt_tl", (data) =>
        {
            Console.WriteLine($"Order Book >> {data.Symbol} " +
                $"Asks:{data.Asks.Count} " +
                $"Bids:{data.Bids.Count} " +
                $"UpdateId:{data.UpdateId} "
                );
        });
        var sub04 = await wss.SubscribeToOrderBookAsync(["usdt_tl", "btc_tl", "eth_tl", "usdc_tl", "xrp_tl", "doge_tl", "chz_tl", "avax_tl",], (data) =>
        {
            Console.WriteLine($"Order Book >> {data.Symbol} " +
                $"Asks:{data.Asks.Count} " +
                $"Bids:{data.Bids.Count} " +
                $"UpdateId:{data.UpdateId} "
                );
        });

        // Unsubscribe
        await wss.UnsubscribeAsync(sub01.Data);
        await wss.UnsubscribeAsync(sub02.Data);
        await wss.UnsubscribeAsync(sub03.Data);
        await wss.UnsubscribeAsync(sub04.Data);
        await wss.UnsubscribeAllAsync();

        // Done
        Console.WriteLine("Done");
        Console.ReadLine();
    }
}