@ECHO OFF
dotnet publish -c release -r win10-x64 -o dist_win10 Breacher\Breacher.csproj
tar -cvzf breacher_win10.tar dist_win10

dotnet publish -c release -r osx-x64 -o dist_osx Breacher\Breacher.csproj
tar -cvzf breacher_osx.tar dist_osx

dotnet publish -c release -r linux-x64 -o dist_linux Breacher\Breacher.csproj
tar -cvzf breacher_linux.tar dist_linux