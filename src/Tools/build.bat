@echo off

set FrameworkPath="%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn"
if exist "%FrameworkPath%\csc.exe" goto :start
set FrameworkPath="%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\Roslyn"
if exist "%FrameworkPath%\csc.exe" goto :start
set FrameworkPath=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319
if exist "%FrameworkPath%\csc.exe" goto :start
:start

if exist obj rd /q /s obj
%FrameworkPath%\csc.exe /target:exe /out:mgmt.exe *.cs
popd
