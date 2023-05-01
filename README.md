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
```

## Websocket Api Examples
The Paribu.Api socket client provides several socket endpoint to which can be subscribed.

**Core » Public Feeds**
```C#
...
```

## Release Notes
* Version 1.1.1 - 01 May 2023
    * Migrated to Paribu API V4 Endpoints

* Version 1.0.0 - 26 Mar 2023
    * First Release