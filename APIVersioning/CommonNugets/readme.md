Task: Publish to nuget.org
STEP1: Need to have a NuGet.org account.
Sign up at https://www.nuget.org if don't
STEP2: Generate an API key:
account settings on NuGet.org and create an API key.
STEP3: Publish the package:
dotnet nuget push bin/Release/MyCompany.SharedCode.1.0.0.nupkg --api-key <your_api_key> --source https://api.nuget.org/v3/index.json
