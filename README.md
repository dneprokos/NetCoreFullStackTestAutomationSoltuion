# NetCoreFullStackTestAutomationSoltuion

## Summary

Rest API and Selenium 4 projects on .CORE 3.1

## Prerequisites

- IDE (integrated development environment) to work with the C# e.g. Visual Studio

## Installation

- Pull git solution
- Build solution from IDE (e.g. in Visual Studio) or run the following command in command line: "dotnet build"

![image](https://user-images.githubusercontent.com/8307892/204016058-188ca0de-8cf7-4045-9955-f9282a60021e.png)


##How to run tests

### From Visual Studio


- In VIsual studio, Open root of the project tests you want to run, e.g. "JsonPlaceholder.Api.Tests" for API tests
- Select .runsettings file you want to run with, on top navigation menu -> "Test" -> "Configure Run Settings" --> Select file from the project root 
![image](https://user-images.githubusercontent.com/8307892/204016602-731f47b9-5908-49ea-8b0a-9284181c0c24.png)

- Open Test Explorer window, on top navigation menu -> "Test" -> "Test Explorer"

- Choice test you want to run or all tests scope

- Right button context menu and click run

![image](https://user-images.githubusercontent.com/8307892/204017362-f4ba16a8-4a69-4354-97ac-67c2ea2e5994.png)


### From command line

- Open RestApiTestsOnDotNetCore3_1 test project root folder
- Open CMD for this folder (or shell command line in Linux)
- Type "dotnet test -s "./env.runsettings"", where -s is a location of .runsettings test configuration file

![image](https://user-images.githubusercontent.com/8307892/204018296-5d3a54b5-e446-405c-ad02-ae23f30696b6.png)

![image](https://user-images.githubusercontent.com/8307892/204018354-164c1158-a3c3-4738-8326-5c4e515b0cbf.png)

## How to generate Allure report

### Prerequisites

- Report generation requires local Docker installation https://docs.docker.com/get-docker/
- Test it typing "docker --version" to command line

### How to run

- On Windows OS --> Find powershell script in the root of the any test projects "GenerateAllureResultsAndOpenInDockerContainer.ps1"
- Run this script in powershell

![image](https://user-images.githubusercontent.com/8307892/204125271-e2926a2a-c620-4d08-933a-ca90a2388c15.png)


Note: Results will be opened in Docker attached mode. It means, container will be stopped once you close PowerShell window. 
In order to run it in dettach mode, please open Powershell and change last line "docker-compose up" to "docker-compose up -d"








