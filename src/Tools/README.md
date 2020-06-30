The `src/Tools` folder contains a simple C# program to manage some aspects of the solution. It can be compiled by running the `build` script from the command line.

The executable `suitesparse.exe` can be used with the following commands:

```
mgmt clean
mgmt clean -default =  Remove unused files from SuiteSparse
mgmt clean -build   =  Remove build artifacts (obj files etc.)
mgmt clean -docs    =  Remove docs from SuiteSparse
mgmt clean -tests   =  Remove tests from SuiteSparse
mgmt clean -matlab  =  Remove MATLAB files from SuiteSparse
mgmt clean -all     =  Combines all above options

mgmt update
mgmt update -check    =  Check installed version of SuiteSparse
mgmt update -download =  Download latest version of SuiteSparse
```