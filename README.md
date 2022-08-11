Backend part, ASP.Net Core Web Api 

How to use:
Requirements:
- Visual Studio 2022
- .NET 6.0

MODIFY launchSettings.json 
Change IP Address from 192.168.0.40 to your Local IP address
      "applicationUrl": "http://{your Local IP address}:22299",
      "sslPort": 44332
    }
  },
  "profiles": {
    "si.ineor.webapi": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://{your Local IP address}:7098;http://{your Local IP address}:5098",
      "dotnetRunMessages": true
