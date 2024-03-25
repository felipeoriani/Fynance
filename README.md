<div align="center">

![Fynance](https://raw.githubusercontent.com/felipeoriani/Fynance/master/logo.png)

</div>

<div align="center">

[![Build Status](https://img.shields.io/github/workflow/status/felipeoriani/Fynance/Build)](https://img.shields.io/github/workflow/status/felipeoriani/Fynance/Build)

[![NuGet](https://img.shields.io/nuget/v/fynance.svg)](https://www.nuget.org/packages/Fynance)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Fynance.svg)](https://www.nuget.org/packages/Fynance)
[![GitHub Stars](https://img.shields.io/github/stars/felipeoriani/fynance.svg)](https://github.com/felipeoriani/fynance/stargazers)
[![GitHub Issues](https://img.shields.io/github/issues/felipeoriani/fynance.svg)](https://github.com/felipeoriani/fynance/issues)
[![Apache License](https://img.shields.io/github/license/felipeoriani/fynance.svg)](LICENSE)
[![Donate](https://img.shields.io/badge/Donate-PayPal-blue.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=BRLZR87XSBQT8&source=url)

</div>

Fynance is a handy wrapper to get stock market quotes written in .Net Standard. It is currently support the Yahoo Finance but it can be extended to other APIs.

## Installation - Nuget Package

This is available on [nuget package](https://www.nuget.org/packages/Fynance).

```
Install-Package Fynance
```

To update the package to the last version use the following nuget statement:

```
Update-Package Fynance
``` 

## Fynance Examples

What data you can get with `Fynance`:

- Get Security Info
- Get Quotes 
- Get Events
  - Get Dividends
  - Get Splits

#### General Usage 

First you have to add the `Fynance` namespace to make the types available on your code:

```c#
using Fynance;
```

The *Fynance* implements a fluent interface to read data from a stock market api. Given it, a instance of `Ticker` is a representation of a security on the stock market. Then you can make an instance of `Ticker` and use all the methods to configure what you want to have until call the `Get` method. There is a `async` version of this method called `GetAsync`. The following code is a sample of use:

```c#
var result = await Ticker.Build()
                         .SetSymbol("MSFT")
                         .SetPeriod(Period.OneMonth)
                         .SetInterval(Interval.OneDay)
                         .GetAsync();
```

The `Build` method instance the `Ticker` object and all these `Set` methods configure what information you want to get from the available APIs.

To the the *events* as we call *dividends* or *splits* you can use `SetDividends` and `SetEvents` to define it:
```c#
var result = await Ticker.Build()
                         .SetSymbol("MSFT")
                         .SetPeriod(Period.OneMonth)
                         .SetInterval(Interval.OneDay)
                         .SetDividends(true)
                         .SetEvents(true)
                         .GetAsync();
```

Alternatively, you can use the method `.SetEvents(true)` and it will set `Dividends` and `Splits`.

When you call the `Get` methods, you get a instance of `FyResult`. It contains all the data following the methods configured over the `Ticker`. The `FyResult` can contain all the information from the *Security*, the *OLHC* history, dividends and splits.

##### Results

There are many information you can read from the security, see some samples available:

```c#
var currenty = result.Currency;
var symbol = result.Symbol;
var symbol = result.ExchangeName;
```
   
Reading all the *OLHC* history:

```c#
foreach (var item in result.Quotes)
{
   var period = item.Period; // DateTime
   var open = item.Open; // Decimal
   var low = item.Low; // Decimal
   var high = item.High; // Decimal 
   var close = item.Close; // Decimal 
   var close = item.Close; // Decimal 
   var adjClose = item.AdjClose; // Decimal 
   var volume = item.Volume; // Decimal
}
```

Reading the dividends:

```c#
foreach (var item in result.Dividends)
{
   var date = item.Date; // DateTime: Payment date 
   var value = item.value; // Decimal: Payment value
}
```

Reading the splits:

```c#
foreach (var item in result.Splits)
{
   var date = item.Date; // DateTime: Event Date 
   var numerator = item.Numberator; // Decimal: Numerator of splits
   var denominator = item.Denominator; // Decimal: Denominator of split
}
```


### Issue Reports

If you find any issue, please report them using the [GitHub issue tracker](https://github.com/felipeoriani/Fynance/issues). Would be great if you provide a sample of code or a repository with a sample project.

### Contributions

Contributions are welcome, feel free to fork the repository and send a pull request with your changes to contribute with the package.

### Licenses

This software is distributed under the terms of the *Apache License 2.0*. Please, read the [LICENSE](https://github.com/felipeoriani/Fynance/blob/master/LICENSE) file available on this repository.

### Credits

This project is a collab of [@FelipeOriani](https://github.com/felipeoriani/) and [@EduVencovsky](https://github.com/eduvencovsky/).

Many thanks for the [Newtonsoft.Json](https://www.newtonsoft.com/json) packaged used as a dependency of this project.

If you wanna colaborate with this project or it was useful to you, make a donate:

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=BRLZR87XSBQT8&source=url)
