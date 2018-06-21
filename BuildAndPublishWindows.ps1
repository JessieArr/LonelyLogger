dotnet build /p:TrimUnusedDependencies=true -r win10-x64
dotnet publish /p:TrimUnusedDependencies=true -c release -r win10-x64