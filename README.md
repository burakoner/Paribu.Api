# Paribu.Api 

A .Net wrapper for the Paribu API, including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/burakoner/Paribu.Api/issues)**

## Donations
Donations are greatly appreciated and a motivation to keep improving.

**BTC**:  33WbRKqt7wXARVdAJSu1G1x3QnbyPtZ2bH  
**ETH**:  0x65b02db9b67b73f5f1e983ae10796f91ded57b64  
**USDT (TRX)**: TXwqoD7doMESgitfWa8B2gHL7HuweMmNBJ  

## Installation
![Nuget version](https://img.shields.io/nuget/v/Paribu.Api.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/Paribu.Api.svg)
Available on [Nuget](https://www.nuget.org/packages/Paribu.Api).
```
PM> Install-Package Paribu.Api
```
To get started with Paribu.Api first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/Paribu.Api). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type 'Paribu.Api' and hit enter. The Paribu.Api package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use Paribu.Api in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that Paribu.Api will be installed in. After selecting the correct project type  `Install-Package Paribu.Api`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using Paribu.Api.
## Getting started
After installing it's time to actually use it. To get started we have to add the Paribu.Api namespace:  `using Paribu.Api;`.

Paribu.Api provides two clients to interact with the Paribu API. The  `ParibuRestClient`  provides all rest API calls. The  `ParibuStreamClient` provides functions to interact with the websocket provided by the Paribu API. Both clients are disposable and as such can be used in a  `using`statement.

## Rest Api Examples

```C#
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
```

## Websocket Api Examples

```C#
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
```

## Release Notes
* Version 2.0.0 - 18 Oct 2025
    * Implemented Paribu Official API as described in [Paribu API Documentation](https://docs.paribu.com/api)

* Version 1.1.1 - 01 May 2023
    * Migrated to Paribu API V4 Endpoints

* Version 1.0.0 - 26 Mar 2023
    * First Release