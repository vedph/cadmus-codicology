@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Codicology.Graph\bin\Release\Cadmus.Codicology.Graph.9.0.5.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Codicology.Parts\bin\Release\Cadmus.Codicology.Parts.9.0.5.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Codicology.Services\bin\Release\Cadmus.Codicology.Services.9.0.5.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Codicology.Parts\bin\Release\Cadmus.Seed.Codicology.Parts.9.0.5.nupkg -source C:\Projects\_NuGet
pause
