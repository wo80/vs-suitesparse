# SuiteSparse for Visual Studio

This is a Visual Studio solution for [SuiteSparse](https://github.com/DrTimothyAldenDavis/SuiteSparse). As of version 7.2, SuiteSparse has a mature CMake build system, making this repository kind of obsolete. There are some differences, though:

* Modified CXSparse allowing factorization of complex matrices.
* All-in-one library _suitesparse.dll_ containing all SuiteSparse solvers.

## Instructions

The repository does not contain the SuiteSparse source code. It can be obtained from https://github.com/DrTimothyAldenDavis/SuiteSparse. The Visual Studio solution was tested with SuiteSparse version [7.8.2](https://github.com/DrTimothyAldenDavis/SuiteSparse/archive/refs/tags/v7.8.2.zip), but should also work with newer versions. Download the latest version and extract it to the `src` folder (a subfolder `SuiteSparse` should be created automatically). To check if everything is in its right place, make sure that the file `src/SuiteSparse/README.txt` exists.

To build the solution, some projects require BLAS and LAPACK. Please read the wiki page [BLAS and LAPACK](https://github.com/wo80/vs-suitesparse/wiki/BLAS-and-LAPACK) for instructions on how to resolve these dependencies.

Pre-compiled binaries for windows users can be found [here](http://wo80.bplaced.net/packages/).

## Why?

The project was created to maintain SuiteSparse builds matching the [CSparse.Interop](https://github.com/wo80/csparse-interop) bindings for C#.
