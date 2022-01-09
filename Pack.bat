@echo off
echo BUILD Cadmus Codicology Packages
del .\Cadmus.Codicology.Parts\bin\Debug\*.nupkg
del .\Cadmus.Codicology.Parts\bin\Debug\*.snupkg
del .\Cadmus.Seed.Codicology.Parts\bin\Debug\*.nupkg
del .\Cadmus.Seed.Codicology.Parts\bin\Debug\*.snupkg

cd .\Cadmus.Codicology.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Codicology.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
