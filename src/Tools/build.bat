@echo off

set FrameworkPath="%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn"
if exist "%FrameworkPath%\csc.exe" goto :start
set FrameworkPath="%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\Roslyn"
if exist "%FrameworkPath%\csc.exe" goto :start
set FrameworkPath=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
if exist "%FrameworkPath%\csc.exe" goto :start
:start

if exist obj rd /q /s obj
%FrameworkPath%\csc.exe /target:exe /out:suitesparse.exe *.cs
popd
