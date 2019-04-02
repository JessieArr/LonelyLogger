dotnet build -r linux-x64
dotnet publish -c release -r linux-x64
Remove-Item "./LonelyLogger/bin/Release/netcoreapp2.1/linux-x64/publish/logs" -Force -Recurse
Add-Type -Assembly System.IO.Compression.FileSystem
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
$sourcedir = "$(Get-Location)/LonelyLogger/bin/Release/netcoreapp2.1/linux-x64/publish"
$zipfilename = "$(Get-Location)/LonelyLogger-linux.zip"
[System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $false)
