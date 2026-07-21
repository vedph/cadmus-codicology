@echo off
echo BUILD Cadmus Codicology Packages
del .\Cadmus.Codicology.Parts\bin\Release\*.*nupkg
del .\Cadmus.Seed.Codicology.Parts\bin\Release\*.*nupkg
del .\Cadmus.Codicology.Services\bin\Release\*.*nupkg
del .\Cadmus.Codicology.Graph\bin\Release\*.*nupkg

cd .\Cadmus.Codicology.Graph
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Codicology.Parts
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Codicology.Parts
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Codicology.Services
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
