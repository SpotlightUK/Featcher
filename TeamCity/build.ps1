src\.nuget\NuGet.exe restore TeamCity\packages.config -PackagesDirectory src\packages

$dotNetVersion = "4.0"
$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\$dotNetVersion"
$regProperty = "MSBuildToolsPath"

$msbuildExe = join-path -path (Get-ItemProperty $regKey).$regProperty -childpath "msbuild.exe"

& $msbuildExe .\TeamCity\build.msbuild
