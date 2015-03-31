set var=0.0.37

c:\LocalNugetPackages\nuget pack ValueObjects.nuspec -Version %var%


move *.nupkg c:\\LocalNugetPackages

pause