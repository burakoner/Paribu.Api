using Paribu.Api.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Paribu.Api.Examples;

class Program
{
    static async Task Main(string[] args)
    {
        /* Rest Api Client */
        var api = new ParibuRestClient();
        api.SetDeviceId(Guid.NewGuid().ToString());

        /* Public Endpoints */
        var p01 = await api.GetInitialsAsync();
        var p02 = await api.GetTickersAsync();
        var p03 = await api.GetMarketDataAsync("btc-tl");
        var p04 = await api.GetChartDataAsync("btc-tl");
        var p05 = await api.RegisterAsync("John Doe", "a@b.com", "532XXXXXXX", "Pa55w0rd");
        var p06 = await api.RegisterTwoFactorAsync(p05.Data.Token, "---CODE---");
        var p07 = await api.LoginAsync("532XXXXXXX", "Pa55w0rd");
        var p08 = await api.LoginTwoFactorAsync(p07.Data.Token, "---CODE---");
        api.SetAccessToken(p08.Data.Token);

        /* Private Endpoints */
        var p11 = await api.GetUserInitialsAsync();
        var p12 = await api.GetOpenOrdersAsync();
        var p13 = await api.PlaceOrderAsync("usdt-tl", OrderSide.Sell, OrderType.Limit, 110.0m, 10.0m, 11.0m);
        var p14 = await api.CancelOrderAsync("j1kwxq9l-eyr6-7yzg-ogkd-6gp843dzvn5o");
        var p15 = await api.CancelOrdersAsync("usdt-tl");
        var p16 = await api.CancelOrdersAsync("all");
        var p21 = await api.GetAlertsAsync();
        var p22 = await api.SetAlertAsync("usdt-tl", 9.25m);
        var p23 = await api.SetAlertAsync("usdt-tl", 10.25m);
        var p24 = await api.SetAlertAsync("btc-tl", 620000m);
        var p25 = await api.SetAlertAsync("btc-tl", 660000m);
        var p26 = await api.CancelAlertAsync("1z4r65mv-qe3l-29oj-l40d-278ydpnxj90g");
        var p27 = await api.CancelAlertsAsync("eth-tl");
        var p28 = await api.CancelAlertsAsync("all");
        var p31 = await api.GetBalancesAsync();
        var p32 = await api.GetDepositAddressesAsync();
        var p33 = await api.WithdrawAsync("tl", 1000.0m, "---IBAN---");
        var p34 = await api.WithdrawAsync("usdt", 100.0m, "---USDT-ADDRESS---", "", "trx");
        var p35 = await api.CancelWithdrawalAsync(p34.Data.Id);

        /* Web Socket Client */
        var ws = new ParibuStreamClient();

        /* Tickers */
        var sub01 = await ws.SubscribeToTickersAsync((data) =>
        {
            if (data != null)
            {
                Console.WriteLine($"Ticker State >> {data.Symbol} " +
                    (data.Open.HasValue ? $"O:{data.Open} " : "") +
                    (data.High.HasValue ? $"H:{data.High} " : "") +
                    (data.Low.HasValue ? $"L:{data.Low} " : "") +
                    (data.Close.HasValue ? $"C:{data.Close} " : "") +
                    (data.Volume.HasValue ? $"V:{data.Volume} " : "") +
                    (data.Change.HasValue ? $"CH:{data.Change} " : "") +
                    (data.ChangePercent.HasValue ? $"CP:{data.ChangePercent} " : "") +
                    (data.Average24H.HasValue ? $"Avg:{data.Average24H} " : "") +
                    (data.VolumeQuote.HasValue ? $"G:{data.VolumeQuote} " : "") +
                    (data.Bid.HasValue ? $"Bid:{data.Bid} " : "") +
                    (data.Ask.HasValue ? $"Ask:{data.Ask} " : "") +
                    (data.EBid.HasValue ? $"EBid:{data.EBid} " : "") +
                    (data.EAsk.HasValue ? $"EAsk:{data.EAsk} " : "")
                    );
            }
        }, (data) =>
        {
            if (data != null)
            {
                Console.WriteLine($"Ticker Prices >> {data.Symbol} C:{data.Prices.Count()} P:{string.Join(',', data.Prices)}");
            }
        });

        /* Order Book & Trades */
        var sub02 = await ws.SubscribeToMarketDataAsync("btc-tl", (data) =>
        {
            if (data != null)
            {
                Console.WriteLine($"Book Update >> {data.Symbol} " +
                    $"AsksToAdd:{data.AsksToAdd.Count} " +
                    $"BidsToAdd:{data.BidsToAdd.Count} " +
                    $"AsksToRemove:{data.AsksToRemove.Count} " +
                    $"BidsToRemove:{data.BidsToRemove.Count} "
                    );
            }
        }, (data) =>
        {
            if (data != null)
            {
                Console.WriteLine($"New Trade >> {data.Symbol} T:{data.Timestamp} P:{data.Price} A:{data.Amount} S:{data.Side}");
            }
        });

        // Unsubscribe
        // _ = ws.UnsubscribeAsync(sub01.Data);
        // _ = ws.UnsubscribeAsync(sub02.Data);

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}