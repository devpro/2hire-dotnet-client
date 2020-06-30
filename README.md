# 2hire .NET Client

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/libraries/2hire-dotnet-client-ci?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=33&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.2hire.dotnetclient&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.2hire.dotnetclient)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.2hire.dotnetclient&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.2hire.dotnetclient)

.NET client for [2hire.io](https://2hire.io/) solution to manage a fleet of connected vehicles.

Package | Version | Type
------- | ------- | ----
`Devpro.Twohire.Abstractions` | [![Version](https://img.shields.io/nuget/v/Devpro.Twohire.Abstractions.svg)](https://www.nuget.org/packages/Devpro.Twohire.Abstractions/) | .NET Standard 2.1
`Devpro.Twohire.Client` | [![Version](https://img.shields.io/nuget/v/Devpro.Twohire.Client.svg)](https://www.nuget.org/packages/Devpro.Twohire.Client/) | .NET Standard 2.1

## How to use

- Have the [NuGet package](https://www.nuget.org/packages/Devpro.Twohire.Client) in your csproj file (can be done manually, with Visual Studio or through nuget command)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Devpro.Twohire.Client" Version="X.Y.Z" />
  </ItemGroup>
</Project>
```

- Make the code changes to be able to use the library (config & service provider)

```csharp
// implement the configuration interface (for instance in a configuration class in your app project) or use DefaultTwohireClientConfiguration
using Devpro.Twohire.Client;

public class AppConfiguration : ITwohireClientConfiguration
{
    // explicitely choose where to take the configuration for 2hire REST API (this is the responibility of the app, not the library)
}

// configure your service provider (for instance in your app Startup class)
using Devpro.Twohire.Client.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
  .AddLogging()
  .AddTwohireClient(Configuration);
```

- Use the repositories (enjoy a simple, yet optimized, HTTP client)

```csharp
using Devpro.Twohire.Abstractions.Repositories;

private readonly IPersonalVehicleRepository _personalVehicleRepository;

public MyService(IPersonalVehicleRepository personalVehicleRepository)
{
    _personalVehicleRepository = personalVehicleRepository;
}

public async Task GetVehicles()
{
    var vehicles = await _personalVehicleRepository.FindAllAsync();
}
```

## How to build

```bash
dotnet restore
dotnet build
```

## How to test

For integration tests, to manage the configuration (secrets) you can create a file at the root directory called `Local.runsettings` or define them as environment variables:

```xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <RunConfiguration>
    <EnvironmentVariables>
      <TwoHire__Sandbox__BaseUrl>xxx</TwoHire__Sandbox__BaseUrl>
      <TwoHire__Sandbox__ApiVersion>xxx</TwoHire__Sandbox__ApiVersion>
      <TwoHire__Sandbox__ServiceToken>xxx</TwoHire__Sandbox__ServiceToken>
      <TwoHire__Sandbox__Username>xxx</TwoHire__Sandbox__Username>
      <TwoHire__Sandbox__Password>xxx</TwoHire__Sandbox__Password>
    </EnvironmentVariables>
  </RunConfiguration>
</RunSettings>
```

And execute all tests (unit and integration ones):

```bash
dotnet test --settings Local.runsettings
```
