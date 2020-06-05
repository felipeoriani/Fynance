<div align="center">

![Fynance](https://raw.githubusercontent.com/felipeoriani/Fynance/master/logo.png)

</div>

<div align="center">

[![NuGet](https://img.shields.io/nuget/v/fynance.svg)](https://www.nuget.org/packages/Fynance)
[![GitHub Stars](https://img.shields.io/github/stars/felipeoriani/fynance.svg)](https://github.com/felipeoriani/fynance/stargazers)
[![GitHub Issues](https://img.shields.io/github/issues/felipeoriani/fynance.svg)](https://github.com/felipeoriani/fynance/issues)
[![Apache License](https://img.shields.io/github/license/felipeoriani/fynance.svg)](LICENSE)

</div>

Fynance is a handy wrapper to get stock market quotes written in .Net Standard. It is currently support the Yahoo Finance but it can be extended to other APIs.

## Installation - Nuget Package

This is available on [nuget package.](https://www.nuget.org/packages/Fynance).

```
Install-Package Fynance
```

## Features

- Get Security Info
- Get Quotes 
- Get Events
 - Get Dividends
 - Get Splits

Add the `Fynance` namespace:

```
using Fynance;
```

Then you can build an instance of `Ticker` and use it with fluent implementation uintil call the `Get` or `GetAsync` method.

```
var result = await Ticker.Build()
                         .SetSymbol("MSFT")
                         .SetPeriod(Period.OneMonth)
                         .SetInterval(Interval.OneDay)
                         .GetAsync();
```

Collab of @FelipeOriani and @EduVencovsky
