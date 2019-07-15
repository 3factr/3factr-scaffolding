﻿# 3factr scaffolding

Extends upon [https://github.com/Plac3hold3r/MvxScaffolding](MvxScaffolding)

__Templates features__

 |                                                     Features                                                      | mvxnative | mvxforms |
 | :---------------------------------------------------------------------------------------------------------------: | :-------: | :------: |
 |           [.NET Standard class library](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)            |     *     |    *     |
 | [Package references](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files) |     *     |    *     |
 |                                                Unit test projects                                                 |     *     |    *     |
 |                                                 UI test projects                                                  |     *     |    *     |
 | Solution [EditorConfig](https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options)  |     *     |    *     |
 |                                            Android Nougat round icons                                             |     *     |    *     |
 |                                            Android Oreo adaptive icons                                            |     *     |    *     |
 |                                         Android support constraint layout                                         |     *     |
 |                                            Android AXML or XML layout                                             |     *     |
 |                                           iOS storyboard launch screen                                            |     *     |    *     |
 |             iOS storyboard or [FluentLayouts](https://github.com/FluentLayout/Cirrious.FluentLayout)              |     *     |


## Installation

 |        Platform         |      Installation and System Requirements      |                Documentation                 |                                                  Download                                                   |
 | :---------------------: | :--------------------------------------------: | :------------------------------------------: | :---------------------------------------------------------------------------------------------------------: |
 |       dotnet CLI        |       [Installation Guide](#dotnet-cli)        | [Documentation](docs/template_dotnet_cli.md) |                      [NuGet](https://www.nuget.org/packages/MvxScaffolding.Templates/)                      |

## dotnet CLI

### System Requirements

In order to make use of these templates you will need to have the following installed for Windows or macOS

__Required__

 * .NET Core SDK 2.1.4+ ([Download SDK](https://www.microsoft.com/net/download))

 __Optional__ 

 * Xamarin Android SDK _(Recommended version 9.2+)_
 * Xamarin iOS SDK _(Recommended version 12.8+)_
 * UWP SDK _(__Windows Only__, recommended  version 10.0.17763+)_

### Installation

To install the template run the `-i|--install` command

```text
dotnet new --install MvxScaffolding.Templates
```

### Usage

See [documentation](docs/template_dotnet_cli.md) for how to generate a solution.

## License


[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FPlac3hold3r%2FMvxScaffolding.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2FPlac3hold3r%2FMvxScaffolding?ref=badge_large)

##### Third party libraries
- [MvvmCross](https://github.com/MvvmCross/MvvmCross) is licensed under [MS-PL](https://github.com/MvvmCross/MvvmCross/blob/master/LICENSE)
- [FluentLayout](https://github.com/FluentLayout/Cirrious.FluentLayout) is licensed under [MS-PL](https://github.com/FluentLayout/Cirrious.FluentLayout/blob/master/LICENSE)
- [Xamarin Android Support Library](https://github.com/xamarin/AndroidSupportComponents/) is licensed under [MIT](https://github.com/xamarin/AndroidSupportComponents/blob/master/LICENSE.md)

MvxScaffolding is licensed under [MIT](https://github.com/Plac3hold3r/MvxScaffolding/blob/master/LICENSE)