dotnet build -r osx.10.11-x64
dotnet publish -c release -r osx.10.11-x64
Remove-Item "./LonelyLogger/bin/Release/netcoreapp2.1/osx.10.11-x64/publish/logs" -Force -Recurse
Add-Type -Assembly System.IO.Compression.FileSystem
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
$sourcedir = "$(Get-Location)/LonelyLogger/bin/Release/netcoreapp2.1/osx.10.11-x64/publish"
$zipfilename = "$(Get-Location)/LonelyLogger-mac.zip"
[System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $false)