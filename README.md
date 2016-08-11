# GladLive.ProfileService.ASP

GladLive is network session service comparable to Xboxlive or Steam. 

GladLive.ProfileService.ASP provides a web scalable profile webservice/web-api for the GladLive distributed network and preforms the role by providing:
  - Hello Implementation
  - Services Profile Queries
  - Implemented GladLive.Payload.Profile RequestControllers
  - Equipped to handle JWT authorization for elevated requests
  - Web and cloud ready

## GladLive Services

GladLive.PatchingService.ASP: https://github.com/GladLive/GladLive.PatchingService.ASP

GladLive.ProfileService.ASP: https://github.com/GladLive/GladLive.ProfileService.ASP

GladLive.AuthService.ASP: https://github.com/GladLive/GladLive.AuthService.ASP

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2015 RC 3
  - ASP Core VS Tools
  - Dotnet SDK
  - Add Nuget Feed https://www.myget.org/F/hellokitty/api/v2 in VS (Options -> NuGet -> Package Sources)

## How To Use

Start the application and connect to the {baseurl}/api/AuthenticationRequest endpoint to be issued a JWT authorization response. To authenticate you must send username, password and grant_type password in the request body of a POST with a url encoded content type.

For example: username=TestUsername&password=Test123&grant_type=password

The server will issue a JWT token in the response body or error information.

## Builds

(CD will be setup in the future; will not be available on NuGet)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.ProfileService.ASP.svg?branch=master)](https://travis-ci.org/GladLive/GladLive.ProfileService.ASP) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.ProfileService.ASP.svg?branch=dev)](https://travis-ci.org/GladLive/GladLive.ProfileService.ASP)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the MIT license with the additional requirement of adding the GladLive splashscreen to your product.
