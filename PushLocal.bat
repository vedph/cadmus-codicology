@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
pause
c:\exe\nuget add .\Cadmus.Codicology.Parts\bin\Debug\Cadmus.Codicology.Parts.3.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Codicology.Services\bin\Debug\Cadmus.Codicology.Services.3.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Codicology.Parts\bin\Debug\Cadmus.Seed.Codicology.Parts.3.0.0.nupkg -source C:\Projects\_NuGet
pause
