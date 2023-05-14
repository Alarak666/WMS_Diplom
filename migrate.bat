dotnet build --project WMS.Data\WMS.Data.csproj --startup-project WMS.Data\WMS.Data.csproj
dotnet ef database drop --project WMS.Data\WMS.Data.csproj --startup-project WMS.Data\WMS.Data.csproj
dotnet ef migrations remove --project WMS.Data\WMS.Data.csproj --startup-project WMS.Data\WMS.Data.csproj
dotnet ef migrations add InitialCreate  --project WMS.Data\WMS.Data.csproj --startup-project WMS.Data\WMS.Data.csproj
dotnet ef database update --project WMS.Data\WMS.Data.csproj --startup-project WMS.Data\WMS.Data.csproj
pause