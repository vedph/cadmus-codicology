@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Codicology.Parts\bin\Release\Cadmus.Codicology.Parts.10.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Codicology.Services\bin\Release\Cadmus.Codicology.Services.10.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Codicology.Parts\bin\Release\Cadmus.Seed.Codicology.Parts.10.0.0.nupkg -source C:\Projects\_NuGet
pause
