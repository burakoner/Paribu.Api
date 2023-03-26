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
**Public Endpoints**
```C#
// Rest Api Client
var api = new ParibuClient();

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
```

## Websocket Api Examples
The Paribu.Api socket client provides several socket endpoint to which can be subscribed.

**Core » Public Feeds**
```C#
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
_ = ws.Unsubscribe(sub01.Data);
_ = ws.Unsubscribe(sub02.Data);
```

## Release Notes
* Version 1.0.0 - 26 Mar 2023
    * First Release