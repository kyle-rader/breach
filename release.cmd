@ECHO OFF
dotnet publish -c release -r win10-x64 -o dist_win10 Breacher\Breacher.csproj
PowerShell -c "Compress-Archive -Path .\dist_win10\* -DestinationPath win10.zip"

dotnet publish -c release -r osx-x64 -o dist_osx Breacher\Breacher.csproj
PowerShell -c "Compress-Archive -Path .\dist_osx\* -DestinationPath osx.zip"

dotnet publish -c release -r linux-x64 -o dist_linux Breacher\Breacher.csproj
PowerShell -c "Compress-Archive -Path .\dist_linux\* -DestinationPath linux.zip"
