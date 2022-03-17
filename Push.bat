@echo off
echo PUSH PACKAGES TO NUGET
prompt
set nu=C:\Exe\nuget.exe
set src=-Source https://api.nuget.org/v3/index.json

%nu% push .\Cadmus.Codicology.Parts\bin\Debug\*.nupkg %src%
%nu% push .\Cadmus.Codicology.Services\bin\Debug\*.nupkg %src%
%nu% push .\Cadmus.Seed.Codicology.Parts\bin\Debug\*.nupkg %src%
echo COMPLETED
pause
echo on
