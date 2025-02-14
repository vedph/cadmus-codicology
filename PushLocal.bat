@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Codicology.Graph\bin\Debug\Cadmus.Codicology.Graph.7.0.3.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Codicology.Parts\bin\Debug\Cadmus.Codicology.Parts.7.0.3.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Codicology.Services\bin\Debug\Cadmus.Codicology.Services.7.0.3.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Codicology.Parts\bin\Debug\Cadmus.Seed.Codicology.Parts.7.0.3.nupkg -source C:\Projects\_NuGet
pause
