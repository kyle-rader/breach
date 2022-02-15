dotnet publish --self-contained -c release -r win10-x64 -o dist_win10 Breach\Breach.csproj
Compress-Archive -Path .\dist_win10\* -DestinationPath win10.zip -Force

dotnet publish --self-contained -c release -r osx-x64 -o dist_osx Breach\Breach.csproj
Compress-Archive -Path .\dist_osx\* -DestinationPath osx.zip -Force

dotnet publish --self-contained -c release -r linux-x64 -o dist_linux Breach\Breach.csproj
Compress-Archive -Path .\dist_linux\* -DestinationPath linux.zip -Force
