dotnet build /p:TrimUnusedDependencies=true -r win10-x64
dotnet publish /p:TrimUnusedDependencies=true -c release -r win10-x64
Remove-Item "./LonelyLogger/bin/Release/netcoreapp2.1/win10-x64/publish/logs" -Force -Recurse
Add-Type -Assembly System.IO.Compression.FileSystem
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
$sourcedir = "./LonelyLogger/bin/Release/netcoreapp2.1/win10-x64/publish"
$zipfilename = "LonelyLogger-windows.zip"
[System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $false)