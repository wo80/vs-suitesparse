The `src/Tools` folder contains a simple C# program to manage some aspects of the solution. It can be compiled by running `dotnet build` from the command line.

The tool can be used with the following commands:

```
dotnet run clean
dotnet run clean -default =  Remove unused files from SuiteSparse
dotnet run clean -build   =  Remove build artifacts (obj files etc.)
dotnet run clean -docs    =  Remove docs from SuiteSparse
dotnet run clean -tests   =  Remove tests from SuiteSparse
dotnet run clean -matlab  =  Remove MATLAB files from SuiteSparse
dotnet run clean -all     =  Combines all above options

dotnet run update
dotnet run update -check    =  Check installed version of SuiteSparse
dotnet run update -download =  Download latest version of SuiteSparse
```