The `src/Tools` folder contains a simple C# program to manage some aspects of the solution. It can be compiled by running the `build` script from the command line.

The executable `suitesparse.exe` can be used with the following commands:

```
suitesparse clean
suitesparse clean -default =  Remove unused files from SuiteSparse
suitesparse clean -build   =  Remove build artifacts (obj files etc.)
suitesparse clean -docs    =  Remove docs from SuiteSparse
suitesparse clean -tests   =  Remove tests from SuiteSparse
suitesparse clean -matlab  =  Remove MATLAB files from SuiteSparse
suitesparse clean -all     =  Combines all above options

suitesparse update
suitesparse update -check    =  Check installed version of SuiteSparse
suitesparse update -download =  Download latest version of SuiteSparse
```