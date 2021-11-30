#/bin/bash
rm ./published/* -rf
dotnet publish -c Release -o published
#dotnet publish -c Debug -o published

