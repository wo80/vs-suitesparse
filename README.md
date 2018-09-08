# SuiteSparse for Visual Studio

This is a Visual Studio solution for [SuiteSparse](http://faculty.cse.tamu.edu/davis/suitesparse.html).

## Instructions

The repository does not contain the SuiteSparse source code. It can be obtained from http://faculty.cse.tamu.edu/davis/suitesparse.html. The Visual Studio solution was created for SuiteSparse version [5.3.0](http://faculty.cse.tamu.edu/davis/SuiteSparse/SuiteSparse-5.3.0.tar.gz), but should also work with newer versions. Download the latest version and extract it to the `src` folder (a subfolder `SuiteSparse` should be created automatically). To check if everything is in its right place, make sure that the file `src/SuiteSparse/README.txt` exists.

To build the solution, some projects (like Cholmod) require the BLAS and LAPACK. Please read the wiki page [BLAS and LAPACK](https://github.com/wo80/vs-suitesparse/wiki/BLAS-and-LAPACK) for instructions on how to resolve these dependencies.

## Why?

The project was created to maintain SuiteSparse builds matching the [CSparse.Interop](https://github.com/wo80/csparse-interop) bindings for C#.
